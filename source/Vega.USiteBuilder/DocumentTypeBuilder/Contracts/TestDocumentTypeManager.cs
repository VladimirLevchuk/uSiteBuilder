using System;
using System.Collections.Generic;
using System.Linq;

namespace Vega.USiteBuilder.DocumentTypeBuilder.Contracts
{
    /// <summary>
    /// Default IDocumentTypeManager implementation
    /// </summary>
    public class TestDocumentTypeManager : ITestDocumentTypeManager
    {
        private USiteBuilder.DocumentTypeManager _manager;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TestDocumentTypeManager()
        {
            _manager = new Vega.USiteBuilder.DocumentTypeManager();
        }

        public virtual void SynchronizeDocumentTypes(IEnumerable<Type> types)
        {
            var syncTypes = types.ToList();

            _manager.ClearSyncData();

            // todo: make comparison more clever
            var newTypes = syncTypes.Select(x => new ContentComparison
            {
                DocumentTypeStatus = Status.New,
                Alias = GetAlias(x),
                ParentAlias = GetAlias(GetBaseType(x))
            }).ToList();

            _manager.RegisterChanges(newTypes);

            foreach (var type in syncTypes)
            {
                SynchronizeDocumentType(type);
            }
        }

        protected virtual string GetAlias(Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }

            return DocumentTypeManager.GetDocumentTypeAlias(type);
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
