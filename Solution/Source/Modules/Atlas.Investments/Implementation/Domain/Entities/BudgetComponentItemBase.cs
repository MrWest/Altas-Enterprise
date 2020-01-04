using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// This is the base class of all the items composing a budget component.
    /// </summary>
    public abstract class BudgetComponentItemBase : CodedNomenclatorBase, IBudgetComponentItem
    {
        protected decimal _unitarycost;

        public BudgetComponentItemBase()
        {
            PlannedResources = new List<IPlannedResource>();
            Period = ServiceLocator.Current.GetInstance<IPeriod>();
            LastCalculatedStartDate = Period.Starts;
            LastCalculatedFinishDate = Period.Ends;
            isUnitaryPriceCalculated = false;
            CalculatedUnitaryPrice = UnitaryCost;
            StartCalculated = false;
            EndCalculated = false;

        }

        /// <summary>
        /// Gets or sets the time interval (<see cref="IInvestmentElementPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        /// </summary>
        [ForeignKey("PeriodId")]
        public IPeriod Period { get; set; }

        //public abstract IEntity Component { get; set; }

        //  public IEntity Component { get; set; }

        /// <summary>
        /// Gets the list of planned resources composing the current <see cref="BudgetComponentBase"/>.
        /// </summary>
        public IList<IPlannedResource> PlannedResources { get; private set; }
        /// <summary>
        /// Get or sets an identifier to differentiate the current <see cref="BudgetComponentItemBase"/> with respect to others.
        /// </summary>
        public virtual string Code { get; set; }
        
        /// <summary>
        /// Get or sets the quantity of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual decimal Quantity { get; set; }

        /// <summary>
        /// Get or sets the Unitary Cost of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual decimal UnitaryCost {
            get
            {
                //if(!isUnitaryPriceCalculated)
                //  CalculatedUnitaryPrice = (PlannedResources.Count>0 && Quantity>0)?PlannedResources.Sum(x => CurrencyConvert(x))/Quantity:_unitarycost;
                //  isUnitaryPriceCalculated = true;
                return (PlannedResources.Count > 0 && Quantity > 0) ? PlannedResources.Sum(x => CurrencyConvert(x)) / Quantity : _unitarycost;
            }
            set
            {
                _unitarycost = value;
               
            }
        }
        /// <summary>
        /// convert the cost given <see cref="IBudgetComponentItem"/> to match the actual one based on the currency convert factor
        /// </summary>
        /// <param name="budgetComponentItem"></param>
        /// <returns></returns>
        protected decimal CurrencyConvert(IBudgetComponentItem budgetComponentItem)
        {
            var provider = ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<ICurrency>>();
            var foreingCurrency = provider.GetEntity(budgetComponentItem.Currency);
            var localCurrency = provider.GetEntity(Currency);

            // if there is a lack of values, dont do a thing
            if (foreingCurrency == null || localCurrency == null)
                return budgetComponentItem.Cost;

            // find the convertion
            var convertion =
                foreingCurrency.Convertions.SingleOrDefault(
                    x =>
                        x.ConversionForEntity.Id.ToString() == foreingCurrency.Id.ToString() &&
                        x.ConversionUnit.ToString() == localCurrency.Id.ToString());
            if (convertion == null)
                return budgetComponentItem.Cost;

            return budgetComponentItem.Cost * convertion.Factor;

        }
        /// <summary>
        /// Get or sets the Measurement of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// Get or sets the Currency of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Get or sets the Expense Concept of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public string SubExpenseConcept { get; set; }
        /// <summary>
        /// Get or sets the Cost of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
         public  decimal Cost { get { return Quantity * UnitaryCost; }}


        public bool Calculated { get; set; }

        public DateTime LastCalculatedFinishDate { get; set; }

        public DateTime LastCalculatedStartDate { get; set; }

        public string PriceSystem { get; set; }

        public bool IsCostCalculated { get; set; }
        public bool isUnitaryPriceCalculated { get; set; }
        public decimal CalculatedUnitaryPrice { get; set; }
        public string PeriodId { get; set; }
        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                LastCalculatedStartDate = Period.OriStart();
              
                StartCalculated = true;
            }

            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
                LastCalculatedFinishDate = Period.OriEnd();

                EndCalculated = true;
            }

            return LastCalculatedFinishDate;
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
    }
}
