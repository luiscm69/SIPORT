using System.Collections.Generic;

namespace Seguridad.Common
{
    public interface IPerfil
    {
        string CodigoPerfil { get; set; }

        bool Enmascaramiento { get; set; }

        string CodigoMenu { get; set; }

        List<Menu> ListaMenu { get; set; }
    }
}
