using EventsApi.MongoDb;
using MediatR;
using SC.Internship.Common.ScResult;
using JetBrains.Annotations;

namespace EventsApi.Features.Tickets.BuyTicket
{
    [UsedImplicitly]
    public class BuyTicketHandler : IRequestHandler<BuyTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;

        public BuyTicketHandler(IEventRepo eventData, IHttpClientFactory factory)
        {
            _eventData = eventData;
            _factory = factory;
        }

        public async Task<ScResult<Ticket>> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            var client = _factory.CreateClient(Global.EventClient);
            using (var response = await client.GetAsync($"http://localhost:5018/users/{request.UserGuid}", cancellationToken))
            {
                if (response.Content.ReadAsStringAsync(cancellationToken).Result == "false")
                    return new ScResult<Ticket>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" });
            }
            
            try
            {
                var freeTicket = TicketsData.IssueFreeTicket(request.Event, request.UserGuid);
                await _eventData.UpdateEvent(request.Event);
                using (await client.PutAsync("http://localhost:5234/payment/confirm", null,  cancellationToken))
                    return new ScResult<Ticket>(freeTicket);
            }
            catch (Exception)
            {
                using (await client.PutAsync("http://localhost:5234/payment/cancel", null, cancellationToken))
                    throw;
            }

        }
    }
}
