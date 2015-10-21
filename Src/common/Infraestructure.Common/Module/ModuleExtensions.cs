namespace Infraestructure.Common.Module
{
    using System;
    using System.Reflection;

    using StructureMap.Graph;

    public static class ModuleExtensions
    {
        public static Assembly LoadFromModule(this AppDomain appDomain, string assemblyName)
        {
            return appDomain.Load(assemblyName + "." + ModuleStorage.Current);
        }

        public static void AssemblyFromModule(this IAssemblyScanner scanner, string assemblyName)
        {
            scanner.Assembly(assemblyName + "." + ModuleStorage.Current);
        }
    }
}
