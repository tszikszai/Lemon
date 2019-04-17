using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon.Core.Entities.Artists;
using Lemon.Core.Interfaces;
using Lemon.Web.Infrastructure;
using Lemon.Web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lemon.Web.Controllers
{
    public class BandsController : LemonControllerBase
    {
        private readonly IAsyncRepository<Band> _bandRepository;

        public BandsController(IAsyncRepository<Band> bandRepository)
        {
            _bandRepository = bandRepository;
        }

        // GET: api/bands
        [HttpGet]
        public async Task<IEnumerable<BandViewModel>> Get()
        {
            IReadOnlyList<Band> bands = await _bandRepository.GetAllAsync();
            return bands.Adapt<IEnumerable<BandViewModel>>();
        }

        // GET api/bands/5
        [HttpGet("{id}")]
        [ValidateBandExists]
        public async Task<BandViewModel> Get(int id)
        {
            Band band = await _bandRepository.GetByIdAsync(id);
            BandViewModel bandViewModel = band.Adapt<BandViewModel>();
            return bandViewModel;
        }

        // POST api/bands
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BandViewModel model)
        {
            var band = new Band
            {
                Name = model.Name,
                ActiveFromYear = model.ActiveFromYear,
                ActiveToYear = model.ActiveToYear
            };
            await _bandRepository.AddAsync(band);

            BandViewModel bandViewModel = band.Adapt<BandViewModel>();
            return CreatedAtAction(nameof(Get), new { id = band.Id }, bandViewModel);
        }

        // PUT api/bands/5
        [HttpPut("{id}")]
        [ValidateBandExists]
        public async Task<IActionResult> Put(int id, [FromBody]BandViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            Band band = await _bandRepository.GetByIdAsync(id);
            band.Name = model.Name;
            band.ActiveFromYear = model.ActiveFromYear;
            band.ActiveToYear = model.ActiveToYear;
            await _bandRepository.UpdateAsync(band);

            return NoContent();
        }

        // DELETE api/bands/5
        [HttpDelete("{id}")]
        [ValidateBandExists]
        public async Task<IActionResult> Delete(int id)
        {
            Band band = await _bandRepository.GetByIdAsync(id);
            await _bandRepository.DeleteAsync(band);
            return NoContent();
        }
    }
}
