using EventsApi.Features.Events;
using EventsApi.Features.Events.CreateEvent;
using EventsApi.Features.Events.GetEventById;
using FluentValidation;
using MediatR;
using Moq;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace UnitTests
{
    public class EventsControllerTests
    {
        private EventsController _eventsController = null!;
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<IValidator<Event>> _validator = new();

        [SetUp]
        public void Setup()
        {
            _eventsController = new EventsController(_mediator.Object, _validator.Object);
        }

        [Test]
        public async Task GetEventById_WithUnexistingId_ReturnsNull()
        {
            // Arrange
            _mediator.Setup(x => x.Send(new GetEventByIdQuery(It.IsAny<Guid>()), default)).ReturnsAsync(() => null!);

            // Act
            var eEvent = await _eventsController.GetEventById(Guid.NewGuid());

            // Assert
            Assert.Null(eEvent);

        }

        [Test]
        public async Task CreateEvent_WithValidValues_ReturnsEvent()
        {
            // Arrange
            var eEvent = new Event()
            {
                Starts = new DateTime(2023, 03, 05, 14, 30, 0),
                Ends = new DateTime(2023, 03, 05, 17, 0, 0),
                Name = "Event",
                ImageId = new Guid("4c8ebbeb-ffba-4851-8300-ffd192e99372"),
                SpaceId = new Guid("169a4f10-0914-4d8d-b922-3958621a72a5"),
                TicketsQuantity = 2,
                HasNumeration = true
            };
            var expected = new ScResult<Event>(eEvent);

            _mediator.Setup(x => x.Send(new CreateEventCommand(eEvent), default))
                .ReturnsAsync(new ScResult<Event>(eEvent));

            // Act
            var actual = await _eventsController.CreateEvent(eEvent);

            // Assert
            Assert.That(actual.Result, Is.EqualTo(expected.Result));
        }

        [Test]
        public async Task CreateEvent_WithInvalidImageGuid_ThrowsValidationExceptionMessage()
        {
            // Arrange
            var eEvent = new Event()
            {
                Starts = new DateTime(2023, 03, 05, 14, 30, 0),
                Ends = new DateTime(2023, 03, 05, 17, 0, 0),
                Name = "Event",
                ImageId = new Guid("4c8ebbeb-ffba-4851-8300-ffd192e99373"),
                SpaceId = new Guid("169a4f10-0914-4d8d-b922-3958621a72a5"),
                TicketsQuantity = 2,
                HasNumeration = true
            };
            var expected = new ScException("Изображение не найдено");

            _mediator.Setup(x => x.Send(new CreateEventCommand(eEvent), default));

            // Act
            Exception actual = new();
            try
            {
                await _eventsController.CreateEvent(eEvent);
            }
            catch (Exception e)
            {
                actual = e;
            }

            // Assert
            Assert.That(actual.Message, Is.EqualTo(expected.Message)) ;
        }
    }
}