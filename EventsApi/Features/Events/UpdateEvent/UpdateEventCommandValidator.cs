using FluentValidation;
using JetBrains.Annotations;

namespace EventsApi.Features.Events.UpdateEvent
{
    [UsedImplicitly]
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(e => e.Event.Id)
                .NotEmpty().WithName("id").WithMessage("Id не должен быть пустым");

            RuleFor(e => e.Event.Ends)
                .NotEmpty().WithName("ends").WithMessage("Нужно указать когда кончается мероприятия")
                .GreaterThan(e => e.Event.Starts).WithName("ends").WithMessage("Дата окончания должна быть позже даты начала");
            
            RuleFor(e => e.Event.SpaceId)
                .NotEqual(Guid.Empty).WithName("spaceId").WithMessage("Нужен валидный guid пространства")
                .NotEmpty().WithName("spaceId").WithMessage("Необходим guid пространства");

            RuleFor(e => e.Event.Name)
                .NotEmpty().WithName("name").WithMessage("Имя не должно быть пустым")
                .MaximumLength(100).WithName("name").WithMessage("Имя не может быть более 100 символов");

            RuleFor(e => e.Event.Description)
                .MaximumLength(200).WithName("description").WithMessage("Описание не должно превышать 200 символов");

            RuleFor(e => e.Event.TicketsQuantity)
                .GreaterThanOrEqualTo(0).WithName("ticketQuantity").WithMessage("Количество билетов не может быть отрицательным");

            RuleFor(e => e.Event.Price)
                .GreaterThanOrEqualTo(0).WithName("price").WithMessage("Цена не может быть меньше нуля");
        }
    }
}
