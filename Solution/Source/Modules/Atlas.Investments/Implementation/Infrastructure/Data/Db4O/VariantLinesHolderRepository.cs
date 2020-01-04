using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class VariantLinesHolderRepository : Db4ORepositoryBase<IVariantLinesHolder>, IVariantLinesHolderRepository
    {
        public VariantLinesHolderRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }


}
