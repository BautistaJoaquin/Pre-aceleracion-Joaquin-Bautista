using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Repositories;
using preAceleracionDisney.ViewModels.Characters.Get;
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
        [Route("Listado de Personajes")]
        public IActionResult Get()
        {
            var characters = _characterRepository.GetAllCharacters();
            var characterViewModel = new List<GetRequestViewModel>();

            foreach (var character in characters)
            {
                characterViewModel.Add(new GetRequestViewModel
                {
                    Name = character.Name,
                    Image = character.Image

                }); ;
            }

            return Ok(characterViewModel);
        }

        [HttpPost]
        public IActionResult Post(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
            return Ok(_context.Movies.ToList());
        }

        [HttpPut]
        public IActionResult Put(Character character)
        {
            if (_context.Movies.FirstOrDefault(x => x.Id == character.Id) == null) return BadRequest("El pesonaje enviado no existe");

            var InternalCharacter = _context.Characters.Find(character.Id);

            InternalCharacter.Image = character.Image;
            InternalCharacter.Name = character.Name;
            InternalCharacter.Weight = character.Weight;
            InternalCharacter.History = character.History;
            InternalCharacter.Age = character.Age;

            _context.SaveChanges();
            return Ok(_context.Characters.ToList());
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
