using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Characters.Get
{
    public class GetCharacterRequestViewModel
    {
        [Required(ErrorMessage = "Ingrese Nombre del Personaje.")]
        [MaxLength(10, ErrorMessage = "El nombre debe ser de 10 caracteres como max.")]
        [MinLength(5, ErrorMessage = "El nombre debe ser de 5 caracteres como min.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingresa una imagen para el personaje.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Ingresa una Edad.")]
        [Range(1, 100, ErrorMessage = "El intervalo de edad esta entre 1 y 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Ingresa el peso del personaje.")]
        [Range(0.5, 250.0 , ErrorMessage = "Su peso debe estar entre 0.5 kg y 250.0 kg")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Ingresa una  Historia para el personaje.")]
        [MaxLength(255, ErrorMessage = "Puedes escribir 255 caracteres como max.")]
        public string History { get; set; }

        
    }
}
