using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.ViewModels.Characters.Get
{
    public class GetCharacterResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string History { get; set; }
        public string Title { get; internal set; }
    }
}
