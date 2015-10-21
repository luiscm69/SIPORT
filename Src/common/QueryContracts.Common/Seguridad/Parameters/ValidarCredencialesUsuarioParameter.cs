namespace QueryContracts.Common.Seguridad.Parameters
{
    public class ValidarCredencialesUsuarioParameter : QueryParameter
    {
        public string CodigoUsuario { get; set; }
        public string PasswordUsuario { get; set; }
    }
}
