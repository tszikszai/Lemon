using Lemon.Core.Entities.Artists;
using Lemon.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.Infrastructure
{
    public class ValidateBandExistsAttribute : TypeFilterAttribute
    {
        public ValidateBandExistsAttribute()
            : base(typeof(ValidateBandExistsFilter))
        {
        }

        private class ValidateBandExistsFilter : IAsyncActionFilter
        {
            private readonly IAsyncRepository<Band> _bandRepository;

            public ValidateBandExistsFilter(IAsyncRepository<Band> bandRepository)
            {
                _bandRepository = bandRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    int? id = context.ActionArguments["id"] as int?;
                    if (id.HasValue)
                    {
                        if (!await _bandRepository.ExistsAsync(id.Value))
                        {
                            context.Result = new NotFoundObjectResult(id.Value);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
