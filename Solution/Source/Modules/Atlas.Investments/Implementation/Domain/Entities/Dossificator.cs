using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class Dossificator: InvestmentElement, IDossificator
    {
        public Dossificator()
        {
            var budget = ServiceLocator.Current.GetInstance<IBudget>();

           

            var equipment = ServiceLocator.Current.GetInstance<IEquipmentComponent>();

            equipment.Budget = budget;
            budget.EquipmentComponent = equipment;

            var construction = ServiceLocator.Current.GetInstance<IConstructionComponent>();

            construction.Budget = budget;
            budget.ConstructionComponent = construction;

            var otherExpenses = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();

            otherExpenses.Budget = budget;
            budget.OtherExpensesComponent = otherExpenses;

            var workCapital = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();

            workCapital.Budget = budget;
            budget.WorkCapitalComponent = workCapital;

            budget.InvestmentElement = this;
            this.Budget = budget;

            
        }
    }
}
