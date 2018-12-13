using System;
using System.Collections.Generic;
using System.Text;

namespace Lemon.Core.Entities.Artists
{
    public class Band : Artist
    {
        public string Name { get; set; }
        public int ActiveFromYear { get; set; }
        public int? ActiveToYear { get; set; }
    }
}
