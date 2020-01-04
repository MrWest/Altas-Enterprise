using System;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
   

    /// <summary>
        /// Implementation of the domain entity: "Budget". Represents the budget of a certain investment element.
        /// </summary>
        public class Budget : EntityBase, IBudget
    {

        public Budget()
        {
            LastCalculatedFinishDate = DateTime.Today;
            LastCalculatedStartDate = DateTime.Today;
        }
        /// <summary>
        /// Gets the construction and setup component of the current <see cref="Budget"/>.
        /// </summary>
        [ForeignKey("ConstructionComponentId")]
        public IConstructionComponent ConstructionComponent { get; set; }

        /// <summary>
        /// Gets the equipment component of the current <see cref="Budget"/>.
        /// </summary>
        [ForeignKey("EquipmentComponentId")]
        public IEquipmentComponent EquipmentComponent { get; set; }

        /// <summary>
        /// Gets the other expenses component of the current <see cref="Budget"/>.
        /// </summary>
        [ForeignKey("OtherExpensesComponentId")]
        public IOtherExpensesComponent OtherExpensesComponent { get; set; }

        /// <summary>
        /// Gets the other work capital component of the current <see cref="Budget"/>.
        /// </summary>
        [ForeignKey("WorkCapitalComponentId")]
        public IWorkCapitalComponent WorkCapitalComponent { get; set; }

        /// <summary>
        /// Gets the investment element to which belong the current <see cref="Budget"/>.
        /// </summary>
        public IInvestmentElement InvestmentElement { get; set; }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                if (EquipmentComponent == null || ConstructionComponent == null || OtherExpensesComponent == null ||
                    WorkCapitalComponent == null)
                    return DateTime.Today;

                LastCalculatedStartDate = EquipmentComponent.StartDate();

                if (LastCalculatedStartDate.CompareTo(ConstructionComponent.StartDate()) > 0)
                    LastCalculatedStartDate = ConstructionComponent.StartDate();
                if (LastCalculatedStartDate.CompareTo(OtherExpensesComponent.StartDate()) > 0)
                    LastCalculatedStartDate = OtherExpensesComponent.StartDate();
                if (LastCalculatedStartDate.CompareTo(WorkCapitalComponent.StartDate()) > 0)
                    LastCalculatedStartDate = WorkCapitalComponent.StartDate();

                StartCalculated = true;
            }

            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
                if (EquipmentComponent == null || ConstructionComponent == null || OtherExpensesComponent == null ||
                WorkCapitalComponent == null)
                    return DateTime.Today;
                LastCalculatedFinishDate = EquipmentComponent.FinishDate();

                if (LastCalculatedFinishDate.CompareTo(ConstructionComponent.FinishDate()) < 0)
                    LastCalculatedFinishDate = ConstructionComponent.StartDate();
                if (LastCalculatedFinishDate.CompareTo(OtherExpensesComponent.FinishDate()) < 0)
                    LastCalculatedFinishDate = OtherExpensesComponent.StartDate();
                if (LastCalculatedFinishDate.CompareTo(WorkCapitalComponent.FinishDate()) < 0)
                    LastCalculatedFinishDate = WorkCapitalComponent.StartDate();

                EndCalculated = true;

                
            }

            return LastCalculatedFinishDate;
        }

        
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public string EquipmentComponentId { get; set; }
        public string ConstructionComponentId { get; set; }
        public string OtherExpensesComponentId { get; set; }
        public string WorkCapitalComponentId { get; set; }
    }
}
