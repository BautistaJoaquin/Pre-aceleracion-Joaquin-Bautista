using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using preAceleracionDisney.Repositories;
using preAceleracionDisney.ViewModels.Characters.Detail;
using preAceleracionDisney.ViewModels.Characters.Get;
using preAceleracionDisney.ViewModels.Movies.Detail;
using preAceleracionDisney.ViewModels.Movies.Get;
using preAceleracionDisney.ViewModels.Movies.Post;
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
        private readonly IMovieRepository _movieRepository;
        public MovieController(DisneyDbContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }
        
        [HttpGet]
        [Route("MoviesList")]
        public IActionResult Get(string Order)
        {
            var movies = _movieRepository.GetAllMovies();
            var movieViewModel = new List<GetMovieRequestViewModel>();
            foreach (var movie in movies)
            {
                movieViewModel.Add(new GetMovieRequestViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Image = movie.Image,
                    CreationDate = movie.CreationDate
                }); ;
            }

            if (!string.IsNullOrEmpty(Order)) { 
            // Seleccion del orden
            switch (Order.ToUpper())
            {
                case "ASC":
                    return Ok(movieViewModel.OrderBy(x => x.Id).ToList()); 

                case "DESC":
                    return Ok(movieViewModel.OrderByDescending(x => x.Id).ToList());
                
                default:
                    return Ok(movieViewModel); 

            }
            }
            else
            {
                return BadRequest(error: $"Insert an order(ASC = ascending/ DSC = descending)!!!");
            }
        }

        [HttpGet]
        [Route("DetailsMovie")]
        public IActionResult Get(int id, string title) 
        {
            var movies = _context.Movies.Include(x => x.Characters).ToList();
            var movieViewModel = new List<DetailMovieViewModel>();
            if (id != 0 || !string.IsNullOrEmpty(title))
            {
                movies = movies.Where(x => x.Id == id || x.Title == title).ToList();

            }
           
            foreach (var movie in movies)
            {
                movieViewModel.Add(new DetailMovieViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Image = movie.Image,
                    CreationDate = movie.CreationDate,
                    Qualification = movie.Qualification,
                    Characters = movie.Characters.Any() ? movie.Characters.Select(x => new CharactersViewModel
                    {
                        Name = x.Name,
                        Age = x.Age
                    }).ToList() : null
                });
            }

            if (!movies.Any()) return BadRequest(error: $"Esta película/serie {id} no existe");

            return Ok(movieViewModel);
        }

        [HttpPost]
        public IActionResult Post(PostMovieRequestViewModel movie)
        {
            Movie newMovie = new Movie
            {
                Image = movie.Image,
                Title = movie.Title,
                CreationDate = movie.CreationDate,
                Qualification = movie.Qualification,
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Movie movie)
        {
            var oMovie = _movieRepository.Get(movie.Id);
            if (oMovie == null) return BadRequest(error: $"La película/serie {movie.Id} no existe");

            oMovie.Title = movie.Title;
            oMovie.Image = movie.Image;
            oMovie.CreationDate = movie.CreationDate;
            oMovie.Qualification = movie.Qualification;

            _movieRepository.Update(oMovie);

            return Ok();
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
