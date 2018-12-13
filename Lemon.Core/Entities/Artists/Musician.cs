using System;
using System.Collections.Generic;
using System.Text;

namespace Lemon.Core.Entities.Artists
{
    public class Musician : Artist
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
    }
}
