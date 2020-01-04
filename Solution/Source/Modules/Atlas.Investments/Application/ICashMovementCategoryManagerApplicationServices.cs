using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface ICashMovementCategoryManagerApplicationServices<TItem> : IItemManagerApplicationServices<TItem>
        where TItem : class,ICashMovementCategory
    {
        ICashMovementCategory SuperiorCategory { get; set; }
        /// <summary>
        /// Set a CashMovement Value on the given <see cref="IPeriod"/> for the current <see cref="ICashMovementCategoryPresenter"/>
        /// </summary>
        void SetCashMovement(decimal value, IPeriod period, ICashMovementCategory cashMovementCategory);
    }
}
