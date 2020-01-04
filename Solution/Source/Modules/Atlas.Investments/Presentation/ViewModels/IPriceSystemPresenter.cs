using System;
using System.Collections.Generic;
using System.Linq;
using System.Notifications;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    /// incapsulates a PriceSystem  instance for presentation 
    /// </summary>
    public interface IPriceSystemPresenter : IPresenter<IPriceSystem>, IFiltrable,IExportable, IConfirmable, IBudgetComponentItemChangesSpreadder
    {
        IOverGroupViewModel OverGroups { get; }
        bool IsActive { get; set; }
        bool ExistOverGroup(string code);
        void AddFromScratch(string code, string name);
        IOverGroupPresenter GetOverGroup(string code);
    }
}
