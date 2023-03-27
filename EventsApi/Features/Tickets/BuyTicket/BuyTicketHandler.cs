using EventsApi.Features.Tickets.IssueTicket;
using EventsApi.MongoDb;
using MediatR;
using SC.Internship.Common.ScResult;
using System.Net.Http;

namespace EventsApi.Features.Tickets.BuyTicket
{
    public class BuyTicketHandler : IRequestHandler<BuyTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        private readonly HttpClient _httpClient;

        public BuyTicketHandler(IEventRepo eventData, IHttpClientFactory factory)
        {
            _eventData = eventData;
            _httpClient = factory.CreateClient();
        }

        public async Task<ScResult<Ticket>> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            using (await _httpClient.GetAsync("http://localhost:5234/payment/create", cancellationToken))
            {
            }

            try
            {
                var ticket = await _eventData.IssueTicket(request.Event, request.UserGuid);
                using (await _httpClient.GetAsync("http://localhost:5234/payment/confirm", cancellationToken))
                {
                }

                return new ScResult<Ticket>(ticket);
            }
            catch (Exception)
            {
                using (await _httpClient.GetAsync("http://localhost:5234/payment/cancel", cancellationToken))
                {
                }

                throw;
            }

        }
    }
}
