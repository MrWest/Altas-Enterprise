using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface ISubSpecialityHolderPresenter<THolder,TComponent> : ISubSpecialityHolderPresenter<THolder>
        where TComponent: class ,IBudgetComponent
          where THolder : class, ISubSpecialityHolder
    {
        //ISubSpeciality SubSpeciality { get; }

        IBudgetComponentPresenter<TComponent> BudgetComponent { get; set; }
       
    }
    public interface ISubSpecialityHolderPresenter<THolder> : IPresenter<THolder>, ITreeNode, IPeriodCalculator
    {
        ISubSpeciality SubSpeciality { get;  }

        string SubSpecialityObject { get; set; }

        object Code { get;  }

        /// <summary>
        /// Get or sets the Category id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        ICategoryPresenter Category { get; set; }

        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        ISubExpenseConceptPresenter SubExpenseConcept { get; set; }

    }
}