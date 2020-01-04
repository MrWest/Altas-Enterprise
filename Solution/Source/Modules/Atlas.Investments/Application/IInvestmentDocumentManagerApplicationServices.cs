using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Contracts.Application.Common;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IInvestmentDocumentManagerApplicationServices : IDocumentManagerApplicationServices<IInvestmentDocument>
    {
        //IEntity Holder { get; set; }
    }
}
