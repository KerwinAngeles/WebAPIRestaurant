using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;

namespace WebAPIRestaurant.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Waiter")]

    public class OrdenController : BaseApiController
    {
        private readonly IOrdenService _ordenService;
        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveOrdenViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _ordenService.Add(sv);

            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(EditViewModel sv, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _ordenService.UpdateOrden(sv, id);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var ordens = await _ordenService.GetAll();
                if (ordens.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "The ordens it doesn't exist");
                }
                return Ok(ordens);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var orden = await _ordenService.GetById(id);

                if (orden == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "The orden it doesn't exist");
                }

                return Ok(orden);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _ordenService.Delete(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NoContent();
        }
    }
}
