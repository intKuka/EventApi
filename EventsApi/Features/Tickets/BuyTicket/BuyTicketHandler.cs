using EventsApi.MongoDb;
using MediatR;
using SC.Internship.Common.ScResult;
using JetBrains.Annotations;
using EventsApi.Settings;
using Microsoft.Extensions.Options;

namespace EventsApi.Features.Tickets.BuyTicket;

[UsedImplicitly]
public class BuyTicketHandler : IRequestHandler<BuyTicketCommand, ScResult<Ticket>>
{
    private readonly IEventRepo _eventData;
    private readonly IHttpClientFactory _factory;
    private readonly IOptions<ServicesUris> _options;

    public BuyTicketHandler(IEventRepo eventData, IHttpClientFactory factory, IOptions<ServicesUris> options)
    {
        _eventData = eventData;
        _factory = factory;
        _options = options;
    }

    public async Task<ScResult<Ticket>> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
    {
        var client = _factory.CreateClient(Global.EventClient);
        using (var response = await client.GetAsync($"{_options.Value.Users}/{request.UserGuid}", cancellationToken))
        {
            if (response.Content.ReadAsStringAsync(cancellationToken).Result == "false")
                return new ScResult<Ticket>(new ScError { Message = $"Пользователь {request.UserGuid} не найден" });
        }

        using (await client.PostAsync($"{_options.Value.Payment}/create", null, cancellationToken))
        {
        }
            
        try
        {
            var freeTicket = TicketsData.IssueFreeTicket(request.Event, request.UserGuid);
            await _eventData.UpdateEvent(request.Event);
            using (await client.PutAsync($"{_options.Value.Payment}/confirm", null,  cancellationToken))
                return new ScResult<Ticket>(freeTicket);
        }
        catch (Exception)
        {
            using (await client.PutAsync($"{_options.Value.Payment}/cancel", null, cancellationToken))
                throw;
        }

    }
}