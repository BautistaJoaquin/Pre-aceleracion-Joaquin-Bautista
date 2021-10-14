using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Movies.Post
{
    public class PostMovieResponseViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
    }
}
