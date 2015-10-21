using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;


namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public class CellStyleCabecera : IEstilo 
    {
        ICellStyle estilo;

        public CellStyleCabecera() {}
        
        public void Set(IWorkbook wb)
        {
            if (wb == null) throw new ArgumentNullException("wb", "El libro al que pertenece el estilo no puede estar vacío.");

            IFont fuenteCabecera = wb.CreateFont();
            fuenteCabecera.Boldweight = (short)FontBoldWeight.BOLD;

            estilo = wb.CreateCellStyle();
            estilo.FillForegroundColor = IndexedColors.LIGHT_YELLOW.Index;
            estilo.FillPattern = FillPatternType.SOLID_FOREGROUND;
            estilo.SetFont(fuenteCabecera);
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
