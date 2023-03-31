using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.UpdateEvent;

public record UpdateEventCommand(Event Event) : ICommand<ScResult<Event>>;
