using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;


namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public class CellStyleDecimal : IEstilo
    {
        ICellStyle estilo;

        public CellStyleDecimal() { }

        public void Set(IWorkbook wb)
        {
            if (wb == null) throw new ArgumentNullException("El libro al que pertenece el estilo no puede estar vacío.", "wb");

            IDataFormat format = wb.CreateDataFormat();
            estilo = wb.CreateCellStyle();
            estilo.Alignment = HorizontalAlignment.RIGHT;
            estilo.DataFormat = format.GetFormat("###,###,##0.00");
        }

        public void Set(IWorkbook wb, string alineamiento, string formato) { throw new Exception("Este estilo no implementa esta propiedad"); }

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
