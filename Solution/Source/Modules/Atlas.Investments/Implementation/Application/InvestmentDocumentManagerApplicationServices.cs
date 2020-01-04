using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Application.Common;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class InvestmentDocumentManagerApplicationServices : DocumentManagerApplicationServices<IInvestmentDocument, IInvestmentDocumentRepository, IInvestmentDocumentDomainService>, IInvestmentDocumentManagerApplicationServices
    {
        ////public IEntity Holder { get; set; }

        ////protected override IInvestmentDocumentRepository Repository
        ////{
        ////    get
        ////    {
        ////        var repo = base.Repository;
        ////        repo.Holder = Holder;
        ////        return repo;
        ////    }
        ////}

        ////protected override IInvestmentDocumentDomainService DomainServices
        ////{
        ////    get
        ////    {
        ////        var domain = base.DomainServices;
        ////        domain.Holder = Holder;
        ////        return domain;
        ////    }
        ////}
    }
}
