using System.Web;
using log4net;              //Eliminar esta linea y la referencia
using System.Reflection;
using System;    //Eliminar solo esta linea

namespace Seguridad.Common
{
    public static class SessionWrapper
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);    //Eliminar solo esta linea
        const string InfoPerfil = "SessionWrapperPerfil";
        const string InfoUsuario = "SessionWrapperUsuario";
        const string InfoPrimeraConexion = "SessionWrapperConexion";

        
        public static Perfil Perfil
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoPerfil])
                    return HttpContext.Current.Session[InfoPerfil] as Perfil;
                return null;
            }
            set
            {
                HttpContext.Current.Session[InfoPerfil] = value;
            }
        }

        public static Usuario Usuario
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoUsuario])
                    return HttpContext.Current.Session[InfoUsuario] as Usuario;
                return null;
            }
            set
            {
                HttpContext.Current.Session[InfoUsuario] = value;
            }
        }

        public static bool PrimeraConexion
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoPrimeraConexion])
                    return  Convert.ToBoolean(HttpContext.Current.Session[InfoPrimeraConexion]);
                return false;
            }
            set
            {
                HttpContext.Current.Session[InfoPrimeraConexion] = value;
            }
        }
    }
}
