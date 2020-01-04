using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class OverGroup : PriceSystemGroup, IOverGroup
    {
        [ForeignKey("AbovePriceSystemId")]
        public IPriceSystem AbovePriceSystem { get; set; }
        public string AbovePriceSystemId { get; set; }
        public IList<IRegularGroup> RegularGroups { get; private set; }
        
    }
}
