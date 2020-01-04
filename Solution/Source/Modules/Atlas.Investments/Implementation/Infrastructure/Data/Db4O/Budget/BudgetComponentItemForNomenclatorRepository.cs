using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    //public class BudgetComponentItemForNomenclatorRepository<TItem>:NomenclatorRepository<TItem>, IBudgetComponentItemForNomenclatorRepository<TItem>
    //      where TItem : class, IBudgetComponentItem
    //{
    //    public BudgetComponentItemForNomenclatorRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
    //    {
    //    }

    //    public override IEnumerable<TItem> Entities
    //    {
    //        get
    //        {
    //            ISpecification<TItem> specification = new BudgetComponentItemForNomenclatorSpecification<TItem>(Text);
    //            return Where(specification).Take(MaxNumber);
    //        }
    //    }
    //}
}