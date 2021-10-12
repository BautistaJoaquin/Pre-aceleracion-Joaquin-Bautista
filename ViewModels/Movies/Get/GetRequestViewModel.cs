using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Movies.Get
{
    public class GetRequestViewModel
    {
        [Required(ErrorMessage = "Ingresa una Imagen para la pelicula.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Ingresa el titulo de la pelicula.")]
        [MaxLength(50, ErrorMessage = "Se admite 50 caracteres como max.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ingresa la fecha de creación.")]
        [DataType(DataType.DateTime, ErrorMessage = "Ingresa una fecha válida.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Ingresa una calificación.")]
        [Range(1, 5, ErrorMessage = "La calificaión debe estar entre 1 y 5.")]
        public int Qualification { get; set; }
    }
}
