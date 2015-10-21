namespace Infraestructure.Common.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using StructureMap;

    public static class KnownTypesHelper
    {
        public static IEnumerable<Type> GetKnownTypes<T>()
        {
            var instances = ObjectFactory.Container.Model.AllInstances.
                                                    Where(i => i.PluginType == typeof(T));

            return instances.Select(instanceRef => instanceRef.ConcreteType).ToList();
        }
    }
}
