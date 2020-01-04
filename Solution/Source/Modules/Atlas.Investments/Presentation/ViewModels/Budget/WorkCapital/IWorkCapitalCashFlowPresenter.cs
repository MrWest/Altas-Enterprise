using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    /// describes the encapsulation for a <see cref="IWorkCapitalCashFlow"/>
    /// </summary>
    public interface IWorkCapitalCashFlowPresenter :  IPresenter<IWorkCapitalCashFlow>
    {
        IWorkCapitalComponentPresenter WorkCapitalComponent { get; set; }
        IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry> CashEntries { get; set; }
        IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing> CashOutgoings { get; set; }

        IList<ICashMovementCategoryPresenter<ICashEntry>> CashEntriesInDeep { get; }
        IList<ICashMovementCategoryPresenter<ICashOutgoing>> CashOutgoingsInDeep { get; }

        IList<ICashMovementCategoryPresenter<ICashEntry>> AllCashEntries { get; }

        IList<ICashMovementCategoryPresenter<ICashOutgoing>> AllCashOutgoings { get; }
        IList<ICashMovementCategoryPresenter<ICashOutgoing>> BuiltinCash { get; }
        
        IList<DataGridTextColumn> DataGridColumns { get; set; }

        IList<ICashMovementCategoryPresenter<ICashOutgoing>> AllCashMovements { get; }

        DateTimeScale DateTimeScale { get; set; }

        //bool ShowLiquity { get; set; }
        IPeriod Period { get; set; }
        DateTime Starts { get; set; }
        DateTime Ends { get; set; }
        DateTimeScale PeriodKind { get; set; }
        decimal WorkCapital { get; }
        string CashPeriod { get; }
        void TellYourFather();

        Style CellStyle { get; set; }
        bool isVisible { get; set; }

         bool isWorkCapitalCalculated { get; set; }
         decimal CalculatedWorkCapital { get; set; }
    }
}
