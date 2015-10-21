namespace Infraestructure.Common.Module
{
    using System.Configuration;
    using System.Reflection;
    using System;

    public class ModuleStorage
    {
        private static string moduleName;

        public static void RegisterCurrentModuleFromCallingAssembly()
        {
            var assembly = Assembly.GetCallingAssembly();
            string name = assembly.GetName().Name;
            moduleName = name.Substring(0, name.LastIndexOf("."));
        }

        public static void RegisterCurrentModuleFromConfigFile()
        {
            moduleName = ConfigurationManager.AppSettings["ModuleName"];
        }

        public static string Current
        {
            get
            {
                if (moduleName == null)
                    throw new Exception("No se ha registrado el nombre del modulo actual");
                return moduleName;
            }
        }

        public static void RegisterCurrentModule(string name)
        {
            moduleName = name;
        }
    }
}