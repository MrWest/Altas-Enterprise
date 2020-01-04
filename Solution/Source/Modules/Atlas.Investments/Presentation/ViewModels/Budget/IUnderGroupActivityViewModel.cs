using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IUnderGroupActivityViewModel : IUnderGroupItemViewModel<IUnderGroupActivity, IUnderGroupActivityPresenter>
    {
        void AddFromScratch(string code, string name, string desc, string MU, string cu, decimal Price);
        void EditFromScratch(object Id, string name, string desc, string MU, string cu, decimal Price);
        IUnderGroupActivityPresenter CreatePresenter(IUnderGroupActivity activity);
    }
}
