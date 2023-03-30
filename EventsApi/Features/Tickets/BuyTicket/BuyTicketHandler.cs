using EventsApi.Features.Tickets.IssueTicket;
using EventsApi.MongoDb;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using System.Net.Http;
using JetBrains.Annotations;

namespace EventsApi.Features.Tickets.BuyTicket
{
    [UsedImplicitly]
    public class BuyTicketHandler : IRequestHandler<BuyTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        private readonly HttpClient _httpClient;

        public BuyTicketHandler(IEventRepo eventData)
        {
            _eventData = eventData;
            _httpClient = new HttpClient();
        }

        public async Task<ScResult<Ticket>> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            using (var response = await _httpClient.GetAsync($"http://localhost:5018/users/{request.UserGuid}", cancellationToken))
            {
                if (response.Content.ReadAsStringAsync(cancellationToken).Result == "false")
                    return new ScResult<Ticket>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" });
            }

            using (await _httpClient.PostAsync("http://localhost:5234/payment/create", null, cancellationToken))
            {
            }

            try
            {
                var freeTicket = TicketsData.IssueFreeTicket(request.Event, request.UserGuid);
                await _eventData.UpdateEvent(request.Event);
                using (await _httpClient.PutAsync("http://localhost:5234/payment/confirm", null,  cancellationToken))
                    return new ScResult<Ticket>(freeTicket);
            }
            catch (Exception)
            {
                using (await _httpClient.PutAsync("http://localhost:5234/payment/cancel", null, cancellationToken))
                    throw;
            }

        }
    }
}
