using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Repositories;
using preAceleracionDisney.ViewModels.Genders.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GenderController : ControllerBase
    {
        private readonly DisneyDbContext _context;
        private readonly IGenderRepository _genderRepository;
        public GenderController(IGenderRepository genderRepository, DisneyDbContext context)
        {
            _context = context;
            _genderRepository = genderRepository;
        }

        [HttpGet]
        [Route("Listado de Generos")]
        public IActionResult Get()
        {
            var genders = _genderRepository.GetAllGenders();
            var gendersViewModel = new List<GetRequestViewModel>();

            foreach (var gender in genders)
            {
                gendersViewModel.Add(new GetRequestViewModel
                {
                    Name = gender.Name,
                    Image = gender.Image

                });
            }

            return Ok(gendersViewModel);
        }

        [HttpPost]
        public IActionResult Post(Gender gender)
        {
            Gender newGender = new Gender
            {
                Id = gender.Id,
                Image = gender.Image,
                Name = gender.Name
            };

            _context.Genders.Add(newGender);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Gender gender)
        {
            var oGender = _genderRepository.Get(gender.Id);
            if (oGender == null) return BadRequest(error: $"El género {gender.Id} no existe");

            oGender.Name = gender.Name;
            oGender.Image = gender.Image;
           
            _genderRepository.Update(oGender);

            return Ok();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult Delete(int id)
        {
            if (_context.Genders.FirstOrDefault(x => x.Id == id) == null) return BadRequest("El genero enviado no existe");

            var InternalGender = _context.Genders.Find(id);

            _context.Genders.Remove(InternalGender);
            _context.SaveChanges();
            return Ok(_context.Genders.ToList());
        }
    }
}
