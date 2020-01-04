using System;
using System.Windows;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    /// Implements an instance for a convertible entity presenter interface
    /// </summary>
    public  class ConvertibleEntityPresenter<TEntity> : NomenclatorPresenterBase<TEntity, IConvertibleEntityManagerApplicationServices<TEntity>>,
        IConvertibleEntityPresenter<TEntity>, IView
        where TEntity:class ,IConvertibleEntity
    {
        private IUnitConverterViewModel<TEntity> _convertions;

        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedConvertible; }
        }

        public override string ToString()
        {
            return Letters;
        }

        /// <summary>
        /// Letters for the current entities
        /// </summary>
        public String Letters
        {
            get { return Object.Letters; }
            set
            {
                SetProperty(v => Object.Letters = v, value);
            }
        }

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IUnitConverterViewModel<TEntity> Convertions
        {
            get
            {
                if (_convertions == null)
                {
                    _convertions = ServiceLocator.Current.GetInstance<IUnitConverterViewModel<TEntity>>();
                    //  Actioner(x => x.SuperiorCategory = Object);
                    _convertions.ConvertibleEntityPresenter= this;
                    _convertions.Load();

                    _convertions.Raised += OnInteractionRequested;

                }
                return _convertions;
            }
          
        }

        public override void Notify()
        {
            OnPropertyChanged(()=>Convertions);
        }

        public decimal ConvertionFactorFor(TEntity entity)
        {
            var current = Convertions.Items.FirstOrDefault(x => x.ConversionUnit.Id.ToString() == entity.Id.ToString());
            return current != null ? current.Factor : 1;
        }

        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new datacontext are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }
        public object DataContext { get; set; }
        public bool Export(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                return false;
            return CreateServices().Export(databaseContext, Object)!=null;
        }
    }
}
