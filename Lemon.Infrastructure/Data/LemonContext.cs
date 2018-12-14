using Lemon.Core.Entities.Artists;
using Lemon.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lemon.Infrastructure.Data
{
    public class LemonContext : DbContext
    {
        public LemonContext(DbContextOptions<LemonContext> options)
            : base(options)
        {
        }

        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Band> Bands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new MusicianConfiguration());
            modelBuilder.ApplyConfiguration(new BandConfiguration());
        }
    }
}
