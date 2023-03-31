using FluentValidation;

namespace EventsApi.Features.Events.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(e => e.NewEvent.Id)
                .Empty().WithName("id").WithMessage("Id устанавливается автоматически");
            
            RuleFor(e => e.NewEvent.Ends)
                .NotEmpty().WithName("ends").WithMessage("Нужно указать когда кончается мероприятия")
                .GreaterThan(e => e.NewEvent.Starts).WithName("ends").WithMessage("Дата окончания должна быть позже даты начала");
            
            RuleFor(e => e.NewEvent.SpaceId)
                .NotEqual(Guid.Empty).WithName("spaceId").WithMessage("Нужен валидный guid пространства")
                .NotEmpty().WithName("spaceId").WithMessage("Необходим guid пространства");

            RuleFor(e => e.NewEvent.Name)
                .NotEmpty().WithName("name").WithMessage("Имя не должно быть пустым")
                .MaximumLength(100).WithName("name").WithMessage("Имя не может быть более 100 символов");

            RuleFor(e => e.NewEvent.Description)
                .MaximumLength(200).WithName("description").WithMessage("Описание не должно превышать 200 символов");

            RuleFor(e => e.NewEvent.TicketsQuantity)
                .GreaterThanOrEqualTo(0).WithName("ticketsQuantity").WithMessage("Количество билетов не может быть отрицательным");

            RuleFor(e => e.NewEvent.Price)
                .GreaterThanOrEqualTo(0).WithName("price").WithMessage("Цена не может быть меньше нуля");
        }
    }
}
