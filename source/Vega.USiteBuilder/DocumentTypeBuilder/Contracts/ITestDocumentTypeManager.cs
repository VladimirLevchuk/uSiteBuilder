using System;
using System.Collections.Generic;

namespace Vega.USiteBuilder.DocumentTypeBuilder.Contracts
{
    /// <summary>
    /// Manages document types synchronization
    /// </summary>
    public interface ITestDocumentTypeManager
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
}