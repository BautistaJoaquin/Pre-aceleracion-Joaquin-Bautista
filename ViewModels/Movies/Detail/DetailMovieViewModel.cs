using preAceleracionDisney.ViewModels.Characters.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Movies.Detail
{
    public class DetailMovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public float Qualification { get; set; }
        public List<CharactersViewModel> Characters { get; set; }

    }
}
