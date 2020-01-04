using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
  public  interface IUnitConverterRepository:IRelatedRepository<IUnitConverter,IConvertibleEntity>
    {
        IConvertibleEntity ConversionForEntity { get; set; }
    }
}
