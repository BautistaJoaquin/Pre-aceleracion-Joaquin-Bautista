using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Repositories;
using preAceleracionDisney.ViewModels.Characters.Detail;
using preAceleracionDisney.ViewModels.Characters.Get;
using preAceleracionDisney.ViewModels.Characters.Post;
using preAceleracionDisney.ViewModels.Movies.Detail;
using preAceleracionDisney.ViewModels.Movies.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private readonly DisneyDbContext _context;
        private readonly ICharacterRepository _characterRepository;
        public CharacterController(ICharacterRepository characterRepository, DisneyDbContext context)
        {
            _context = context;
            _characterRepository = characterRepository;
        }

        [HttpGet]
        [Route("ListCharacters")]
        //Muestra Listado de Personajes
        public IActionResult Get()
        {
            var characters = _characterRepository.GetAllCharacters();
            var characterViewModel = new List<GetCharacterRequestViewModel>();

            foreach (var character in characters)
            {
                characterViewModel.Add(new GetCharacterRequestViewModel
                {
                    Name = character.Name,
                    Image = character.Image

                }); ;
            }

            return Ok(characterViewModel);
        }

        [HttpGet]
        [Route(template: "DetailsCharacters")]
        //Filtrado de personajes con detalle
        public IActionResult GetDetails(int id, string name, int age) 
        {
            var characters = _context.Characters.Include(x => x.Movies).ToList();
            var characterViewModel = new List<DetailCharacterViewModel>();

            if (id != 0 || !string.IsNullOrEmpty(name) || age > 0)
            {
                characters = characters.Where(x => x.Id == id || x.Name == name || x.Age == age).ToList();
            
            }

            foreach (var character in characters)
            {
                characterViewModel.Add(new DetailCharacterViewModel
                {
                    Id = character.Id,
                    Name = character.Name,
                    Image = character.Image,
                    Age = character.Age,
                    Weight = character.Weight,
                    History = character.History,
                    Movies = character.Movies.Any() ? character.Movies.Select(x => new MoviesViewModel
                    {
                        Title = x.Title,
                        Image = x.Image
                    }).ToList() : null
                });
            }

            if (!characters.Any()) return BadRequest(error: $"El personaje {id} no existe");

            return Ok(characterViewModel);
        }


        [HttpPost]
        public IActionResult Post(PostRequestViewModel character)
        {
            Character newCharacter = new Character
            {
                Name = character.Name,
                Image = character.Image,
                Age = character.Age,
                Weight = character.Weight,
                History = character.History
            };
            _context.Characters.Add(newCharacter);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Character character)
        {
            var oCharacter = _characterRepository.Get(character.Id);
            if (oCharacter == null) return BadRequest(error: $"El personaje {character.Id} no existe");

            oCharacter.Name = character.Name;
            oCharacter.Image = character.Image;
            oCharacter.Weight = character.Weight;
            oCharacter.Age = character.Age;
            oCharacter.History = character.History;

            _characterRepository.Update(oCharacter);

            return Ok();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult Delete(int id)
        {
            if (_context.Characters.FirstOrDefault(x => x.Id == id) == null) return BadRequest("La pelicula enviada no existe");

            var InternalCharacter = _context.Characters.Find(id);

            _context.Characters.Remove(InternalCharacter);
            _context.SaveChanges();
            return Ok(_context.Characters.ToList());
        }
    }
}
