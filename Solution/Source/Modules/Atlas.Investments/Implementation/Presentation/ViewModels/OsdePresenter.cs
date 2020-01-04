using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOsdePresenter" /> being the presenter view model used to decorated and
    ///     impersonate OSDE domain entities in the UI.
    /// </summary>
    public class OsdePresenter : CodedNomenclatorPresenterBase<IOsde, IOsdeManagerApplicationServices>, IOsdePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OacePresenter" /> given an OSDE.
        /// </summary>
        /// <param name="osde">The OASE to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="osde" /> is null.</exception>
        public OsdePresenter(IOsde osde)
            : base(osde)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedOsde; }
        }
    }
}