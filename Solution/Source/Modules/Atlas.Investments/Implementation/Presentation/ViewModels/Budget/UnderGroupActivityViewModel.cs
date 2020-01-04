using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class UnderGroupActivityViewModel: UnderGroupItemViewModel<IUnderGroupActivity, IUnderGroupActivityPresenter, IUnderGroupActivityManagerApplicationServices>, IUnderGroupActivityViewModel
    {
        protected override IUnderGroupActivityPresenter CreatePresenterFor(IUnderGroupActivity item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.UnderGroup = UnderGroup;
            return presenter;
        }

        protected override IUnderGroupActivityManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.UnderGroup = UnderGroup.Object;
            return service;
        }

        public void AddFromScratch(string code, string name, string desc, string MU, string cu, decimal Price)
        {
            ExecuteUsingServices(services =>
            {
                services.AddFromScratch( code,  name,  desc,  MU, cu, Price);
            });

            Load();
        }

        public void EditFromScratch(object Id, string name, string desc, string MU, string cu, decimal Price)
        {
            ExecuteUsingServices(services =>
            {
                services.EditFromScratch(Id, name, desc, MU, cu, Price);
            });

            
        }

        public IUnderGroupActivityPresenter CreatePresenter(IUnderGroupActivity activity)
        {
            return CreatePresenterFor(activity);
        }
    }
}