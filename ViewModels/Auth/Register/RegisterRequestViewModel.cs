using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Auth.Register
{
    public class RegisterRequestViewModel
    {
        [Required(ErrorMessage = "Ingresa un Nombre de Usuario.")]
        public string Username {get; set;}
        [Required(ErrorMessage = "Ingresa un Correo Electrónico.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingresa una contraseña.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirma tu contraseña.")]
        [Compare("Password", ErrorMessage = "La contraseña no es igual.")]
        public string ConfirmPassword { get; set; }
    }
}
