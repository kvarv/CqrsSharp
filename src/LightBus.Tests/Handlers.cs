using System;
using System.Threading.Tasks;

namespace LightBus.Tests
{
    public class CommandHandler : IHandleMessages<Command>
    {
        public Task HandleAsync(Command command)
        {
            command.IsHandled = true;
            return TaskExt.FromResult();
        }
    }

    public class AnotherCommandHandler : IHandleMessages<Command>
    {
        public Task HandleAsync(Command command)
        {
            command.IsHandled = true;
            return TaskExt.FromResult();
        }
    }

    public class MessageHandler : IHandleMessages<IMessage>
    {
        public bool IsHandled { get; set; }

        public Task HandleAsync(IMessage command)
        {
            IsHandled = true;
            return TaskExt.FromResult();
        }
    }

    public class CommandHandlerThatSendsAnEvent : IHandleMessages<Command>
    {
        private readonly IBus _bus;

        public CommandHandlerThatSendsAnEvent(IBus bus)
        {
            _bus = bus;
        }

        public Task HandleAsync(Command command)
        {
            return _bus.PublishAsync(new EventWithCommand {Command = command});
        }
    }

    public class AsyncCommandHandler : IHandleMessages<AsyncCommand>
    {
        public Task HandleAsync(AsyncCommand command)
        {
            return TaskExt.Delay(50).ContinueWith(task => command.IsHandled = true);
        }
    }

    public class CommandHandlerThatThrowException : IHandleMessages<CommandWithException>
    {
        public Task HandleAsync(CommandWithException message)
        {
            throw new InvalidOperationException();
        }
    }

    public class EventHandler : IHandleMessages<Event>
    {
        public Task HandleAsync(Event @event)
        {
            @event.NumberOfTimesHandled++;
            return TaskExt.FromResult();
        }
    }

    public class AnotherEventHandler : IHandleMessages<Event>
    {
        public Task HandleAsync(Event @event)
        {
            @event.NumberOfTimesHandled++;
            return TaskExt.FromResult();
        }
    }

    public class EventWithCommandHandler : IHandleMessages<EventWithCommand>
    {
        public Task HandleAsync(EventWithCommand @event)
        {
            @event.Command.IsHandled = true;
            return TaskExt.FromResult();
        }
    }

    public class QueryHandler : IHandleQueries<Query, Response>
    {
        public Task<Response> HandleAsync(Query query)
        {
            return TaskExt.FromResult(new Response {IsHandled = true});
        }
    }

    public class AnotherQueryHandler : IHandleQueries<Query, Response>
    {
        public Task<Response> HandleAsync(Query query)
        {
            return TaskExt.FromResult(new Response {IsHandled = true});
        }
    }

    public class EventWithExceptionHandler : IHandleMessages<EventWithException>
    {
        public Task HandleAsync(EventWithException message)
        {
            throw new InvalidOperationException();
        }
    }

    public class QueryWithExceptionHandler : IHandleQueries<QueryWithExcepetion, Response>
    {
        public Task<Response> HandleAsync(QueryWithExcepetion query)
        {
            throw new InvalidOperationException();
        }
    }
}