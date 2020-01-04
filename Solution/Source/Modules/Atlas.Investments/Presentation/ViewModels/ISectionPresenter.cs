using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    /// incapsulates a Section  instance for presentation 
    /// </summary>
    public interface ISectionPresenter :  IBudgetComponentPresenter<ISection>
    {

        /// <summary>
        /// Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        IGenericSectionPresenter AboveSection { get; set; }

        /// <summary>
        /// Gets the crud view model used to manage the section  the price system section contained in the current
        /// presenter.
        /// </summary>
        ISectionViewModel Sections { get; }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="ISectionPresenter" /> name.
        /// </summary>
        string ShortName { get; set; }

        bool IsExpanded { get; set; }


    }
}
