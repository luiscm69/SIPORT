namespace Componentes.Common.Utilidades
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Componentes.Common.CustomAttributes;

    using NPOI.SS.UserModel;
    using NPOI.HSSF.UserModel;

    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using Componentes.Common.Utilidades.CellStyleFactory;
    //
    using log4net;
    using System.Diagnostics;

    public static class Utilidades
    {
        #region Utilidades de Exportación

        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static List<Estilos> estilosHistoricos;

        public static MemoryStream Exportar<T>(IEnumerable<T> datos)
        {
            return Exportar<T>(datos, TipoExportacion.ExcelCabeceras);
        }

        public static MemoryStream Exportar<T>(IEnumerable<T> datos, TipoExportacion tipoExportacion)
        {
            return Exportar<T>(datos, tipoExportacion, null);
        }

        public static MemoryStream Exportar<T>(IEnumerable<T> datos, TipoExportacion tipoExportacion, string grupo)
        {
            //Iniciamos el reloj, Calculamos la memoria inicial, Escribimos el inicio del proceso 
            Stopwatch sw = Stopwatch.StartNew();
            long totalMemoriaInicial = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            log.Debug(string.Format("Inicio:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));

            //Validamos la existencia del parametro datos
            if (datos == null) throw new ArgumentException("No hay datos para exportar.", "datos");

            //Inicializamos los estilos de la exportación
            estilosHistoricos = new List<Estilos>();    

            //Asignamos un valor por defecto del parametro grupo
            string valorgrupo = string.IsNullOrWhiteSpace(grupo) ? ExportAttribute.GRUPO_DEFAULT : grupo;

            var workbook = CrearLibro<T>(datos, tipoExportacion, valorgrupo);
            log.Debug(string.Format("CrearLibro:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));
            var output = new MemoryStream();
            workbook.Write(output);
            log.Debug(string.Format("WriteOutput:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));

            //
            long totalMemoriaFinal = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            GC.Collect();
            long totalMemoriaReducida = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            log.Debug(string.Format("Fin:{0}|{1}|{2}", totalMemoriaFinal.ToString(), totalMemoriaReducida.ToString(), sw.Elapsed.ToString()));
            return output;
        }

        public static MemoryStream Exportar<T, U>(IEnumerable<T> datos, IEnumerable<U> totales)
        {
            //Validamos la existencia del parametro datos
            if (datos == null) throw new ArgumentException("No hay datos para exportar.", "datos");
            return Exportar<T, U>(datos, totales, TipoExportacion.ExcelSimple, null);
        }

        public static MemoryStream Exportar<T, U>(IEnumerable<T> datos, IEnumerable<U> totales, TipoExportacion tipoExportacion, string grupo)
        {
            //Iniciamos el reloj, Calculamos la memoria inicial, Escribimos el inicio del proceso 
            Stopwatch sw = Stopwatch.StartNew();
            long totalMemoriaInicial = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            log.Debug(string.Format("Inicio:{0}|{1}", totalMemoriaInicial.ToString(),sw.Elapsed.ToString()));

            //Inicializamos los estilos de la exportación
            estilosHistoricos = new List<Estilos>();

            //Validamos la existencia del parametro datos
            if (datos == null) throw new ArgumentException("No hay datos para exportar.", "datos");

            //Asignamos un valor por defecto del parametro grupo
            string valorgrupo = string.IsNullOrWhiteSpace(grupo) ? ExportAttribute.GRUPO_DEFAULT : grupo;

            var workbook = CrearLibro<T>(datos, tipoExportacion, valorgrupo);
            log.Debug(string.Format("CrearLibro:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));
            workbook = AgregarDatos<U>(workbook, totales, tipoExportacion, valorgrupo);
            log.Debug(string.Format("AgregarDatos:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));
            var output = new MemoryStream();
            workbook.Write(output);
            log.Debug(string.Format("WriteOutput:{0}|{1}", totalMemoriaInicial.ToString(), sw.Elapsed.ToString()));

            //
            long totalMemoriaFinal = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            GC.Collect();
            long totalMemoriaReducida = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
            log.Debug(string.Format("Fin:{0}|{1}|{2}",totalMemoriaFinal.ToString(), totalMemoriaReducida.ToString(),sw.Elapsed.ToString()));

            return output;
        }

        private class ExportarColumnasInfo
        {
            public string Campo { get; set; }
            public PropertyInfo Propiedad { get; set; }
            public int Orden { get; set; }
            public int Tamanio { get; set; }
            public string Cabecera { get; set; }
            public string Formato { get; set; }
            public string Alineamiento { get; set; } 
            public string[] Grupo { get; set; }
        }

        private static List<ExportarColumnasInfo> LeerAtributos(Type tipoDTO)
        {
            //Inicializamos la lista de columnas a exportar
            var columnasExportar = new List<ExportarColumnasInfo>();

            //Iteramos entre las columnas del DTO para obtener la información de los atributos personalizados
            foreach (var infoMiembro in tipoDTO.GetMembers())
            {
                //Filtramos los campos o columnas del DTO
                if (infoMiembro.MemberType == MemberTypes.Property)
                {
                    var Campo = (PropertyInfo)infoMiembro;
                    var NombreClase = (infoMiembro).Name;

                    if (tipoDTO.GetProperty(NombreClase) != null)
                    {
                        //Obtenemos sus atributos de tipo Export
                        ExportAttribute Atributos = (ExportAttribute)tipoDTO.GetProperty(NombreClase).GetCustomAttributes(typeof(ExportAttribute), true).SingleOrDefault();

                        //Es caso el campo o columna este decorado con el atributo creamos una linea más en las columnas a exportar
                        if (Atributos != null)
                        {
                            if (Atributos.Cabecera != null)
                            {
                                columnasExportar.Add(
                                    new ExportarColumnasInfo()
                                    {
                                        Campo = NombreClase,
                                        Propiedad = Campo,
                                        Orden = Atributos.Orden,
                                        Tamanio = Atributos.Tamanio,
                                        Cabecera = Atributos.Cabecera,
                                        Formato = Atributos.Formato,
                                        Grupo = Atributos.Grupo,
                                        Alineamiento = Atributos.Alineamiento
                                    });
                            }
                        }

                    }
                }
            }
            return columnasExportar;
        }

        private static IWorkbook CrearLibro<T>(IEnumerable<T> datos, TipoExportacion tipoExportacion, string grupo)
        {
            //Inicializamos el archivo excel
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("DatosExportados");

            //Inmoviliza la cabecera del reporte
            sheet.CreateFreezePane(0, 1, 0, 1);

            workbook = AgregarDatos<T>(workbook, datos, tipoExportacion, grupo);

            return workbook;
        }

        private static IWorkbook AgregarDatos<T>(IWorkbook wb, IEnumerable<T> datos, TipoExportacion tipoexportacion, string grupo)
        {
            //Recuperar la hoja de exportación - Se asume que siempre trabajamos con la primera hoja
            ISheet sheet = wb.GetSheetAt(0);
            IRow fila;
            ICell celda;
            FabricaEstilos fabricaEstilos = new FabricaEstilos(estilosHistoricos);

            //Obtener la siguiente fila disponible, sino es la primera dejar dos espacios al medio
            var ultimaFila = sheet.LastRowNum;
            ultimaFila = ultimaFila != 0 ? ultimaFila + 2 : 0;

            //Inicializamos la lista de columnas a exportar
            var columnasExportar = LeerAtributos(typeof(T));

            //En caso haya columnas para exportar continuamos
            if (columnasExportar.Any())
            {
                //Inicializamos el ancho de las columnas solo si es la primera exportación de datos
                if (ultimaFila == 0)
                {
                    foreach (var item in columnasExportar)
                    {
                        if (item.Grupo.Contains(grupo))
                        {
                            sheet.SetColumnWidth(item.Orden, item.Tamanio * 256);
                        }
                    }
                }

                //Llena la cabecera
                fila = sheet.CreateRow(ultimaFila);
                foreach (var columna in columnasExportar.OrderBy(x => x.Orden))
                {
                    if (columna.Grupo.Contains(grupo))
                    {
                        celda = fila.CreateCell(columna.Orden);
                        celda.SetCellValue(columna.Cabecera);
                        if (tipoexportacion == TipoExportacion.ExcelCabeceras)
                        {
                            celda.CellStyle = (HSSFCellStyle)fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Cabecera);

                            //Actualizamos los estilos históricos
                            estilosHistoricos = fabricaEstilos.Estilos;
                        }
                    }
                }

                //Agregar las filas de datos
                var secuencia = ultimaFila + 1;
                foreach (var registro in datos)
                {
                    fila = sheet.CreateRow(secuencia++);
                    foreach (var columna in columnasExportar.OrderBy(x => x.Orden))
                    {
                        if (columna.Grupo.Contains(grupo))
                        {
                            EscribirCelda(wb, fila, columna, registro);
                        }
                    }
                }
            }

            return wb;
        }

        private static void EscribirCelda(IWorkbook wb, IRow fila, ExportarColumnasInfo columna, object dato)
        {
            var fabricaEstilos = new FabricaEstilos(estilosHistoricos);
            var celda = fila.CreateCell(columna.Orden);
            bool esPersonalizado = !string.IsNullOrEmpty(columna.Formato);

            switch (columna.Propiedad.PropertyType.Name)
            {
                case "Decimal":
                    celda.SetCellValue(Convert.ToDouble(columna.Propiedad.GetValue(dato, null)));
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        :celda.CellStyle = fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Decimal);
                    break;
                case "Double":
                    celda.SetCellValue(Convert.ToDouble(columna.Propiedad.GetValue(dato, null)));
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        :celda.CellStyle = fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Decimal);
                    break;
                case "DateTime":
                    celda.SetCellValue(Convert.ToDateTime(columna.Propiedad.GetValue(dato, null)));
                    if (celda.ToString().CompareTo("-1") == 0) celda.SetCellValue("");
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        :celda.CellStyle = fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Fecha);
                    break;
                case "Int32":
                    celda.SetCellValue(Convert.ToInt32(columna.Propiedad.GetValue(dato, null)));
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        :celda.CellStyle = fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Centro);
                    break;
                case "int":
                    celda.SetCellValue(Convert.ToInt32(columna.Propiedad.GetValue(dato, null)));
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        :celda.CellStyle = fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Centro);
                    break;
                default:
                    celda.SetCellValue(Convert.ToString(columna.Propiedad.GetValue(dato, null)));
                    celda.CellStyle = esPersonalizado ? 
                        fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Personalizado, columna.Formato, columna.Alineamiento)
                        : fabricaEstilos.obtenerCellStyle(wb, TipoCellStyle.Izquierda);
                    break;
            }

            //Actualizamos los estilos históricos
            estilosHistoricos = fabricaEstilos.Estilos;
        }
        #endregion

        #region Utilidades de Codificación
        /// <summary>
        /// Convierte la representacion en forma de objeto de un Tipo en el tipo especificado en la función. 
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="obj">objeto a convertir</param>
        /// <returns></returns>
        public static T Cast<T>(object obj)
        {
            if (obj is T)
            {
                return (T)obj;
            }
            else
            {
                try
                {
                    //se ha corregido la funcionalidad de convertir el objeto en un tipo nullable.
                    Type t = typeof(T);
                    t = Nullable.GetUnderlyingType(t) ?? t;
                    return (obj == null || DBNull.Value.Equals(obj)) ? default(T) : (T)Convert.ChangeType(obj, t);
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }
        }

        public static List<T> CastArray<T>(object obj)
        {
            if (obj is T)
            {
                return (List<T>)obj;
            }
            else
            {
                try
                {
                    return (List<T>)Convert.ChangeType(obj, typeof(List<T>));
                }
                catch (InvalidCastException)
                {
                    return default(List<T>);
                }
            }
        }

        public static int Contar(object obj)
        {
            //Este método reemplaza a la siguiente estructura:
            //if (resultadoVista != null)
            //{
            //    if (resultadoVista.Hits != null)
            //    {
            //        if (resultadoVista.Hits.Count() > 0)
            //        {
            int respuesta = 0;
            if (obj != null)
            {
                foreach (var infoMiembro in obj.GetType().GetMembers())
                {
                    //Filtramos los campos o columnas del DTO
                    if (infoMiembro.MemberType == MemberTypes.Property && (infoMiembro).Name.CompareTo("Hits") == 0)
                    {
                        var Campo = (PropertyInfo)infoMiembro;
                        var Hits = Campo.GetValue(obj, null);
                        if (Hits != null)
                        {
                            var property = typeof(ICollection).GetProperty("Count");
                            respuesta = (int)property.GetValue(Hits, null);
                        }
                        break;
                    }
                }
            }
            return respuesta;
        }
        public static bool HayErrorParametroNulo(Dictionary<string, object> parametros, out string mensajeError)
        {
            bool respuesta = false;
            mensajeError = string.Empty;

            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                object obj;
                if (!parametros.TryGetValue(parametro.Key, out obj))
                {
                    respuesta = true;
                    mensajeError = string.Format("No se pudo obtener el parametro {0}.", parametro.Key);
                    break;
                }
                if (obj == null)
                {
                    respuesta = true;
                    mensajeError = string.Format("El parametro {0} no puede ser nulo.", parametro.Key);
                    break;
                }
            }
            return respuesta;
        }
        #endregion
    }
}