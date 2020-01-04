using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface IRegularGroupDomainService:IPriceSystemGroupDomainService<IRegularGroup>
    {
        IOverGroup OverGroup { get; set; }
    }
}
