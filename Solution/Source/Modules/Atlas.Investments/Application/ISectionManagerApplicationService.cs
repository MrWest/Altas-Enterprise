using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// Describes an application service for  Section entities
    /// </summary>
    public interface ISectionManagerApplicationService:IItemManagerApplicationServices<ISection>
    {
        /// <summary>
        /// Gets or sets the Section to which belong the section managed here.
        /// </summary>
        IPriceSystem AboveSection { get; set; }
    }
}
