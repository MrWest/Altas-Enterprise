using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
   public class AtlasModuleRoleOfSpecification : Specification<IAtlasModuleRole>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleRoleOfSpecification(IAtlasGenericModuleAccess ownerModuleAccess)
        {
            if (ownerModuleAccess == null)
                throw new ArgumentNullException("ownerModuleAccess");

            Predicate = acces => acces.ModuleAccess != null && Equals(acces.ModuleAccess.Id, ownerModuleAccess.Id);
        }
    }
}
