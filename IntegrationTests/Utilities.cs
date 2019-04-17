using Lemon.Core.Entities.Artists;
using Lemon.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(LemonContext db)
        {
            db.Bands.AddRange(GetBands());
            db.Musicians.AddRange(GetMusicians());
            db.SaveChanges();
        }

        public static List<Band> GetBands()
        {
            return new List<Band>
            {
                new Band
                {
                    Name = "Radiohead",
                    ActiveFromYear = 1985
                },
                new Band
                {
                    Name = "Tool",
                    ActiveFromYear = 1990
                },
                new Band
                {
                    Name = "The Doors",
                    ActiveFromYear = 1965,
                    ActiveToYear = 1973
                }
            };
        }

        public static List<Musician> GetMusicians()
        {
            return new List<Musician>
            {
                new Musician
                {
                    FirstName = "Thom",
                    LastName = "Yorke",
                    DateOfBirth = new DateTime(1968, 10, 7)
                },
                new Musician
                {
                    FirstName = "Maynard James",
                    LastName = "Keenan",
                    DateOfBirth = new DateTime(1964, 4, 17)
                },
                new Musician
                {
                    FirstName = "Jim",
                    LastName = "Morrison",
                    DateOfBirth = new DateTime(1943, 12, 8)
                }
            };
        }
    }
}
