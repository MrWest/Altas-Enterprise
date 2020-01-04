using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class AtlasGenericModuleAccess: NomenclatorBase,IAtlasGenericModuleAccess
    {
        private IList<IAtlasModuleRole> _rols;
        private IList<IAtlasModuleAccess> _ownedAccesses;
        public object User { get; set; }
        public IList<IAtlasModuleRole> Rols
        {
            get { return _rols ?? new List<IAtlasModuleRole>(); }
            set { _rols = value; }
        }

        public IList<IAtlasModuleAccess> OwnedAccesses
        {
            get { return _ownedAccesses ?? new List<IAtlasModuleAccess>(); }
            set { _ownedAccesses = value; }
        }

    }
}
