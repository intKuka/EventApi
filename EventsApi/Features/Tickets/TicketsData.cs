using EventsApi.Features.Events;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Tickets;

public class TicketsData
{

    public static async Task<List<Ticket>> CheckUserTicket(Event eEvent, Guid userGuid)
    {
        return await Task.FromResult(eEvent.TicketList.Where(ticket => ticket.Owner == userGuid).ToList());
    }

    public static bool CheckSeat(Event eEvent, int seat)
    {
        if (eEvent.HasNumeration == false) throw new ScException("Мероприятие не содержит распределение по местам");
        if (seat > eEvent.TicketsQuantity) throw new ScException("Мероприятие не содержит такого места");
        return eEvent.TicketList[seat - 1].Owner == Guid.Empty;
    }

    //проверяет нужно ли добовлять или оставлять билеты
    public static void TryTicketsApplication(Event eEvent)
    {
        eEvent.TicketList = new List<Ticket>();
        if (eEvent.TicketsQuantity == 0) return;
        for (var i = 0; i < eEvent.TicketsQuantity; i++)
        {
            eEvent.TicketList.Add(new Ticket());
            if (eEvent.HasNumeration) eEvent.TicketList[i].Seat = i + 1;
        }
    }

    public static Ticket IssueFreeTicket(Event eEvent, Guid userGuid)
    {
        var freeTicket = eEvent.TicketList.FirstOrDefault(t => t.Owner == Guid.Empty);
        if (freeTicket == null) throw new ScException("Нет свободных билетов");
        freeTicket.Owner = userGuid;
        return freeTicket;
    }
}