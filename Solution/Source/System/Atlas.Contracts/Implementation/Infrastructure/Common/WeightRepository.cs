using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class WeightRepository : RepositoryBase<IWeight, IDb4ODatabaseContext>, IWeightRepository
    {
        public WeightRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IWeight> Entities
        {
            get
            {
                var specification = new WeightOfSpecification(Holder);

                return Where(specification);
            }
        }
        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id), GetName(x => x.MeasurementUnit), GetName(x => x.Amount) }; }
        }
        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(IWeight weight)
        {
            if (weight == null)
                throw new ArgumentNullException("Weight");

            base.Update(weight);


        }

        private IEntity _holder;
        public IEntity Holder { get { return _holder; } set { _holder = value; } }
    }
}
