using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;
using System.Net.Http;

namespace EventsApi.Features.Tickets.CheckUserTicket
{
    [UsedImplicitly]
    public class CheckUserTicketHandler : IRequestHandler<CheckUserTicketQuery, ScResult<IEnumerable<Ticket>>>
    {
        private readonly HttpClient _httpClient;

        public CheckUserTicketHandler()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ScResult<IEnumerable<Ticket>>> Handle(CheckUserTicketQuery request, CancellationToken cancellationToken)
        {
            using var response = await _httpClient.GetAsync($"http://localhost:5018/users/{request.UserGuid}", cancellationToken);
            return response.Content.ReadAsStringAsync(cancellationToken).Result == "false" 
                ? new ScResult<IEnumerable<Ticket>>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" }) 
                : new ScResult<IEnumerable<Ticket>>(await TicketsData.CheckUserTicket(request.Event, request.UserGuid));
        }
    }
}
