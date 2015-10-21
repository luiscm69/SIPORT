using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;
using System.Runtime.Remoting;

namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public enum TipoCellStyle
	{
	    Cabecera,
        Centro,
        Decimal,
        Derecha,
        Fecha,
        IDMovimiento,
        Izquierda,
        Personalizado
	}

    public class Estilos
    {
        public string nombre { get; set; }
        public short wbIndiceEstilo { get; set; }
    }

    public class FabricaEstilos
    {
        public FabricaEstilos(List<Estilos> estilos)
        {
            this.estilos = estilos;
        }

        private List<Estilos> estilos;

        public List<Estilos> Estilos
        {
            get
            {
                return estilos;
            }
            set
            {
                estilos = value;
            }
        }

        private short existeEstilo(string nombre)
        {
            short respuesta = -1;

            if (estilos != null)
            {
                foreach (var estilo in estilos)
                {
                    if (estilo.nombre.CompareTo(nombre) == 0)
                    {
                        respuesta = estilo.wbIndiceEstilo;
                        break;
                    }
                }
            }
            else
            {
                estilos = new List<Estilos>();
            }
            return respuesta;
        }

        public ICellStyle obtenerCellStyle(IWorkbook wb, TipoCellStyle tipoCellStyle) { return obtenerCellStyle(wb, tipoCellStyle, string.Empty, string.Empty); }
        public ICellStyle obtenerCellStyle(IWorkbook wb, TipoCellStyle tipoCellStyle, string formato, string alineamiento) 
        {
            if (wb == null) throw new ArgumentException("Se requiere un libro de trabajo.");

            //TODO
            //20150312 JCLP - Se asume que una fábrica atiende un solo workbook
            var strClassName = string.Empty;
            IEstilo estilo = null;

            var wbIndiceEstilo = existeEstilo(tipoCellStyle == TipoCellStyle.Personalizado ? alineamiento.ToUpper().Substring(0,1) + formato : tipoCellStyle.ToString());

            if (wbIndiceEstilo > -1)
            {
                return wb.GetCellStyleAt(wbIndiceEstilo);
            }
            else
            {
                try
                {
                    strClassName = string.Format("Componentes.Common.Utilidades.CellStyleFactory.CellStyle{0}", tipoCellStyle.ToString());
                    var obj = Activator.CreateInstance(null, strClassName) as ObjectHandle;

                    estilo = (IEstilo)obj.Unwrap();
                    if (tipoCellStyle == TipoCellStyle.Personalizado)
                    {
                        estilo.Set(wb, alineamiento, formato);
                    }
                    else
                    {
                        estilo.Set(wb);
                    }
                    estilos.Add(new Estilos { nombre = tipoCellStyle == TipoCellStyle.Personalizado ? alineamiento.ToUpper().Substring(0,1) + formato : tipoCellStyle.ToString(), wbIndiceEstilo = estilo.GetIndex() });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return estilo.Get();
            }
        }
    }
}
