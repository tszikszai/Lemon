using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lemon.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class LemonControllerBase : ControllerBase
    {
    }
}
