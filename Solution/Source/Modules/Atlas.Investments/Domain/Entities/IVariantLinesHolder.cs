using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Holds unused resourses an activities 
    /// </summary>
    
    //TODO analyze the component interpretation
    public interface IVariantLinesHolder : IBudgetComponentItem, IBudgetComponent
    {
        
        IEntity Component { get; set; }
        /// <summary>
        /// Gets the list of planned activities composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IPlannedActivity> PlannedActivities { get; }
        /// <summary>
        /// Gets the list of planned resources composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IPlannedResource> PlannedResources { get; }

        
    }
}
