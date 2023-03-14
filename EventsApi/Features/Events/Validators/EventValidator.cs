using EventsApi.Features.Events.Data;
using FluentValidation;

namespace EventsApi.Features.Events.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(e => e.Ends).GreaterThan(e => e.Starts).WithMessage("End date is earlier than start date");
            RuleFor(e => e.ImageId).NotNull();
            RuleFor(e => e.SpaceId).NotNull();
        }

    }
}
