using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents the implementation of the domain entity: "Investment element".
    /// </summary>
    public abstract class InvestmentElement : NomenclatorBase, IInvestmentElement
    {
        private IList<IInvestmentElement> _elements;
        private DateTime _lastCalculatedStartDate;
        private DateTime _lastCalculatedFinishDate;

        /// <summary>
        /// Initializes a new instance of <see cref="InvestmentElement"/>.
        /// </summary>
        public InvestmentElement()
        {
           // Elements = new List<IInvestmentElement>();

          Period = new Period();
            LastCalculatedStartDate = Period.Starts;
            LastCalculatedFinishDate = Period.Ends;
          
        }


        /// <summary>
        /// Gets or sets the parent investment element (<see cref="IInvestmentElement"/>) of the current one.
        /// </summary>
        public IList<IInvestmentElement> Elements {
            get
            {
                if(_elements==null)
                    _elements = new List<IInvestmentElement>();
                return _elements;
            }
            private set { _elements = value; } }

        /// <summary>
        /// Gets or sets the parent investment element (<see cref="IInvestmentElement"/>) of the current one.
        /// </summary>
        public IInvestmentElement Parent { get; set; }


        //INavigable INavigable.Parent { get; }

        /// <summary>
        /// Gets or sets the budget (<see cref="IBudget"/>) of the current <see cref="IInvestmentElement"/>.
        /// </summary>

        [ForeignKey("BudgetId")]
        public IBudget Budget { get; set; }
        /// <summary>
        /// Gets or sets the time period (<see cref="IPeriod"/>) of the current <see cref="IInvestmentElement"/>.
        /// </summary>
        [ForeignKey("PeriodId")]
        public IPeriod Period{ get; set; }

        /// <summary>
        ///     Gets or sets the code of the investment element.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the location of the investment element.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the constructor of the investment element.
        /// </summary>
        public string Constructor { get; set; }

        /// <summary>
        ///     Gets or sets the objective of the investment element.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        ///     Gets or sets the scope of the investment element.
        /// </summary>
        public string Scope { get; set; }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                LastCalculatedStartDate = Period.OriStart(); 
                bool first = true;

                foreach (IInvestmentElement investmentElement in Elements)
                {
                    if (first)
                    {
                        LastCalculatedStartDate = investmentElement.StartDate();
                        first = false;
                    }
                    else
                    {
                        if (LastCalculatedStartDate.CompareTo(investmentElement.StartDate()) > 0)
                            LastCalculatedStartDate = investmentElement.StartDate();
                    }
                }


                if (first)
                {
                    LastCalculatedStartDate = Budget.StartDate();
                    
                }
                else
                {
                    if (LastCalculatedStartDate.CompareTo(Budget.StartDate()) > 0)
                        LastCalculatedStartDate = Budget.StartDate();
                }

                StartCalculated = true;
            }

            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
                     LastCalculatedFinishDate = Period.OriEnd();
                bool first = true;
                    foreach (IInvestmentElement investmentElement in Elements)
                    {
                        if (first)
                        {
                            LastCalculatedFinishDate = investmentElement.FinishDate();
                            first = false;
                        }
                        else
                        {
                            if (LastCalculatedFinishDate.CompareTo(investmentElement.StartDate()) < 0)
                            LastCalculatedFinishDate = investmentElement.StartDate();
                        }
                    }


                    if (first)
                    {
                         LastCalculatedFinishDate = Budget.FinishDate();
                       // first = false;
                    }
                    else
                    {
                        if (LastCalculatedFinishDate.CompareTo(Budget.FinishDate()) < 0)
                        LastCalculatedFinishDate = Budget.FinishDate();
                    }

                EndCalculated = true;
            }
           

            return LastCalculatedFinishDate;
        }

       
        public DateTime LastCalculatedFinishDate
        {
            get { return _lastCalculatedFinishDate; }
            set { _lastCalculatedFinishDate = value; }
        }

        public DateTime LastCalculatedStartDate
        {
            get { return _lastCalculatedStartDate; }
            set { _lastCalculatedStartDate = value; }
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public string BudgetId { get; set; }
        public string PeriodId { get; set; }
       
    }
}
