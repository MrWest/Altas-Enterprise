using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IRegularGroupManagerApplicationServices:IPriceSystemGroupManagerApplicationServices<IRegularGroup>
    {
        IOverGroup OverGroup { get; set; }
        bool ExistUnderGroup(string code, IRegularGroup regularGroup);
        IUnderGroup GetUnderGroup(string code, IRegularGroup regularGroup);
    }
}
