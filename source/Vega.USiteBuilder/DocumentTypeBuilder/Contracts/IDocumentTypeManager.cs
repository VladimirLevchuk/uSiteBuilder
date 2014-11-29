using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.USiteBuilder.DocumentTypeBuilder.Contracts
{
    /// <summary>
    /// Manages document types synchronization
    /// </summary>
    public interface IDocumentTypeManager
    {
        /// <summary>
        /// Synchronizes types
        /// </summary>
        /// <param name="types"></param>
        void SynchronizeDocumentTypes(IEnumerable<Type> types);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        void SynchronizeDocumentType(Type type);
    }

    /// <summary>
    /// Default IDocumentTypeManager implementation
    /// </summary>
    public class DocumentTypeManager : IDocumentTypeManager
    {
        private USiteBuilder.DocumentTypeManager _manager;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DocumentTypeManager()
        {
            _manager = new Vega.USiteBuilder.DocumentTypeManager();
        }

        public virtual void SynchronizeDocumentTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                SynchronizeDocumentType(type);
            }
        }

        public virtual void SynchronizeDocumentType(Type type)
        {
            if (!type.IsAbstract)
            {
                _manager.SynchronizeDocumentType(type, GetBaseType(type));
                _manager.SynchronizeChildNodesFor(type);
            }

            var children = Util.GetFirstLevelSubTypes(type);
            if (children.Count > 0)
            {
                SynchronizeDocumentTypes(children);
            }
        }

        private Type GetBaseType(Type docType)
        {
            if (docType == typeof (DocumentTypeBase))
            {
                return null;
            }

            return docType.BaseType;
        }
    }
}
