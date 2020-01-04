using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view models handling budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to the items belong.</typeparam>

    public interface IBudgetComponentActivityPresenter<TComponent> 
       where TComponent : class, IBudgetComponent
        //where TServices : class, IBudgetComponentActivityManagerApplicationServices<TItem,TComponent>
    {
        
    }
}
