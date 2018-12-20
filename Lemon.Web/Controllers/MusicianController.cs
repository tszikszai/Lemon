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
    public class MusicianController : Controller
    {
        private readonly IAsyncRepository<Musician> _musicianRepository;

        public MusicianController(IAsyncRepository<Musician> musicianRepository)
        {
            _musicianRepository = musicianRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<MusicianViewModel>> Get()
        {
            var musicians = await _musicianRepository.GetAllAsync();
            return musicians.Select(x => new MusicianViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                DateOfDeath = x.DateOfDeath
            });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var musician = await _musicianRepository.GetByIdAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            var musicianViewModel = new MusicianViewModel
            {
                Id = musician.Id,
                FirstName = musician.FirstName,
                LastName = musician.LastName,
                DateOfBirth = musician.DateOfBirth,
                DateOfDeath = musician.DateOfDeath
            };
            return Ok(musicianViewModel);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MusicianViewModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var musician = new Musician
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                DateOfBirth = value.DateOfBirth,
                DateOfDeath = value.DateOfDeath
            };
            await _musicianRepository.AddAsync(musician);
            var musicianViewModel = new MusicianViewModel
            {
                Id = musician.Id,
                FirstName = musician.FirstName,
                LastName = musician.LastName,
                DateOfBirth = musician.DateOfBirth,
                DateOfDeath = musician.DateOfDeath
            };
            return Ok(musicianViewModel);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MusicianViewModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var musician = await _musicianRepository.GetByIdAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            musician.FirstName = value.FirstName;
            musician.LastName = value.LastName;
            musician.DateOfBirth = value.DateOfBirth;
            musician.DateOfDeath = value.DateOfDeath;
            await _musicianRepository.UpdateAsync(musician);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var musician = await _musicianRepository.GetByIdAsync(id);
            if (musician == null)
            {
                return NotFound();
            }

            await _musicianRepository.DeleteAsync(musician);
            return Ok();
        }
    }
}
