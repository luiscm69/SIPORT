
using System.ComponentModel.DataAnnotations;
namespace Web.Siport.Models.Account
{
    public class SignInModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Código Usuario")]
        public string CodigoUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool Recordarme { get; set; }
    }
}