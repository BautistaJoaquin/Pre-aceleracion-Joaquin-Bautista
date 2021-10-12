using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using System.Collections.Generic;
using System.Linq;

namespace preAceleracionDisney.Repositories
{
    public class GenderRepository : BaseRepository<Gender, DisneyDbContext>, IGenderRepository
    {
        public GenderRepository(DisneyDbContext dbContext) : base(dbContext)
        {

        }

        public Gender AddGender(Gender gender)
        {
            return Add(gender);

        }

        public List<Gender> GetAllGenders()
        {
            return GetAllEntities();
        }

        public Gender GetGender(int id)
        {
            return Get(id);
        }
    }
}
