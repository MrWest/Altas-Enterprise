using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// represents an execution for an executed activity
    /// </summary>
    public interface IExecution: IEntity
    {
        IExecutedActivity ExecutedActivity { get; set; }
        string ExecutedActivityId { get; set; }
        DateTime Date { get; set; }
        decimal Amount { get; set; }
        String Description { get; set; }
    }
}
