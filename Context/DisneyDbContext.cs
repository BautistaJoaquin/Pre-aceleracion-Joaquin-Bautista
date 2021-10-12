using Microsoft.EntityFrameworkCore;
using preAceleracionDisney.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace preAceleracionDisney.Context
{
    public class DisneyDbContext : DbContext
    {
        public const string Schema = "disney";
        public DisneyDbContext(DbContextOptions<DisneyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
