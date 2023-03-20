using EventsApi.Features.Models;
using EventsApi.Features.Users;
using MongoDB.Driver;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Tickets.Data
{
    public class TicketsData
    {

        public static async Task<List<Ticket>> CheckUserTicket(Event eEvent, Guid userGuid)
        {
            var user = TempUserData.GetById(userGuid);
            return await Task.FromResult(eEvent.TicketList.Where(ticket => ticket.Owner == user.Id).ToList());
        }

        public static bool CheckSeat(Event eEvent, int seat)
        {
            if (eEvent.HasNumeration == false) throw new ScException("Мероприятие не содержит распределение по местам");
            if (seat > eEvent.TicketsQuantity) throw new ScException("Мероприятие не содержит такого места");
            return eEvent.TicketList[seat - 1].Owner == Guid.Empty;
        }
    }
}
