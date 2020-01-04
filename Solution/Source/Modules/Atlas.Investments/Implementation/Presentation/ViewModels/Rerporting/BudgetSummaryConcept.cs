using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class BudgetSummaryConcept : IBudgetSummaryConcept
    {
        private IBudgetSummary _thousandsOfMoney;
        private bool _background = false;

        public string ConceptName { get; set; }

        public IBudgetSummary ThousandsOfMoney
        {
            get
            {
                return _thousandsOfMoney;
            }
            set { _thousandsOfMoney = value; }
        }

        public bool Background
        {
            get { return _background; } 
            set { _background = value; }
        }

        //public IPeriod Period { get; set; }
    }
}
