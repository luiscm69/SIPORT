using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;


namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public class CellStylePersonalizado : IEstilo
    {
        ICellStyle estilo;

        public CellStylePersonalizado() { }

        public void Set(IWorkbook wb)
        {
            if (wb == null) throw new ArgumentNullException("El libro al que pertenece el estilo no puede estar vacío.", "wb");
        }

        public void Set(IWorkbook wb, string alineamiento, string formato)
        {
            if (wb == null) throw new ArgumentNullException("El libro al que pertenece el estilo no puede estar vacío.", "wb");

            estilo = wb.CreateCellStyle();
            switch (alineamiento)
            {
                case "Izquierda":
                    estilo.Alignment = HorizontalAlignment.LEFT;
                    break;
                case "Centro":
                    estilo.Alignment = HorizontalAlignment.CENTER;
                    break;
                case "Derecha":
                    estilo.Alignment = HorizontalAlignment.RIGHT;
                    break;
                default:
                    estilo.Alignment = HorizontalAlignment.GENERAL;
                    break;
            }
            if (!string.IsNullOrEmpty(formato))
            {
                IDataFormat format = wb.CreateDataFormat();
                estilo.DataFormat = format.GetFormat(formato);
            }
        }

        public ICellStyle Get()
        {
            return estilo;
        }

        public short GetIndex()
        {
            return estilo.Index;
        }
    }
}
