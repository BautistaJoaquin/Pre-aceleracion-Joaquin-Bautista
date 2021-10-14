using preAceleracionDisney.Entities;
using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface IGenderRepository : IBaseRepository<Gender>
    {
        Gender AddGender(Gender gender);
        List<Gender> GetAllGenders();
        Gender GetGender(int id);
    }
}