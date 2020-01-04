using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract to be implemented by the Section crud view models, these view models will handle the CRUD operations for Section
    ///     domain entities in the UI (presentation layer).
    /// </summary>
    public interface ISectionViewModel : ICrudViewModel<ISection, ISectionPresenter>
    {
        /// <summary>
        /// Gets os Set the Section Above the current one.
        /// </summary>
        IGenericSectionPresenter AboveSection { get; set; }
    }
}
