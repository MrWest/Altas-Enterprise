using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    /// <summary>
    /// Base contract of the repositories used to performs the data operations over activities of a certain budget component to ensure that
    /// their managed safely.
    /// </summary>
    /// <typeparam name="TItem">The budget component items which data operations will be handled here.</typeparam>
    /// <typeparam name="TComponent">The budget component to which they belong.</typeparam>
    public interface IExecutedActivityRepository : IActivityRepository<IExecutedActivity>
        //where TItem : class, IBudgetComponentItem
        //where TComponent : class, IBudgetComponent
    {
         /// <summary>
        /// Gets or sets the budget component to which belong the items which data operations are made in the current
        /// <see cref="IBudgetComponentItemRepository{T, TComponent}"/>.
        /// </summary>
      // TComponent Component { get; set; }
    }
  
}
