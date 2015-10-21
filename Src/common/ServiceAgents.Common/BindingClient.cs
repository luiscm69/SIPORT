using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ServiceAgents.Common
{
    public class BindingClient
    {
        public static string UrlDisponible(string nombreServicio)
        {
            IEnumerable<Binding> list = ObtenerDatos();

            var baseAddress = from adr in list
                              where adr.Estado == "A" && adr.Servicio == nombreServicio
                              select adr.Url;

            if (baseAddress.Count() > 0)
            {
                return baseAddress.FirstOrDefault();
            }
            else
            {
                throw new Exception("No se encuentra un BaseAddress para el Servicio :" + nombreServicio + ", Verifique el archivo Bindings.");
            }
        }

        private static IEnumerable<Binding> ObtenerDatos()
        {
            string filePath = ConfigurationManager.AppSettings["rutaArchivoBindings"];
            var list = new List<Binding>();

            if (!File.Exists(filePath))
            {
                throw new Exception("Archivo Bindings no encontrado en la ruta especificada.");
            }

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    var adr = new Binding
                    {
                        Servicio = columns[0],
                        Url = columns[1],
                        Estado = columns[2]
                    };
                    list.Add(adr);
                }
                if (list.Count() > 0)
                    return list;
                else
                    throw new Exception("Archivo Bindings no contiene BaseAddress.");
            }
        }

        private class Binding
        {
            public string Servicio { get; set; }
            public string Url { get; set; }
            public string Estado { get; set; }
        }
    }
}
