using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using Sharpen.IO;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class InvestmentDocument : Document, IInvestmentDocument
    {
        //public IEntity Holder { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Institution { get; set; }
        public object Osde { get; set; }
        public object Oace { get; set; }
        public DateTime RecieveDate { get; set; }
        public DateTime DeliverDate { get; set; }
        //public string FilePath { get; set; }
        //public bool IsAviable()
        //{
        //    return System.IO.File.Exists(FilePath);
        //}

        //public void Open()
        //{
        //    try
        //    {
        //        Process myProcess = new Process();
        //        myProcess.StartInfo.FileName = FilePath;
        //        myProcess.StartInfo.Verb = "Open";
        //        myProcess.StartInfo.CreateNoWindow = true;
        //        myProcess.Start();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw new Exception(Resources.FileNotFound);
        //    }
        //}

        //public string Author { get; set; }
    }
}
