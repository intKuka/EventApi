using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.IssueTicket
{
    [UsedImplicitly]
    public class IssueTicketHandler : IRequestHandler<IssueTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;

        public IssueTicketHandler(IEventRepo eventData, IHttpClientFactory factory)
        {
            _eventData = eventData;
            _factory = factory;;
        }

        public async Task<ScResult<Ticket>> Handle(IssueTicketCommand request, CancellationToken cancellationToken)
        {
            var client = _factory.CreateClient(Global.EventClient);
            using var response = await client.GetAsync($"http://localhost:5018/users/{request.UserGuid}", cancellationToken);
            if (response.Content.ReadAsStringAsync(cancellationToken).Result == "false")
                return new ScResult<Ticket>(new ScError() { Message = $"Пользователь {request.UserGuid} не найден" });
            var freeTicket = TicketsData.IssueFreeTicket(request.Event, request.UserGuid);
            await _eventData.UpdateEvent(request.Event);
            return new ScResult<Ticket>(freeTicket);
        }
    }
}
