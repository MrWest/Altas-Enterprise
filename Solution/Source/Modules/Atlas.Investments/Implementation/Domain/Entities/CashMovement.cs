using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Implements a cash entry or outgoing over placed in time
    /// </summary>
   public class CashMovement : EntityBase, ICashMovement
    {
       public CashMovement()
       {
           Date = DateTime.Today;
       }

        public ICashMovementCategory CashMovementCategory { get; set; }
        public DateTime Date { get; set; }
       public decimal Amount { get; set; }
        public string CashMovementCategoryId { get; set; }
    }
}
