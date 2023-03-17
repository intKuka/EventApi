﻿using EventsApi.Features.Events.Data;
using EventsApi.Features.Models;
using FluentValidation;

namespace EventsApi.Features.Events.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            //Cascade(CascadeMode.Stop)
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(e => e.Ends).GreaterThan(e => e.Starts).WithMessage("Дата окончания раньше даты начала");
            RuleFor(e => e.ImageId).NotEqual(Guid.Empty).WithMessage("Нужен валидный guid");
            RuleFor(e => e.SpaceId).NotEqual(Guid.Empty).WithMessage("Нужен валидный guid");
            RuleFor(e => e.Name).NotEmpty().WithMessage("Имя не должно быть пустым")
                .MaximumLength(100).WithMessage("Имя не может быть более 100 символов");
            RuleFor(e => e.Description).MaximumLength(200).WithMessage("Описание не должно превышать 200 символов");
            RuleFor(e => e.TicketsQuantity).GreaterThanOrEqualTo(0).WithMessage("Количество билетов не может быть отрицательным");
            

    }

    }
}
