using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    public class PlannedResourceStub : IPlannedResource
    {
        public IBudgetComponentItem Component { get; set; }

        //public IExecutedBudgetComponentItem Execution { get; set; }

        public decimal Quantity { get; set; }

        public string Code { get; set; }

        public decimal UnitaryCost { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string FullName { get; set; }
        public decimal Cost { get; }
        public bool IsCostCalculated { get; set; }

        string ICurrenciable.Currency
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public IList<IPlannedResource> PlannedResources { get; }
        public string MeasurementUnit { get; set; }

        string IBudgetComponentItem.Currency
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string Category { get; set; }
        public string SubExpenseConcept { get; set; }
        public string PriceSystem { get; set; }
        public IPeriod Period { get; set; }
        public object Execution { get; set; }
        public decimal Norm { get; set; }
        public IWeight Weight { get; set; }
        public IVolume Volume { get; set; }
        public object WageScale { get; set; }
        public decimal WasteCoefficient { get; set; }
        public int MenNumber { get; set; }
        public ResourceKind ResourceKind { get; set; }
        public object Supplier { get; set; }
        public object Provider { get; set; }
        public bool isUnitaryPriceCalculated { get; set; }
        public decimal CalculatedUnitaryPrice { get; set; }
        public string PeriodId { get; set; }
        public string ComponentId { get; set; }
        public string WeightId { get; set; }
        public string VolumeId { get; set; }
        public DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
    }
}
