using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Entities
{
    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
