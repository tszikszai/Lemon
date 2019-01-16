using Lemon.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.ViewModels
{
    public class BandViewModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [RangeUntilCurrentYear(1900)]
        public int ActiveFromYear { get; set; }
        [RangeUntilCurrentYear(1900)]
        public int? ActiveToYear { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ActiveFromYear > (ActiveToYear ?? int.MaxValue))
            {
                yield return new ValidationResult("Active from Year must be lower than or equal to Active to Year.");
            }
        }
    }
}
