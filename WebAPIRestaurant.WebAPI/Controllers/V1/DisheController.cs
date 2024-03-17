﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;

namespace WebAPIRestaurant.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DisheController : BaseApiController
    {
        private readonly IDisheService _disheService;
        public DisheController(IDisheService disheService)
        {
            _disheService = disheService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveDisheViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _disheService.Add(sv);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] SaveDisheViewModel sv, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _disheService.Update(sv, id);

            return Ok(sv);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           var dishes =  await _disheService.GetAll();
            return Ok(dishes);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var dishe = await _disheService.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(dishe);
        }
    }
}
