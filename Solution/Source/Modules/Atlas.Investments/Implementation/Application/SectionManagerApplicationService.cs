using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Implements an application service for  Section entities
    /// </summary>
    public class SectionManagerApplicationService:ItemManagerApplicationServicesBase<ISection,ISectionRepository,ISectionDomainService>,ISectionManagerApplicationService
    {
        private IPriceSystem _aboveSection;
        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override ISectionRepository Repository
        {
            get
            {
                ISectionRepository repository = base.Repository;
                repository.AboveSection = AboveSection;

                return repository;
            }
        }

        /// <summary>
        /// Gets or sets the Section to which belong the section managed here.
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
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override ISectionDomainService DomainServices
        {
            get
            {
                ISectionDomainService domainServices = base.DomainServices;
                domainServices.AboveSection = AboveSection;

                return domainServices;
            }
        }

    }
}
