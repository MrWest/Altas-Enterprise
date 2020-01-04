using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IPriceSystemGroupManagerApplicationServices<TGroup> : IItemManagerApplicationServices<TGroup>
        where TGroup:class, IPriceSystemGroup
    {
        TGroup AddFromScratch(string code, string name);
       
    }
}
