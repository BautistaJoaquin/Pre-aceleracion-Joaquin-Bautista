using preAceleracionDisney.Entities;
using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        Movie AddMovie(Movie movie);
        List<Movie> GetAllMovies();
        Movie GetMovie(int id);
    }
}