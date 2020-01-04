using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IUnderGroupManagerApplicationServices:IPriceSystemGroupManagerApplicationServices<IUnderGroup>
    {
        IRegularGroup RegularGroup { get; set; }
        bool ExistActivity(string code, IUnderGroup underGroup);
        IUnderGroupActivity GetActivity(string code, IUnderGroup underGroup);
    }
}
