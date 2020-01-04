using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IOverGroupManagerApplicationServices:IPriceSystemGroupManagerApplicationServices<IOverGroup>
    {
        IPriceSystem PriceSystem { get; set; }
        bool ExistGroup(string code, IOverGroup overGroup);
        IRegularGroup GetRegularGroup(string code, IOverGroup overGroup);
    }
}
