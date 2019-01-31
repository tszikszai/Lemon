using Lemon.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.ViewModels
{
    public class MusicianViewModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [DateRangeUntilToday(1900, 1, 1)]
        public DateTime DateOfBirth { get; set; }
        [DateRangeUntilToday(1900, 1, 1)]
        public DateTime? DateOfDeath { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth > (DateOfDeath ?? DateTime.MaxValue))
            {
                yield return new ValidationResult("Date of Birth must be prior to Date of Death.");
            }
        }
    }
}
