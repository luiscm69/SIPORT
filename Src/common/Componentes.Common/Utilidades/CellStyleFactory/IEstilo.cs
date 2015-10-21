using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;

namespace Componentes.Common.Utilidades.CellStyleFactory
{
    interface IEstilo
    {
        void Set(IWorkbook wb);
        void Set(IWorkbook wb, string alineamiento, string formato);
        ICellStyle Get();
        short GetIndex();
    }
}
