using System;
using System.Diagnostics;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class Document : CodedNomenclatorBase, IDocument
    {
        public IEntity Holder { get; set; }
        public string HolderId { get; set; }

        public string FilePath { get; set; }
        public bool IsAviable()
        {
            return System.IO.File.Exists(FilePath);
        }

        public void Open()
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = FilePath;
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (System.Exception ex)
            {
                throw new Exception("FileNotFound");
            }
        }

        public string Author { get; set; }
    }
}
