using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;


namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public class CellStyleCentro : IEstilo 
    {
        ICellStyle estilo;

        public CellStyleCentro() { }

        public void Set(IWorkbook wb)
        {
            if (wb == null) throw new ArgumentNullException("El libro al que pertenece el estilo no puede estar vacío.","wb");

            estilo = wb.CreateCellStyle();
            estilo.Alignment = HorizontalAlignment.CENTER;
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
