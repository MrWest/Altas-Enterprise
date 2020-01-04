using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Represents a base class  for entities who needs to be converted into other of the same kind.
    /// </summary>
    public interface IConvertibleEntity: INomenclator
    {
        IList<IUnitConverter> Convertions { get; set; } 
        String Letters { get; set; }
    }
}
