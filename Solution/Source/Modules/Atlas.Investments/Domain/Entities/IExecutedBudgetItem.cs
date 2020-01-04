using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IExecutedBudgetItem : IPlannedBudgetItem
    {
      

        decimal Planned { get; set; }
      
       
        decimal Quantity { get; }

        decimal EntriesQuantity { get; }
        decimal Existency { get; }

        decimal FinancialCost { get; }
        /// <summary>
        ///    Collection of LogEntries
        ///  </summary>
        ObservableCollection<ILogEntry> ExecutionLog { get; set; }
        ObservableCollection<ILogEntry> EntriesLog { get; set; }
        
       
    }
}
