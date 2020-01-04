using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    public class WorkCapitalCashFlowCashMovementCategoryPresenter<TCategory> : CashMovementCategoryPresenter<TCategory>, IWorkCapitalCashFlowCashMovementCategoryPresenter<TCategory>
        where TCategory:class , ICashMovementCategory
    {
        //private bool _showLiquity;
        public IWorkCapitalCashFlowPresenter WorkCapitalCashFlowPresenter { get; set; }
        //public override bool ShowLiquity
        //{
        //    get { return _showLiquity; }
        //    set { _showLiquity = value; }
        //}

        public override void Notify()
        {
            WorkCapitalCashFlowPresenter.isWorkCapitalCalculated = false;
            WorkCapitalCashFlowPresenter.Notify();

        }

        public override int Level
        {
            get { return -1; }
        }

        public override void TellYourFather()
        {
            OnPropertyChanged(() => MovementsList);
            WorkCapitalCashFlowPresenter.TellYourFather();
        }
    }
}
