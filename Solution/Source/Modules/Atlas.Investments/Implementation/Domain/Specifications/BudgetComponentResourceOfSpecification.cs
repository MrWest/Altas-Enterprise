using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    class BudgetComponentResourceOfSpecification <TItem, TComponent> : BudgetComponentItemsOfSpecification<TItem>
        where TItem:class,IPlannedResource
        where TComponent:class,IBudgetComponentItem
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemsOfSpecification{TItem}"/> given a budget component.
        /// </summary>
        /// <param name="component">The <see cref="IBudgetComponent"/> to get its items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
        public BudgetComponentResourceOfSpecification(TComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            Predicate = item => Equals(item.Component.Id, component.Id);
        }
    
    }

    public class BudgetComponentResourceOfQueryable<TItem,TComponent> : EntityFrameworkQueryable<TItem>
       where TComponent : class, IBudgetComponentItem
         where TItem : PlannedResource
    {

        public BudgetComponentResourceOfQueryable(TComponent parent, IEntityFrameworkDbContext<TItem> context) : base(context)
        {

            if (parent == null)
                throw new ArgumentNullException("parent");
            Query = (from e in context.Entities orderby e.Id ascending where e.ComponentId == parent.Id select e);
            Parameter = parent.Id;
            SQL = "SELECT [Extent1].[Id] AS[Id],[Extent1].[Quantity]AS[Quantity], [Extent1].[UnitaryCost] AS[UnitaryCost], [Extent1].[Norm] AS[Norm], [Extent1].[WasteCoefficient]"+
          "AS[WasteCoefficient], [Extent1].[MenNumber] AS[MenNumber], [Extent1].[ResourceKind] AS[ResourceKind], [Extent1].[ComponentId] AS[ComponentId], " +
    "[Extent1].[WeightId] AS[WeightId],  [Extent1].[VolumeId] AS[VolumeId],  [Extent1].[Code] AS[Code],  [Extent1].[MeasurementUnit] AS[MeasurementUnit],  [Extent1].[Currency]"+
        "AS[Currency], [Extent1].[Category] AS[Category],  [Extent1].[SubExpenseConcept] AS[SubExpenseConcept],  [Extent1].[Calculated] AS[Calculated],  [Extent1].[LastCalculatedFinishDate]"+
        "AS[LastCalculatedFinishDate],  [Extent1].[LastCalculatedStartDate] AS[LastCalculatedStartDate],  [Extent1].[PriceSystem] AS[PriceSystem],  [Extent1].[IsCostCalculated] AS[IsCostCalculated], "+
         "[Extent1].[isUnitaryPriceCalculated] AS[isUnitaryPriceCalculated],  [Extent1].[CalculatedUnitaryPrice] AS[CalculatedUnitaryPrice], [Extent1].[PeriodId] AS[PeriodId], [Extent1].[StartCalculated]"+
         "AS[StartCalculated],  [Extent1].[EndCalculated] AS[EndCalculated],  [Extent1].[Name] AS[Name], [Extent1].[Description] AS[Description] FROM[dbo].[PlannedResources] AS[Extent1]"+
          " WHERE [Extent1].[ComponentId] = '"+ parent.Id + "'";
        }
    }
}
