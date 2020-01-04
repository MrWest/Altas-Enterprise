using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    public interface IDocumentDomainService<TDocument> : IDomainServices<TDocument>
        where TDocument: class, IDocument
    {
        IEntity Holder { get; set; }
    }
}
