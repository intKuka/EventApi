using EventsApi.MongoDb;
using EventsApi.Settings;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.IssueTicket
{
    [UsedImplicitly]
    public class IssueTicketHandler : IRequestHandler<IssueTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;
        private readonly IOptions<ServicesUris> _options;

        public IssueTicketHandler(IEventRepo eventData, IHttpClientFactory factory, IOptions<ServicesUris> options)
        {
            _eventData = eventData;
            _factory = factory;
            _options = options;
        }

        public async Task<ScResult<Ticket>> Handle(IssueTicketCommand request, CancellationToken cancellationToken)
        {
            var client = _factory.CreateClient(Global.EventClient);
            using var response = await client.GetAsync($"{_options.Value.Users}/{request.UserGuid}", cancellationToken);
            if (response.Content.ReadAsStringAsync(cancellationToken).Result == "false")
                return new ScResult<Ticket>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" });
            var freeTicket = TicketsData.IssueFreeTicket(request.Event, request.UserGuid);
            await _eventData.UpdateEvent(request.Event);
            return new ScResult<Ticket>(freeTicket);
        }
    }
}
