using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Gets or sets the budget component to which belong the underlying item.
    /// </summary>
    [Obsolete]
    public class BudgetComponentResourcePresenter<TItem, TComponentItem, TServices> : BudgetComponentItemPresenterBase<TItem,TComponentItem, TServices>,
        IBudgetComponentResourcePresenter<TItem, TComponentItem>
        where TItem : class, IPlannedResource
        where TComponentItem : class, IBudgetComponentItem
        where TServices : class, IBudgetComponentItemManagerApplicationServices<TItem, TComponentItem>
        
    {
        /// <summary>
        /// Gets or sets the Norm for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public decimal Norm
        {
            get
            {
                return Object.Norm;
            }
            set
            {
                SetProperty(v => Object.Norm = v, value);
                NotifyDown();
                NotifyUp();
            }
        }

      
    }
}
