using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IOverGroupPresenter:IPriceSystemGroupPresenter<IOverGroup>,IBudgetComponentItemChangesSpreadder
    {
        IPriceSystemPresenter PriceSystem { get; set; }
        IRegularGroupViewModel RegularGroups { get;  }

        bool ExistGroup(string code);
        IRegularGroupPresenter GetRegularGroup(string code);
        void AddFromScratch(string code, string name);
    }
}
