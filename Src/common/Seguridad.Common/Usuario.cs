using QueryContracts.Common;
using QueryContracts.Common.Seguridad.Results;
using System;
using System.Collections.Generic;


namespace Seguridad.Common
{
    public class Usuario
    {
        private double idUsuario;
        private string codigousuario;
        private string nombreusuario;
        private string emailusuario;
        private DateTime fecharegistro;
        private bool isapproved;
        private bool islockedout;
        private bool isonline;
        private DateTime lastactivitydate;
        private DateTime lastlockeddate;
        private DateTime lastlogindate;
        private Int64? idusuariopwd;
        private string recordatoriopwd;
        private List<Perfil> perfiles;
        

        public Usuario(ObtenerDatosUsuarioResult resultado)
        {
            if (resultado != null)
            {
                idUsuario = resultado.Idusuario;
                codigousuario = resultado.CodigoUsuario;
                nombreusuario = resultado.NombreUsuario;
                fecharegistro = resultado.FechaRegistro.HasValue ? (DateTime)resultado.FechaRegistro : DateTime.Now;
                isapproved = resultado.IsApproved.HasValue ? Convert.ToBoolean(resultado.IsApproved) : false;
                islockedout = resultado.IsLockedout.HasValue ? Convert.ToBoolean(resultado.IsLockedout) : false;

                if (resultado.Perfiles != null){
                    perfiles = new List<Perfil>();
                    foreach (var perfil in resultado.Perfiles){
                        perfiles.Add(new Perfil() { IdPerfil = perfil.IdPerfil, NombrePerfil = perfil.NombrePerfil });
                    }
                }
            }
        }

        public double Idusuario { get { return idUsuario; } }
        public string CodigoUsuario { get { return codigousuario; } }
        public string NombreUsuario { get { return nombreusuario; } }
        public string EmailUsuario { get { return emailusuario; } }
        public DateTime FechaRegistro { get { return fecharegistro; } }
        public bool IsApproved { get { return isapproved; } }
        public bool IsLockedout { get { return islockedout; } }
        public bool IsOnline { get { return isonline; } }
        public DateTime LastActivityDate { get { return lastactivitydate; } }
        public DateTime LastLockedOutDate { get { return lastlockeddate; } }
        public DateTime LastLoginDate { get { return lastlogindate; } }
        public Int64? IdUsuarioPwd { get { return idusuariopwd; } }
        public string RecordatorioPwd { get { return recordatoriopwd; } }

        public List<Perfil> Perfiles { get { return perfiles; } }
    }
}
