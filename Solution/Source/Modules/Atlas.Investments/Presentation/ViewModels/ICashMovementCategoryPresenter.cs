using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    /// Incapsulates a CashMovementcategory for presentation
    /// </summary>
    public interface ICashMovementCategoryPresenter<TItem> : IPresenter<TItem>, INomenclator,ICashMovementCategoryPresenter, ILeverable
        where TItem:class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        ICashMovementCategoryPresenter<TItem> SuperiorCategory { get; set; }
        ICashMovementCategoryViewModel<TItem> SubCategories { get; set; }

        ICashMovementViewModel<TItem> Movements { get; }

        IList<ICashMovement> AllCashMovements { get; }
        IList<ICashMovement> MovementsList { get; set; }

        ICommand AddCommand { get; }
        ICommand DeleteCommand { get; }

        ICashMovementCategoryPresenter<TItem> MySelf { get; }

        //ICashMovementViewModel<ICashMovement> Movements { get; }
        bool IsExpanded { get; set; }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="IInvestmentElementPresenter" /> name.
        /// </summary>
        string ShortName { get; set; }

        bool HasSons { get; }

        

        decimal TotalMovements { get; }
        //bool ShowLiquity { get; set; }

        

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="IInvestmentElementPresenter" /> according to its depth.
        /// </summary>
        string IconData { get; }

        bool IsSelected { get; set; }
        bool IsMouseOver { get; set; }

        void InDeepList<TItem>(ref IList<ICashMovementCategoryPresenter<TItem>> list) where TItem:class ,ICashMovementCategory;
       
        void SetMovementsList(IList<ICashMovement> list );

    }

    public interface ICashMovementCategoryPresenter
    {
        /// <summary>
        /// Set a CashMovement Value on the given <see cref="IPeriod"/> for the current <see cref="ICashMovementCategoryPresenter"/>
        /// </summary>
        void SetCashMovement(decimal value, IPeriod period);
        void TellYourFather();
        void TellYourSelf();
    }
}
