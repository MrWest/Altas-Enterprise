using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///Incapsutales a pricesystem or section object 
    /// </summary>
   public  interface IGenericSectionPresenter: IPresenter
       //where TItem: class , IPriceSystem
    {
        /// <summary>
        /// Gets the crud view model used to manage the section  the price system section contained in the current
        /// presenter.
        /// </summary>
       ISectionViewModel Sections { get; }
       

    }

  
}
