using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IRegularGroupPresenter : IPriceSystemGroupPresenter<IRegularGroup>, IBudgetComponentItemChangesSpreadder
    {
        IOverGroupPresenter OverGroup { get; set; }
        IUnderGroupViewModel UnderGroups { get; }
        bool ExistUnderGroup(string code);
        IUnderGroupPresenter GetUnderGroup(string code);
        void AddFromScratch(string code, string name);
    }
}
