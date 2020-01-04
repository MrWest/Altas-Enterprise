using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IUnderGroupPresenter: IPriceSystemGroupPresenter<IUnderGroup>, IBudgetComponentItemChangesSpreadder
    {
        IRegularGroupPresenter RegularGroup { get; set; }
        IUnderGroupActivityViewModel Activities { get; }
        IUnderGroupResourceViewModel Resources { get;  }

        //IPlannedActivityViewModel<IUnderGroup,IPlannedActivityPresenter<IUnderGroup>> VariantLines { get; }
        bool ExistActivity(string code);
        IUnderGroupActivityPresenter GetActivity(string code);
        void AddFromScratch(string code, string name, string desc, string muId, string cuId, decimal price);
    }
}
