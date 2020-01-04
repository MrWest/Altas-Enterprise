using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    
    public class UnderGroupResourceRepository: UnderGroupItemRepository<IUnderGroupResource>, IUnderGroupResourceRepository,
        IRelatedRepository<IUnderGroupResource, IWeight>, IRelatedRepository<IUnderGroupResource, IVolume>
    {
        public UnderGroupResourceRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Norm),
                    GetName(x => x.WasteCoefficient),
                    GetName(x => x.WageScale),
                    GetName(x => x.MenNumber),
                     GetName(x => x.ResourceKind)

                }).ToArray();
            }
        }

        public override IUnderGroupResource Add(IUnderGroupResource entity)
        {
            IUnderGroupResource item = base.Add(entity);

            entity.Weight = item.Weight;
            entity.Volume = item.Volume;

            //this.Relate(entity, UnderGroup, DatabaseContext);
            this.Relate(entity, entity.Weight, DatabaseContext);
            this.Relate(entity, entity.Volume, DatabaseContext);

           

            return entity;

        }

        protected override IUnderGroupResource Clone(IUnderGroupResource plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");

            IUnderGroupResource plannedResourceClone = base.Clone(plannedResource);

            if (plannedResource.Weight == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceWeight");
            if (plannedResource.Volume == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceVolume");

            plannedResourceClone.Weight = Clone(plannedResource.Weight);
          //  plannedResourceClone.Weight.Holder = plannedResourceClone;
            plannedResourceClone.Volume = Clone(plannedResource.Volume);
          //  plannedResourceClone.Volume.Holder = plannedResourceClone;


            return plannedResourceClone;
        }


        private IWeight Clone(IWeight weight)
        {
            var weightClone = ServiceLocator.Current.GetInstance<IWeight>();

            weightClone.MeasurementUnit = weightClone.MeasurementUnit;
            weightClone.Amount = weight.Amount;


            weightClone.Id = weight.Id ?? DatabaseContext.GenerateKey();

            return weightClone;
        }
        private IVolume Clone(IVolume volume)
        {
            var volumeClone = ServiceLocator.Current.GetInstance<IVolume>();

            volumeClone.MeasurementUnit = volumeClone.MeasurementUnit;
            volumeClone.Amount = volume.Amount;


            volumeClone.Id = volume.Id ?? DatabaseContext.GenerateKey();

            return volumeClone;
        }

        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(IUnderGroupResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (weight == null)
                throw new ArgumentNullException("weight");

            plannedResource.Weight = weight;
           // weight.Holder = plannedResource;
        }
        public void Relate(IUnderGroupResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (volume == null)
                throw new ArgumentNullException("weight");

            plannedResource.Volume = volume;
           // volume.Holder = plannedResource;
        }
        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(IUnderGroupResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (weight == null)
                throw new ArgumentNullException("weight");

            Delete(weight);

            plannedResource.Weight = null;
          //  weight.Holder = null;

        }
        public void Unrelate(IUnderGroupResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (volume == null)
                throw new ArgumentNullException("volume");

            Delete(volume);

            plannedResource.Volume = null;
          //  volume.Holder = null;

        }

        public void SaveReference(IWeight other)
        {
            //   DatabaseContext.Store(other);
        }
        public void SaveReference(IVolume other)
        {
            //   DatabaseContext.Store(other);
        }

      
    }
}