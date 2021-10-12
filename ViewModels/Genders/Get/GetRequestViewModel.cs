using System.ComponentModel.DataAnnotations;

namespace preAceleracionDisney.ViewModels.Genders.Get
{
    public class GetRequestViewModel
    {
        [Required(ErrorMessage = "Ingresa un Nombre.")]
        [MaxLength(20, ErrorMessage = "Se admite solo 20 caracteres como max.")]
        [MinLength(4, ErrorMessage = "Solo 4 caracteres como min.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ingresa una imagen.")]
        public string Image { get; set; }
    }
}
