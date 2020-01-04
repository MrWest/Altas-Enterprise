using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    public class PlannedActivityStub : IPlannedActivity
    {
        public IBudgetComponent Component { get; set; }

        public object Execution { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitaryCost { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string FullName { get; set; }
        public decimal Cost { get; }
        public bool IsCostCalculated { get; set; }

        string ICurrenciable.Currency
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<IPlannedResource> PlannedResources { get; }
        public string MeasurementUnit { get; set; }

        string IBudgetComponentItem.Currency
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Category { get; set; }
        public string SubExpenseConcept { get; set; }
        public string PriceSystem { get; set; }
        public IPeriod Period { get; set; }
        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
        public object SubSpeciality { get; set; }
        public object Executor { get; set; }
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
        public bool isUnitaryPriceCalculated { get; set; }
        public decimal CalculatedUnitaryPrice { get; set; }
        public string PeriodId { get; set; }
        public string SubSpecialityHolderId { get; set; }
    }
}
