using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Presentation.Features
{
    public interface ICopyPasteable//<TEntity> where TEntity:class ,IEntity
    {
        IEntity CopiedObject { get; set; }
       
        void Paste();
    }
}