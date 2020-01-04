using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents the implementation of the domain entity: "Planned Resource"
    /// </summary>
    public class PlannedResource : PlannedBudgetComponentItemBase, IPlannedResource
    {
        private IBudgetComponentItem _componentItem;
        private decimal _quantity;

        public PlannedResource()
        {
           
        }
        /// <summary>
        /// Gets or sets the <see cref="IBudgetComponent"/> to which belong the current <see cref="IBudgetComponentItem"/>.
        /// </summary>
        public  IBudgetComponentItem Component
        {
            get
            {
                //if (_componentItem == null)
                //    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _componentItem;
            }
            set
            {
                //if (value == null)
                //    throw new ArgumentNullException("value");

                _componentItem = value;
            }
            
        }

        /// <summary>
        /// Get or sets the quantity of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public override decimal Quantity {
            get { return Norm == 0 || Component==null ? _quantity :  Component.Quantity * Norm; }
            set { _quantity = value; }
        }
        /// <summary>
        /// Get or sets the Unitary Cost of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public override decimal UnitaryCost
        {
            get
            {
                if (!isUnitaryPriceCalculated)
                {
                    
              
                if (ResourceKind == ResourceKind.MenLabor && WageScale != null)
                {
                    var provider =
                        ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<IWageScale>>();
                    var _wageScale = provider.GetEntity(WageScale);
                    if (_wageScale != null)
                    {
                          //  CalculatedUnitaryPrice = _wageScale.Retribution;
                            return _wageScale.Retribution;
                      }
                       
                }
                     _unitarycost = (PlannedResources.Count > 0 && Quantity > 0) ? PlannedResources.OfType<ICosttable>().Sum(x => x.Cost) / Quantity : _unitarycost;

                }

                return _unitarycost;
            }
            set { _unitarycost = value; }
        }
        /// <summary>
        /// Establish the norm for the current planned resource
        /// </summary>
        public decimal Norm { get; set; }

        [ForeignKey("WeightId")]
        public IWeight Weight { get; set; }

        [ForeignKey("VolumeId")]
        public IVolume Volume { get; set; }

        public object WageScale { get; set; }
        public decimal WasteCoefficient { get; set; }

        private int _menNumber;

        public int MenNumber
        {
            get
            {
                if (ResourceKind == ResourceKind.MenLabor && _menNumber < 1)
                    _menNumber = 1;
                return _menNumber;
            }
            set { _menNumber = value; }
        }

        public ResourceKind ResourceKind { get; set; }
        public object Supplier { get; set; }
        public object Provider { get; set; }

       
        public string ComponentId { get; set; }
        public string WeightId { get; set; }
        public string VolumeId { get; set; }
    }
}
