using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IPhasePresenter" /> being the presenter view model used to decorated and
    ///     impersonate Phase domain entities in the UI.
    /// </summary>
    public class PhasePresenter : CodedNomenclatorPresenterBase<IPhase, IPhaseManagerApplicationServices>, IPhasePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PhasePresenter"/> given an Phase.
        /// </summary>
        /// <param name="phase">The phase to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="phase"/> is null.</exception>
        public PhasePresenter(IPhase phase)
            : base(phase)
        {
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedPhase; }
        }
    }
}