using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.genders.Post
{
    public class PostRequestViewModel
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre del Personaje.")]
        [MaxLength(10, ErrorMessage = "El nombre debe ser de 10 caracteres como max.")]
        [MinLength(5, ErrorMessage = "El nombre debe ser de 5 caracteres como min.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingresa una imagen para el personaje.")]
        public string Image { get; set; }
        
    }
}
