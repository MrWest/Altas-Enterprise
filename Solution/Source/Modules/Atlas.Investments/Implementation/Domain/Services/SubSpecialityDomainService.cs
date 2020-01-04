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

    public class SubSpecialityDomainService : CodedNomenclatorDomainServicesBase<ISubSpeciality>, ISubSpecialityDomainService
    {
        public ISpeciality Speciality { get; set; }

        /// <summary>
        ///     Creates a new instance of an ExpenseConcept.
        /// </summary>
        /// <returns>A new instance of type <see cref="IExpenseConcept" />.</returns>
        public override ISubSpeciality Create()
        {
            ISubSpeciality phase = base.Create();
            phase.Name = Resources.NewSubSpeciality_Name;
            phase.Description = Resources.NewSubSpeciality_Description;

            return phase;
        }
    }
}
