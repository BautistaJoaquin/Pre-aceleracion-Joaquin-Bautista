using preAceleracionDisney.Entities;
using System.Collections.Generic;

namespace preAceleracionDisney.Repositories
{
    public interface ICharacterRepository
    {
        Character AddCharacter(Character character);
        List<Character> GetAllCharacters();
        Character GetCharacter(int id);
    }
}