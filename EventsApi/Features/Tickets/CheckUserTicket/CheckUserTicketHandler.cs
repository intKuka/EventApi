using EventsApi.Settings;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.CheckUserTicket
{
    [UsedImplicitly]
    public class CheckUserTicketHandler : IRequestHandler<CheckUserTicketQuery, ScResult<IEnumerable<Ticket>>>
    {
        private readonly IHttpClientFactory _factory;
        private readonly IOptions<ServicesUris> _options;

        public CheckUserTicketHandler(IHttpClientFactory factory, IOptions<ServicesUris> options)
        {
            _factory = factory;
            _options = options;
        }

        public async Task<ScResult<IEnumerable<Ticket>>> Handle(CheckUserTicketQuery request, CancellationToken cancellationToken)
        {
            var client = _factory.CreateClient(Global.EventClient);
            using var response = await client.GetAsync($"{_options.Value.Users}/{request.UserGuid}", cancellationToken);
            return response.Content.ReadAsStringAsync(cancellationToken).Result == "false" 
                ? new ScResult<IEnumerable<Ticket>>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" }) 
                : new ScResult<IEnumerable<Ticket>>(await TicketsData.CheckUserTicket(request.Event, request.UserGuid));
        }
    }
}
