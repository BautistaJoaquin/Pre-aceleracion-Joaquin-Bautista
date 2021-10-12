using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int Qualification { get; set; }

        //Relaciones
        public Gender genders { get; set; }
        public ICollection<Character> characters { get; set; }
    }
}
