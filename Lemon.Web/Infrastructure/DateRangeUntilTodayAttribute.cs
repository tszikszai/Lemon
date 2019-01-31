using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateRangeUntilTodayAttribute : RangeAttribute
    {
        public DateRangeUntilTodayAttribute(int minimumYear, int minimumMonth, int minimumDay)
            : base(typeof(DateTime), new DateTime(minimumYear, minimumMonth, minimumDay).ToShortDateString(), DateTime.Today.ToShortDateString())
        {
        }
    }
}
