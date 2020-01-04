using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract for the presenter view models used to decorate and impersonate instances of the domain entity:
    ///     "ExpenseConcept" in the UI.
    /// </summary>
    public interface IExpenseConceptPresenter : IPresenter<IExpenseConcept>,IItemeable<ISubExpenseConceptPresenter>
    {
        string Id { get; set; }
        ISubExpenseConceptViewModel SubExpenseConcepts { get; }
        //ISubExpenseConceptPresenter SelectedItem { get; set; }

        
    }
}