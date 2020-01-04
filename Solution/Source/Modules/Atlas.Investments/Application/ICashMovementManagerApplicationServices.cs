using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface ICashMovementManagerApplicationServices:IItemManagerApplicationServices<ICashMovement>
    {
        ICashMovementCategory Category { get; set; }
   
    }
}
