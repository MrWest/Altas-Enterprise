using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentTypePresenter" /> being the presenter view model used to
    ///     decorated and impersonate Investment Type domain entities in the UI.
    /// </summary>
    public class InvestmentTypePresenter : CodedNomenclatorPresenterBase<IInvestmentType, IInvestmentTypeManagerApplicationServices>, IInvestmentTypePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentTypePresenter" /> given an Investment Type.
        /// </summary>
        /// <param name="investmentType">The Investment Type to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentType" /> is null.</exception>
        public InvestmentTypePresenter(IInvestmentType investmentType)
            : base(investmentType)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedInvestmentType; }
        }
    }
}