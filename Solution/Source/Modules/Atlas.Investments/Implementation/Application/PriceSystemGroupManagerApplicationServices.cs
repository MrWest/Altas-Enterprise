using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public abstract class PriceSystemGroupManagerApplicationServices<TGroup,TRepository,TDomain> : ItemManagerApplicationServicesBase<TGroup, TRepository,TDomain>, IPriceSystemGroupManagerApplicationServices<TGroup>
        where TGroup:class,IPriceSystemGroup
        where TRepository : class,IPriceSystemGroupRepository<TGroup>
        where TDomain : class,IPriceSystemGroupDomainService<TGroup>
    {
        public TGroup AddFromScratch(string code, string name)
        {
            var group = DomainServices.Create();
            group.Name = name;
            group.Code = code;

            return Repository.Add(group);
        }

       
    }
}
