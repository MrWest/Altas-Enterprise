using System;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Represents the base class of a view model which actions are reflected in the state of the system. Uses specific services
    /// to carry its operations to the system core.
    /// </summary>
    /// <typeparam name="TServices">The type of application services the current view model uses.</typeparam>
    public abstract class ServiceBackedViewModel<TServices> : ViewModelBase where TServices : IDisposable
    {
        private int _stack;
        private TServices _services;


        /// <summary>
        /// Executes the given method using the application services this view model requires.
        /// </summary>
        /// <param name="method">The method to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        protected virtual T ExecuteUsingServices<T>(Func<TServices, T> method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            return ExecuteSafely(() =>
            {
                try
                {
                    TServices services = StackServices();
                    return method(services);
                }
                finally
                {
                    DisposeServices();
                }
            });
        }

        /// <summary>
        /// Executes the given method using the application services this view model requires.
        /// </summary>
        /// <param name="method">The method to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        protected virtual void ExecuteUsingServices(Action<TServices> method)
        {
            ExecuteUsingServices(new Func<TServices, object>(services =>
            {
                method(services);
                return null;
            }));
        }

        /// <summary>
        /// Creates a new instance of the application services that this class will use.
        /// </summary>
        /// <returns>An instance of <typeparamref name="TServices"/>.</returns>
        protected virtual TServices CreateServices()
        {
            var crap = this;
            return ServiceLocator.Current.GetInstance<TServices>();
        }

        private TServices StackServices()
        {
            return _stack++ == 0 ? (_services = CreateServices()) : _services;
        }

        private void DisposeServices()
        {
            if (--_stack == 0)
                _services.Dispose();
        }
    }
}
