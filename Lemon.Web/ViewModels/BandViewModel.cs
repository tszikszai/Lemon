﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.ViewModels
{
    public class BandViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int ActiveFromYear { get; set; }
        public int? ActiveToYear { get; set; }
    }
}