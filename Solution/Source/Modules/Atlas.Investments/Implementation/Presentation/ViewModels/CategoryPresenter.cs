using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="ICategoryPresenter" /> being the presenter view model used to decorated
    ///     and impersonate Category domain entities in the UI.
    /// </summary>
    public class CategoryPresenter : CodedNomenclatorPresenterBase<ICategory, ICategoryManagerApplicationServices>, ICategoryPresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="CategoryPresenter" /> given an Category.
        /// </summary>
        /// <param name="category">The category to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="category" /> is null.</exception>
        public CategoryPresenter(ICategory category)
            : base(category)
        {
        }
        
        
        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedCategory; }
        }
    }
}