using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly DisneyDbContext _context;
        public MovieController(DisneyDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("Obtener peliculas")]
        public IActionResult Get()
        {
            return Ok(_context.Movies.ToList());
        }

        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok(_context.Movies.ToList());
        }

        [HttpPut]
        public IActionResult Put(Movie movie)
        {
            if (_context.Movies.FirstOrDefault(x => x.Id == movie.Id) == null) return BadRequest("La pelicula enviada no existe");

            var InternalMovie = _context.Movies.Find(movie.Id);

            InternalMovie.Image = movie.Image;
            InternalMovie.Title = movie.Title;
            InternalMovie.CreationDate = movie.CreationDate;
            InternalMovie.Qualification = movie.Qualification;

            _context.SaveChanges();
            return Ok(_context.Movies.ToList());
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult Delete(int id)
        {
            if (_context.Movies.FirstOrDefault(x => x.Id == id) == null) return BadRequest("La pelicula enviada no existe");

            var InternalMovie = _context.Movies.Find(id);

            _context.Movies.Remove(InternalMovie);
            _context.SaveChanges();
            return Ok(_context.Movies.ToList());
        }

    }
}
