using System.ComponentModel;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Base class of the crud view models to be placed as data context of items composed controls. Notice that this crud view model can be disabled in order to reject
    /// all the calls there are made to it, but properties will still continue notifying changes and being readable.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the view model.</typeparam>
    /// <typeparam name="TPresenter">
    /// This is the type of the presenter view models used to decorate the items managed in the current crud view
    /// model.
    /// </typeparam>
    /// <typeparam name="TServices">The type application service to be used by this view model.</typeparam>
    public abstract class EnableableCrudViewModelBase<T, TPresenter, TServices> : CrudViewModelBase<T, TPresenter, TServices>, IEnableable
        where T : class, IEntity
        where TPresenter : class, IPresenter<T>
        where TServices : IItemManagerApplicationServices<T>
    {
        private bool _isEnabled = true;


        /// <summary>
        /// Gets or sets wether this object is enabled or disabled.
        /// </summary>
        public virtual bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }


        /// <summary>
        /// Adds the given presenter or adds a self created one if the current view model is enabled. If disabled, does nothing.
        /// </summary>
        /// <param name="presenter">
        /// The item presenter to add or null if there is to add another one created by the view model itself, all will be done in case
        /// the current view model is enabled.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter"/> is null.</exception>
        public override void Add(TPresenter presenter)
        {
            if (IsEnabled)
                base.Add(presenter);
        }

        /// <summary>
        /// Deletes an presenter view model from the current view model if not disabled. If disabled, does nothing.
        /// </summary>
        /// <param name="presenter">The presenter view model to delete if the current crud view model is enabled.</param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter"/> is null.</exception>
        public override void Delete(TPresenter presenter)
        {
            if (IsEnabled)
                base.Delete(presenter);
        }

        /// <summary>
        /// Determines whether there can be added an presenter view model to the current view model, in case it is enabled.
        /// If disabled, returns false.
        /// </summary>
        /// <param name="presenter">The presenter view model to determine whether can be added, can be null.</param>
        /// <returns>
        /// True if the current crud view model is enabled and <paramref name="presenter"/> according to teh services can be added; false
        /// otherwise.
        /// </returns>
        public override bool CanAdd(TPresenter presenter)
        {
            return IsEnabled && base.CanAdd(presenter);
        }

        /// <summary>
        /// Determines whether there can be deleted an presenter view model from the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to determine whether can be deleted.</param>
        /// <returns>
        /// True if given an non null presenter view model of <typeparamref name="T"/> type; false otherwise.
        /// </returns>
        public override bool CanDelete(TPresenter presenter)
        {
            return IsEnabled && base.CanDelete(presenter);
        }
    }
}
