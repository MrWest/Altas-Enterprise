using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IBudgetComponentResourceManagerApplicationServices<TComponent> : IBudgetComponentItemManagerApplicationServices<IPlannedResource>
        //where TItem : class ,IPlannedResource
        where TComponent: class ,IBudgetComponentItem
    {
        ///// <summary>
        /////     Gets or sets the budget component to which belong the items which business rules are enforced in the current
        /////     <see cref="IBudgetComponentItemDomainServices{TItem}" />.
        ///// </summary>
        TComponent Component { get; set; }
        DateTime FinishDate(IPlannedResource plannedResource);
        void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv);
        void EditFromScratch(object id, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv);
        bool ExistPlannedResource(string code);
        IPlannedResource GetPlannedResource<TComponent>(string code);
    }
}
