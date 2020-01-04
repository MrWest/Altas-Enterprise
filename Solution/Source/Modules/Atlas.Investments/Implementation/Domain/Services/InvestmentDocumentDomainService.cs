using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class InvestmentDocumentDomainService : DocumentDomainService<IInvestmentDocument>, IInvestmentDocumentDomainService
    {
        public IEntity Holder { get; set; }

        /// <summary>
        ///     Creates a new instance of an Category.
        /// </summary>
        /// <returns>A new instance of type <see cref="ICategory" />.</returns>
        public override IInvestmentDocument Create()
        {
            IInvestmentDocument document = base.Create();
            //document.Name = Resources.NewDocument_Name;
            //document.Description = Resources.NewDocument_Description;
            document.RecieveDate = DateTime.Today;
            document.DeliverDate = DateTime.Today;
            return document;
        }
    }
}
