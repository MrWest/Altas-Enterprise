using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    public interface ICashMovementCategoryRepository<TItem>:IRelatedRepository<TItem,ICashMovementCategory>
        where TItem:class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        ICashMovementCategory SuperiorCategory { get; set; }
    }
}
