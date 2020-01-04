using System;
using System.Reflection;
using Microsoft.Practices.Prism.Modularity;
using CompanyName.Atlas.Contracts.Modularity;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using CompanyName.Atlas.Contracts.Presentation.Prism;

namespace CompanyName.Atlas.Contracts.Implementation.Modularity
{
    /// <summary>
    /// Represents the base class of the subsystems in the Atlas suite
    /// </summary>
    public abstract class SubsystemBase : ISubsystem
    {
        /// <summary>
        /// Gets the name of the current subsystem.
        /// </summary>
        public string Name
        {
            get
            {
                ModuleAttribute attribute = GetType().GetCustomAttribute<ModuleAttribute>();
                return attribute.ModuleName;
            }
        }

        /// <summary>
        /// Gets the version of the current subsystem.
        /// </summary>
        public Version Version
        {
            get
            {
                return GetType().Assembly.GetName().Version;
            }
        }


        /// <summary>
        /// When overridden in a deriver it initializes the current subsystem.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Gets a new instance of a crud view model and loads it.
        /// </summary>
        /// <returns>A new and initialized instance of <typeparamref name="TViewModel"/>.</returns>
        protected TViewModel CreateAndInitialize<TViewModel>()
            where TViewModel : ICrudViewModel
        {
            var viewModel = ServiceLocator.Current.GetInstance<TViewModel>();

            viewModel.Raised -= OnInteractionRequested;


            viewModel.Load();

            viewModel.Raised += OnInteractionRequested;

            return viewModel;
        }

        protected internal virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            InteractionRequestHelpers.Notify(e);
        }
    }
}
