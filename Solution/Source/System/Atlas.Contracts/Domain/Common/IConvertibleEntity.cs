using System;
using System.Collections.Generic;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// Represents a base class  for entities who needs to be converted into other of the same kind.
    /// </summary>
    public interface IConvertibleEntity : INomenclator, IOwnable
    {
        
        IList<IUnitConverter> Convertions { get; set; } 
        String Letters { get; set; }
    }
}
