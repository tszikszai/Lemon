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
    public class ValidateMusicianExistsAttribute : TypeFilterAttribute
    {
        public ValidateMusicianExistsAttribute()
            : base(typeof(ValidateMusicianExistsFilter))
        {
        }

        private class ValidateMusicianExistsFilter : IAsyncActionFilter
        {
            private readonly IAsyncRepository<Musician> _musicianRepository;

            public ValidateMusicianExistsFilter(IAsyncRepository<Musician> musicianRepository)
            {
                _musicianRepository = musicianRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    int? id = context.ActionArguments["id"] as int?;
                    if (id.HasValue)
                    {
                        if (!await _musicianRepository.ExistsAsync(id.Value))
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
