using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class AtlasModuleGenericSubject : NomenclatorBase, IAtlasModuleGenericSubject
    {
        private IList<IAtlasModuleSubject> _subjects;
        public object Content { get; set; }

        public IList<IAtlasModuleSubject> Subjects
        {
            get
            {
                _subjects = _subjects ?? new List<IAtlasModuleSubject>();
                return _subjects;
            }
        }

        private IList<IDocument> _documents;

        /// <summary>
        /// Get or sets a list of Documents associated to this investment <see cref="IInvestment"/>.
        /// </summary>
        public IList<IDocument> Documents
        {
            get { return _documents ?? new List<IDocument>(); }
            set { _documents = value; }
        }
    }
}
