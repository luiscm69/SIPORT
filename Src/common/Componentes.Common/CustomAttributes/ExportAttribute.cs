using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Componentes.Common.CustomAttributes
{
    public enum TipoExportacion
    {
        ExcelSimple,
        ExcelCabeceras
    }
    public class ExportAttribute : Attribute
    {
        public const string GRUPO_DEFAULT = "grupo1";
        private TipoExportacion tipoExportacion;
        private int? orden;
        private string cabecera;
        private int? tamanio;
        private string formato;
        private string alineamiento;
        private string[] grupo;

        public TipoExportacion TipoExportacion
        {
            get { return this.tipoExportacion; }
            set { this.tipoExportacion = value; }
        }

        public int Orden
        {
            get { return this.orden.HasValue ? Convert.ToInt32(this.orden) : 0; }
            set { this.orden = value; }
        }

        public string Cabecera
        {
            get { return this.cabecera; }
            set { this.cabecera = value; }
        }

        public int Tamanio
        {
            get { return this.tamanio.HasValue ? Convert.ToInt32(this.tamanio) : 0; }
            set { this.tamanio = value; }
        }

        public string Formato
        {
            get { return this.formato; }
            set { this.formato = value; }
        }

        public string Alineamiento
        {
            get { return this.alineamiento; }
            set { this.alineamiento = value; }
        }

        public string[] Grupo
        {
            get { return this.grupo != null && this.grupo.Any() ? this.grupo : new string[] { GRUPO_DEFAULT }; }
            set { this.grupo = value; }
        }

    }
}