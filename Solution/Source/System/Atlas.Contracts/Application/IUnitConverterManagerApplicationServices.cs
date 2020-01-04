using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application
{
   public interface IUnitConverterManagerApplicationServices : IItemManagerApplicationServices<IUnitConverter>
   {
       IConvertibleEntity ConversionForEntity { get; set; }
    }
}
