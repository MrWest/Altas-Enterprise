using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class ExecutedActivityStub : IExecutedActivity
    {
        private string _name, _code, _description;
        private decimal _quantity;


        public IBudgetComponent Component { get; set; }

        public IPlannedBudgetComponentItem Planification { get; set; }

        public string Name
        {
            get { return Planification != null ? Planification.Name : _name; }
            set
            {
                if (Planification == null)
                    _name = value;
            }
        }

        public string Description
        {
            get { return Planification != null ? Planification.Description : _description; }
            set
            {
                if (Planification == null)
                    _description = value;
            }
        }

        public string Code
        {
            get { return Planification != null ? Planification.Code : _code; }
            set
            {
                if (Planification == null)
                    _code = value;
            }
        }

        public decimal Quantity
        {
            get { return Planification != null ? Planification.Quantity : _quantity; }
            set
            {
                if (Planification == null)
                    _quantity = value;
            }
        }

        public object Id { get; set; }

        public string FullName { get; set; }
    }
}
