using EventsApi.Features.Models;
using EventsApi.Features.Users;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Tickets.Data
{
    public class TicketsData
    {
        public static async Task<Ticket> IssueTicket(Event eEvent, Guid userGuid)
        {
            var user = await Task.FromResult(TempUserData.Users.FirstOrDefault(x => x.Id == userGuid));
            if (user == null) throw new ScException("Пользователь не найден");
            var freeTicket = await Task.FromResult(eEvent.TicketList.FirstOrDefault(t => t.Owner == Guid.Empty));
            if (freeTicket == null) throw new ScException("Нет свободных билетов");
            freeTicket.Owner = userGuid;
            return freeTicket;

        }

        public static async Task<List<Ticket>> CheckUserTicket(Event eEvent, Guid userGuid)
        {
            var user = TempUserData.GetById(userGuid);
            return await Task.FromResult(eEvent.TicketList.Where(ticket => ticket.Owner == user.Id).ToList());
        }
    }
}
