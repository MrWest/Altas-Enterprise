using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals
{
    /// <summary>
    /// represents an structure which can be walked througth its levels
    /// </summary>
    public interface ILeverable
    {
        int Level { get; }
    }
}
