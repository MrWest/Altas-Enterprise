using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;
using Db4objects.Db4o.Internal.Query.Result;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Implements a cash movement in or out for the cash flow
    /// </summary>
    public class CashMovementCategory : NomenclatorBase, ICashMovementCategory
    {

        private IList<ICashMovementCategory> _subCategories;

        private IList<ICashMovement> _cashOverTimes;
        public string SuperiorCategoryId { get; set; }

        public IEntity SuperiorCategory { get; set; }

        /// <summary>
        /// sub cathegories en cash movements
        /// </summary>
        public virtual IList<ICashMovementCategory> SubCategories
        {
            get { return _subCategories ?? new List<ICashMovementCategory>(); }
            set { _subCategories = value; }
        }

        /// <summary>
        /// Movements of cash throw time
        /// </summary>
        public IList<ICashMovement> Movements
        {
            get { return _cashOverTimes ?? new List<ICashMovement>(); }
            set { _cashOverTimes = value; }
        }

    }
}
