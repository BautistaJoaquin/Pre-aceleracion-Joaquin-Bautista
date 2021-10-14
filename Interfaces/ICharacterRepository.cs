using preAceleracionDisney.Entities;
using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface ICharacterRepository : IBaseRepository<Character>
    {
        Character AddCharacter(Character character);
        List<Character> GetAllCharacters();
        Character GetCharacter(int id);
    }
}