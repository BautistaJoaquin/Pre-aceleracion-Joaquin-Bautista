using Microsoft.EntityFrameworkCore;
using preAceleracionDisney.Context;
using preAceleracionDisney.Entities;
using System.Collections.Generic;
using System.Linq;

namespace preAceleracionDisney.Repositories
{
    public class CharacterRepository : BaseRepository<Character, DisneyDbContext>, ICharacterRepository
    {

        public CharacterRepository(DisneyDbContext dbContext) : base(dbContext)
        {
        }

        public Character AddCharacter(Character character)
        {
            return Add(character);
        }

        public List<Character> GetAllCharacters()
        {
            return GetAllEntities();
        }

        public Character GetCharacter(int id)
        {
            return DbSet.Find(id);
        }
    }
}