using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class DocumentDomainService<TDocument> : CodedNomenclatorDomainServicesBase<TDocument>, IDocumentDomainService<TDocument> 
        where TDocument : class, IDocument
    {
        public IEntity Holder { get; set; }

        /// <summary>
        ///     Creates a new instance of an Category.
        /// </summary>
        /// <returns>A new instance of type <see cref="ICategory" />.</returns>
        public override TDocument Create()
        {
            TDocument document = base.Create();
            document.Name = Resources.NewDocument_Name;
            document.Description = Resources.NewDocument_Description;

            return document;
        }
    }
}
