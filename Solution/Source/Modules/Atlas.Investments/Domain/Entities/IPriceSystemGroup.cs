using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// represents the definition for the price system groups
    /// </summary>
    public interface IPriceSystemGroup:ICodedNomenclator
    {
        string PriceSystem { get; set; }
    }
}
