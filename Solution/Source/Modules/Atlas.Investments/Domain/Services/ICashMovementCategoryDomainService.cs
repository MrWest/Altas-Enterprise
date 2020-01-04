using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface ICashMovementCategoryDomainService<TItem>: IDomainServices<TItem>
        where TItem:class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        ICashMovementCategory SuperiorCategory { get; set; }
    }
}
