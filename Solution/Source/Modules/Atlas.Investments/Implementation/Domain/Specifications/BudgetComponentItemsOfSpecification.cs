using System;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    /// <summary>
    /// Specification with the predicate empoyed to get the items of a budget component.
    /// </summary>
    public class BudgetComponentItemsOfSpecification<TItem> : Specification<TItem>
        where TItem : IBudgetComponentItem
    {
        
    }
}
