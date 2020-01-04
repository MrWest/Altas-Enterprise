using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{

    /// <summary>
    /// Describes an application service for  Price system entities
    /// </summary>
    public interface IPriceSystemManagerApplicationService: IItemManagerApplicationServices<IPriceSystem>
    {
        bool Export(IDatabaseContext databaseContext, IPriceSystem priceSystem);
        bool ExistOverGroup(string code, IPriceSystem priceSystem);
        void AddFromScratch(string code, string name, IPriceSystem priceSystem);
        IOverGroup GetOverGroup(string code, IPriceSystem priceSystem);
    }
}
