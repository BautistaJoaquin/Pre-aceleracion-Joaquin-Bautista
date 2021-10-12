using preAceleracionDisney.Entities;
using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface IGenderRepository
    {
        Gender AddGender(Gender gender);
        List<Gender> GetAllGenders();
        Gender GetGender(int id);
    }
}