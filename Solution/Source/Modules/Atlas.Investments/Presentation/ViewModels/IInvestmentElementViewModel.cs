using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    /// Contract for the crud view models beind bound to in the UI on controls allowing to handle the CRUD-operations
    /// regarding <see cref="IInvestmentElement"/> domain entities, wrapped in <see cref="IInvestmentElementPresenter"/>
    /// presenter view models. Note: that this crud view model can make CRUD-operations over the independent investment
    /// elements or over those being childrens of a specified one.
    /// </summary>
    //public interface IInvestmentElementViewModel : ICrudViewModel<IInvestmentElement, IInvestmentElementPresenter>
    //{
        

    //   // IPeriodPresenter Period { get; set; }
    //}
   
}
