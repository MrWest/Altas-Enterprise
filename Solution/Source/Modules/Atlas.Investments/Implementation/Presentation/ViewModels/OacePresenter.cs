using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOacePresenter" /> being the presenter view model used to decorated and
    ///     impersonate OACE domain entities in the UI.
    /// </summary>
    public class OacePresenter : CodedNomenclatorPresenterBase<IOace, IOaceManagerApplicationServices>, IOacePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OacePresenter"/> given an OACE.
        /// </summary>
        /// <param name="oace">The oace to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="oace"/> is null.</exception>
        public OacePresenter(IOace oace)
            : base(oace)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedOace; }
        }
    }
}