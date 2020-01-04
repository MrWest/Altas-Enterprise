using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    public interface IRegularGroupRepository:IPriceSystemGroupRepository<IRegularGroup>
    {
        IOverGroup OverGroup { get; set; }
    }
}
