using System;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWageScalePresenter" /> being the presenter view model used to decorated
    ///     and impersonate Wage Scale domain entities in the UI.
    /// </summary>
    public class WageScalePresenter : EntityPresenterBase<IWageScale, IWageScaleManagerApplicationServices>, IWageScalePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WageScalePresenter" /> given an WageScale.
        /// </summary>
        /// <param name="wageScale">The wage scale to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="wageScale" /> is null.</exception>
        public WageScalePresenter(IWageScale wageScale)
            : base(wageScale)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedWageScale; }
        }


        /// <summary>
        ///     Gets or sets the name of the <see cref="IWageScale" />.
        /// </summary>
        public string Name
        {
            get { return Object.Name; }
            set { SetProperty(v => Object.Name = v, value); }
        }

        /// <summary>
        ///     Gets or sets the retribution of the current <see cref="IWageScale" />.
        /// </summary>
        public decimal Retribution
        {
            get { return Object.Retribution; }
            set { SetProperty(v => Object.Retribution = v, value); }
        }
    }
}