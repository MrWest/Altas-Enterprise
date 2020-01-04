using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IInvestmentDocument : IDocument
    {
        ////IEntity Holder { get; set; }
        DocumentType DocumentType { get; set; }
        String Author { get; set; }
        String Institution { get; set; }
        object Osde { get; set; }
        object Oace { get; set; }
        DateTime RecieveDate { get; set; }
        DateTime DeliverDate { get; set; }
        //String FilePath { get; set; }
        //bool IsAviable();
        //void Open();
    }
}
