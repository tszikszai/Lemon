using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon.Core.Entities.Artists;
using Lemon.Core.Interfaces;
using Lemon.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lemon.Web.Controllers
{
    [Route("api/[controller]")]
    public class BandController : Controller
    {
        private readonly IAsyncRepository<Band> _bandRepository;

        public BandController(IAsyncRepository<Band> bandRepository)
        {
            _bandRepository = bandRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<BandViewModel>> Get()
        {
            var bands = await _bandRepository.GetAllAsync();
            return bands.Select(x => new BandViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ActiveFromYear = x.ActiveFromYear,
                ActiveToYear = x.ActiveToYear
            });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var band = await _bandRepository.GetByIdAsync(id);
            if (band == null)
            {
                return NotFound();
            }

            var bandViewModel = new BandViewModel
            {
                Id = band.Id,
                Name = band.Name,
                ActiveFromYear = band.ActiveFromYear,
                ActiveToYear = band.ActiveToYear
            };
            return Ok(bandViewModel);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BandViewModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var band = new Band
            {
                Name = value.Name,
                ActiveFromYear = value.ActiveFromYear,
                ActiveToYear = value.ActiveToYear
            };
            await _bandRepository.AddAsync(band);
            var bandViewModel = new BandViewModel
            {
                Id = band.Id,
                Name = band.Name,
                ActiveFromYear = band.ActiveFromYear,
                ActiveToYear = band.ActiveToYear
            };
            return Ok(bandViewModel);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BandViewModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var band = await _bandRepository.GetByIdAsync(id);
            if (band == null)
            {
                return NotFound();
            }

            band.Name = value.Name;
            band.ActiveFromYear = value.ActiveFromYear;
            band.ActiveToYear = value.ActiveToYear;
            await _bandRepository.UpdateAsync(band);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var band = await _bandRepository.GetByIdAsync(id);
            if (band == null)
            {
                return NotFound();
            }

            await _bandRepository.DeleteAsync(band);
            return Ok();
        }
    }
}
