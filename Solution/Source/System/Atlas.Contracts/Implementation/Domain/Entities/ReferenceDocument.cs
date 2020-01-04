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
using Sharpen.IO;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class ReferenceDocument : Document, IReferenceDocument
    {
        //public IEntity Holder { get; set; }
        public string KeyWords{get;set;}

        public DateTime PublishDate
        {
            get;
            set;
        }
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
