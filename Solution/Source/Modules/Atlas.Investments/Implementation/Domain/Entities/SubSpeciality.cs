using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class SubSpeciality : CodedNomenclatorBase, ISubSpeciality
    {
        public IEntity OwnerEntity { get; set; }
        public ISpeciality Speciality { get; set; }
        public string OwnerEntityId { get; set; }
        public string SpecialityId { get; set; }
    }
}
