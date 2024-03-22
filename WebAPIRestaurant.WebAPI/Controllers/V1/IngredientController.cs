using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Ingredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrator")]
    
    public class IngredientController : BaseApiController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> Create(SaveIngredientViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _ingredientService.Add(sv);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> Update(SaveIngredientViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var searchIngrendient = await _ingredientService.GetById(sv.Id);
                await _ingredientService.Update(sv, searchIngrendient.Id);
                return Ok(sv);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
          
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> List()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var ingredients = await _ingredientService.GetAll();
                if (ingredients.Count == 0)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "There is not ingredient");
                }
                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var ingredient = await _ingredientService.GetById(id);
                if (ingredient == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "The ingredient doesn't exist");
                }
                return Ok(ingredient);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
