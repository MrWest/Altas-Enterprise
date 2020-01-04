using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Contract to be implemented by the domain services used to enforce operations for price system domain
    ///     entities.
    /// </summary>
   public interface IPriceSystemDomainService: IDomainServices<IPriceSystem>
    {

    }
}
