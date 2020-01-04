using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IUnderGroupActivityManagerApplicationServices : IUnderGroupItemManagerApplicationServices<IUnderGroupActivity>
    {
        IUnderGroupActivity AdquirePlannedProperties(IUnderGroupActivity onAdquiring, IPlannedActivity toAdquire);

        void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal price);
        void EditFromScratch(object Id, string name, string desc, string mu, string cu, decimal price);

        
    }
}
