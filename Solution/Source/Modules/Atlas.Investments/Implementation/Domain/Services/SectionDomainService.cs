using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implements some services used to enforce operations for price system domain
    ///     entities.
    /// </summary>
   public class SectionDomainService:DomainServicesBase<ISection>,ISectionDomainService
    {


        private IPriceSystem _aboveSection;


        /// <summary>
        ///     Gets or sets the <see cref="ISection" /> to which belong the <see cref="ISection" /> which
        ///     business rules are managed here.
        /// </summary>
        public IPriceSystem AboveSection
        {
            get
            {
                if (_aboveSection == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _aboveSection;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _aboveSection = value;
            }
        }
        /// <summary>
        ///     Creates a new instance of a Price System.
        /// </summary>
        /// <returns>A new instance of type <see cref="IPriceSystem" />.</returns>
       public override ISection Create()
        {
            ISection section = base.Create();
            section.Name = Resources.NewSection;

            return section;
        }
    }
}
