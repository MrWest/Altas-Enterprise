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
    /// Implements a Price system features
    /// </summary>
    public class PriceSystem :NomenclatorBase, IPriceSystem
    {
        //private IList<ISection> _sections;
        ///// <summary>
        ///// Gets the list of Sections for the current <see cref="ISection"/>.
        ///// </summary>
        //public IList<ISection> Sections { get { return _sections ?? new List<ISection>() ; } }
        private IList<IOverGroup> _overGroups;
        /// <summary>
        /// Gets the list of Sections for the current <see cref="ISection"/>.
        /// </summary>
        public IList<IOverGroup> OverGroups { get { return _overGroups ?? new List<IOverGroup>(); } }

        public bool Calculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
