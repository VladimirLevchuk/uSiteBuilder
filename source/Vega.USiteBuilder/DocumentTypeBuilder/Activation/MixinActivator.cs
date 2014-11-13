using System;
using umbraco.NodeFactory;

namespace Vega.USiteBuilder
{
    public class MixinActivator
    {
        static MixinActivator()
        {
            Current = new MixinActivator();
        }

        public static MixinActivator Current { get; set; }

        /// <summary>
        /// Creates an instance of mixin. 
        /// </summary>
        /// <param name="mixinType">Mixin type</param>
        /// <param name="node">node is necessary to lazy load values from (for virtual properties). </param>
        /// <returns></returns>
        public virtual MixinBase CreateInstance(Type mixinType, Node node)
        {
            var result = (MixinBase) DocumentTypeResolver.Instance.Activator.CreateInstance(mixinType);
            result.Source = node;
            return result;
        }
    }
}