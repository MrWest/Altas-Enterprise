using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.ExceptionHandling.Stubs
{
    public interface IExceptionThrower : IInteractionRequester
    {
        Exception AnyException { get; set; }

        ValidationException ValidationException { get; set; }


        void ThrowAnyException();

        void ThrowValidationException();

        int DoNoThrowException();
    }


    public class ExceptionThrower : IExceptionThrower
    {
        public Exception AnyException { get; set; }

        public ValidationException ValidationException { get; set; }


        public void ThrowAnyException()
        {
            throw AnyException;
        }

        public void ThrowValidationException()
        {
            throw ValidationException;
        }

        public int DoNoThrowException()
        {
            return 900;
        }

        public TContext Interact<TContext>(TContext context, Action callback) where TContext : Microsoft.Practices.Prism.Interactivity.InteractionRequest.INotification
        {
            return context;
        }

        public event EventHandler<InteractionRequestedEventArgs> Raised;
    }
}
