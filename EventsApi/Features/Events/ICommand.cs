using MediatR;

namespace EventsApi.Features.Events
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
