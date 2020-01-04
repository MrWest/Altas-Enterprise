using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// implements an execution for an executed activity
    /// </summary>
    public class Execution : EntityBase, IExecution
    {
        public Execution()
        {
            Date = DateTime.Today;
        }
        private IExecutedActivity _activity;
        [ForeignKey("ExecutedActivityId")]
        public IExecutedActivity ExecutedActivity { get { return _activity; } set { _activity = value; } }
        public DateTime Date { get; set; }
        public  decimal Amount { get; set; }
        public String Description { get; set; }
        public string ExecutedActivityId { get; set; }
    }
}
