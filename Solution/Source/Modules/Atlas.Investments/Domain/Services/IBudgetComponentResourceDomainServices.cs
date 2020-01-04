using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    /// Defines the main interface describing the resourses associeted to another budget component item 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponentItem"></typeparam>
   public interface IBudgetComponentResourceDomainServices<TComponentItem> : IBudgetComponentItemDomainServices<IPlannedResource>
        //where TItem : class, IBudgetComponentItem
        where TComponentItem : class , IBudgetComponentItem
    {
        TComponentItem Component { get; set; }
    }
}
