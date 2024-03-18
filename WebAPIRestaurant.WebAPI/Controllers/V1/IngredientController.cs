using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Ingredient;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Create(SaveIngredientViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _ingredientService.Add(sv);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(SaveIngredientViewModel sv)
        {
            var searchIngrendient = await _ingredientService.GetById(sv.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _ingredientService.Update(sv, searchIngrendient.Id);
            return Ok(sv);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var ingredients = await _ingredientService.GetAll();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(ingredients.Count == 0)
            {
                return StatusCode(StatusCodes.Status204NoContent, "There is not ingredient");
            }

            return Ok(ingredients);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var ingredient = await _ingredientService.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (ingredient == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "The ingredient doesn't exist");
            }
            return Ok(ingredient);
        }
    }
}
