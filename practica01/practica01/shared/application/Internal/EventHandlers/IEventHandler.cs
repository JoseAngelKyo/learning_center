using practica01.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace practica01.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
