using System;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IDocument : ICodedNomenclator
    {
        IEntity Holder { get; set; }
        string HolderId { get; set; }

        String Author { get; set; }
       
        String FilePath { get; set; }
        bool IsAviable();
        void Open();
    }
}
