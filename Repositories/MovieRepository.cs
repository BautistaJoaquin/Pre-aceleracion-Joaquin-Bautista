using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using System.Collections.Generic;
using System.Linq;

namespace preAceleracionDisney.Repositories
{
    public class MovieRepository : BaseRepository<Movie, DisneyDbContext>, IMovieRepository
    {

        public MovieRepository(DisneyDbContext dbContext) : base(dbContext)
        {

        }

        public Movie AddMovie(Movie movie)
        {
            return Add(movie);
        }

        public List<Movie> GetAllMovies()
        {

            return GetAllEntities();

        }

        public Movie GetMovie(int id)
        {
            return Get(id);

        }
    }
}
