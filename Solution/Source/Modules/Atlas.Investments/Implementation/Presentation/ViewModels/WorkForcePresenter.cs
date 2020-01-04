using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkForcePresenter" /> being the presenter view model used to decorated
    ///     and impersonate Work Force domain entities in the UI.
    /// </summary>
    public class WorkForcePresenter : EntityPresenterBase<IWorkForce, IWorkForceManagerApplicationServices>, IWorkForcePresenter
    {
        private readonly IWageScaleProvider _wageScaleProvider = ServiceLocator.Current.GetInstance<IWageScaleProvider>();
        private IWageScalePresenter _temporalWageScale;
        private IWageScalePresenter _wageScale;


        /// <summary>
        ///     Initializes a new instance of <see cref="WorkForcePresenter" /> given an WorkForce.
        /// </summary>
        /// <param name="workForce">The work force to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="workForce" /> is null.</exception>
        public WorkForcePresenter(IWorkForce workForce)
            : base(workForce)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedWorkForce; }
        }


        /// <summary>
        ///     Gets or sets the code of the <see cref="IWorkForce" />.
        /// </summary>
        public string Code
        {
            get { return Object.Code; }
            set { SetProperty(v => Object.Code = v, value); }
        }

        /// <summary>
        ///     Gets or sets the name of the <see cref="IWorkForce" />.
        /// </summary>
        public string Name
        {
            get { return Object.Name; }
            set { SetProperty(v => Object.Name = v, value); }
        }

        /// <summary>
        ///     Gets or sets the wage scale of the <see cref="IWorkForce" />.
        /// </summary>
        public IWageScalePresenter WageScale
        {
            get
            {
                if (_wageScale == null && Object.WageScale != null)
                {
                    // TODO: Change the query below, for something like this: wage scale provider.Find (...)
                    //var provider =
                    //    ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<IWageScale>>();
                    // _wageScale.Object = provider.GetEntity(Object.WageScale);
                     _wageScale = _wageScaleProvider.WageScales.SingleOrDefault(x => Equals(x.Object.Id, Object.WageScale));
                    _wageScale.PropertyChanged += WageScaleOnPropertyChanged;
                }

                return _wageScale;
            }
            set
            {
                if (!SetProperty(v => { Object.WageScale = v.Object.Id; }, value))
                    return;

                if (_wageScale != null)
                    _wageScale.PropertyChanged -= WageScaleOnPropertyChanged;

                _wageScale = value;
                if (_wageScale != null)
                    _wageScale.PropertyChanged += WageScaleOnPropertyChanged;

                OnPropertyChanged(() => Retribution);
            }
        }

        /// <summary>
        ///     Gets the retribution of the underlying <see cref="IWorkForce" />.
        /// </summary>
        public decimal Retribution
        {
            get { return WageScale==null? 0:WageScale.Retribution; }
        }

        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenters (<see cref="IWageScalePresenter" />) wrapping the
        ///     <see cref="IWageScale" /> there are in the system.
        /// </summary>
        public IEnumerable<IWageScalePresenter> WageScales
        {
            get { return _wageScaleProvider.WageScales; }
        }


        private void WageScaleOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(() => WageScale);
            OnPropertyChanged(() => Retribution);
        }
    }
}