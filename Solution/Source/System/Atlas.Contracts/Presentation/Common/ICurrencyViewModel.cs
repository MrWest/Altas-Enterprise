using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    /// <summary>
    /// defines an instance for a  Currency ViewModel
    /// </summary>
    public interface ICurrencyViewModel: IConvertibleEntityViewModel<ICurrency,ICurrencyPresenter>
    {
    }
}
