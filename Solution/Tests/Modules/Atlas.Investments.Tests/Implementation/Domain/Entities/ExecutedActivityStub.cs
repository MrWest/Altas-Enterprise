using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
{
    public class ExecutedActivityStub : IExecutedActivity
    {
        private string _name, _code, _description;
        private decimal _quantity;


        //public IBudgetComponent Component { get; set; }

        public object Planification { get; set; }

        //public string Name
        //{
        //    get { return Planification != null ? Planification.ToString() : _name; }
        //    set
        //    {
        //        if (Planification == null)
        //            _name = value;
        //    }
        //}

        //public string Description
        //{
        //    get { return Planification != null ? Planification.Description : _description; }
        //    set
        //    {
        //        if (Planification == null)
        //            _description = value;
        //    }
        //}

        //public string Code
        //{
        //    get { return Planification != null ? Planification.Code : _code; }
        //    set
        //    {
        //        if (Planification == null)
        //            _code = value;
        //    }
        //}

        //public decimal Quantity
        //{
        //    get { return Planification != null ? Planification.Quantity : _quantity; }
        //    set
        //    {
        //        if (Planification == null)
        //            _quantity = value;
        //    }
        //}

        public string Id { get; set; }

        public string FullName { get; set; }
        public decimal UnitaryCost { get; set; }
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
        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
        public object SubSpeciality { get; set; }
        public object Executor { get; set; }
        public decimal ExecutedQuantity { get; }
        public IList<IExecution> ExecutionLog { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Quantity { get; set; }
        public bool isUnitaryPriceCalculated { get; set; }
        public decimal CalculatedUnitaryPrice { get; set; }
        public string PeriodId { get; set; }
        public string SubSpecialityHolderId { get; set; }
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
