
using preAceleracionDisney.ViewModels.Characters.Get;
using preAceleracionDisney.ViewModels.Movies.Detail;
using System.Collections.Generic;

namespace preAceleracionDisney.ViewModels.Characters.Detail
{
    public class DetailCharacterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string History { get; set; }
        public List<MoviesViewModel> Movies { get; set; }
    }
}
