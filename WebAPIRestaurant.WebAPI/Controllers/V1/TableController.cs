using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRestaurant.Core.Application.Interfaces.Services;
using WebAPIRestaurant.Core.Application.ViewModels.Table;

namespace WebAPIRestaurant.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TableController : BaseApiController
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveTableViewModel sv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _tableService.Add(sv);

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(EditSaveViewModel ev, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _tableService.UpdateTable(ev, id);
            return Ok(ev);
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

            var table = await _tableService.GetAll();
            if(table.Count == 0)
            {
                return StatusCode(StatusCodes.Status204NoContent, "There is not table");
            }
            return Ok(table);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByid(int id)
        {
            var table = await _tableService.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(table == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "The table doesn't exist");
            }
            
            return Ok(table);
        }

        [HttpGet("{tableId}/getTableOrdenById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTable(int tableId)
        {
            var table = await _tableService.GetTableOrden(tableId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (table == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "orden in proccess it doesn't exist in this table");
            }

            return Ok(table);
        }
    }
}
