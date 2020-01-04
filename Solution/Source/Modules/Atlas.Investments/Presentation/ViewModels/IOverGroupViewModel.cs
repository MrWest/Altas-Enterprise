using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{

    public interface IOverGroupViewModel:IPriceSystemGroupViewModel<IOverGroup,IOverGroupPresenter>
    {
        IPriceSystemPresenter PriceSystem { get; set; }
    }
}
