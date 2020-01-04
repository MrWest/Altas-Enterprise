using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class RegularGroup : PriceSystemGroup, IRegularGroup
    {
        [ForeignKey("OverGroupId")]
        public IOverGroup OverGroup { get; set; }
        public IList<IUnderGroup> UnderGroups { get; private set; }
        public string OverGroupId { get; set; }
    }
}
