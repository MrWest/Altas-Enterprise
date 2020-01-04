using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IUnderGroupActivityPresenter: IUnderGroupItemPresenter<IUnderGroupActivity>
    {
        
       // bool ExistPlannedResource(string code);
       // IPlannedResourcePresenter<IUnderGroupActivity> GetPlannedResource(string code);
       // void AddFromScratch(string code, string name, string desc, object muId, object cuId, decimal norm, decimal price, int kind, object muwId, decimal wv);
    }
}