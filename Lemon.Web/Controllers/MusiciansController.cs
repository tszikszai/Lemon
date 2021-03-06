﻿using System;
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
    public class MusiciansController : LemonControllerBase
    {
        private readonly IAsyncRepository<Musician> _musicianRepository;

        public MusiciansController(IAsyncRepository<Musician> musicianRepository)
        {
            _musicianRepository = musicianRepository;
        }

        // GET: api/musicians
        [HttpGet]
        public async Task<IEnumerable<MusicianViewModel>> Get()
        {
            IReadOnlyList<Musician> musicians = await _musicianRepository.GetAllAsync();
            return musicians.Adapt<IEnumerable<MusicianViewModel>>();
        }

        // GET api/musicians/5
        [HttpGet("{id}")]
        [ValidateMusicianExists]
        public async Task<MusicianViewModel> Get(int id)
        {
            Musician musician = await _musicianRepository.GetByIdAsync(id);
            MusicianViewModel musicianViewModel = musician.Adapt<MusicianViewModel>();
            return musicianViewModel;
        }

        // POST api/musicians
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MusicianViewModel model)
        {
            var musician = new Musician
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                DateOfDeath = model.DateOfDeath
            };
            await _musicianRepository.AddAsync(musician);

            MusicianViewModel musicianViewModel = musician.Adapt<MusicianViewModel>();
            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musicianViewModel);
        }

        // PUT api/musicians/5
        [HttpPut("{id}")]
        [ValidateMusicianExists]
        public async Task<IActionResult> Put(int id, [FromBody]MusicianViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            Musician musician = await _musicianRepository.GetByIdAsync(id);
            musician.FirstName = model.FirstName;
            musician.LastName = model.LastName;
            musician.DateOfBirth = model.DateOfBirth;
            musician.DateOfDeath = model.DateOfDeath;
            await _musicianRepository.UpdateAsync(musician);

            return NoContent();
        }

        // DELETE api/musicians/5
        [HttpDelete("{id}")]
        [ValidateMusicianExists]
        public async Task<IActionResult> Delete(int id)
        {
            Musician musician = await _musicianRepository.GetByIdAsync(id);
            await _musicianRepository.DeleteAsync(musician);
            return NoContent();
        }
    }
}
