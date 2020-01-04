using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface ISubSpecialityManagerApplicationServices : IItemManagerApplicationServices<ISubSpeciality>,IExportable<ISubSpeciality>
    {
        ISpeciality Speciality { get; set; }

        
    }
}
