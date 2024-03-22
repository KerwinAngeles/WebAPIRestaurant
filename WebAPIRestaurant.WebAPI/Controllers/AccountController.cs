using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRestaurant.Core.Application.Dtos.Account;
using WebAPIRestaurant.Core.Application.Interfaces.Services;

namespace WebAPIRestaurant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticateRequest)
        {
            return Ok( await _accountService.AuthenticateAsync(authenticateRequest));
        }

        [HttpPost("RegisterWaiter")]
        public async Task<IActionResult> RegisterWaiter(RegisterRequest registerRequest)
        {
            return Ok(await _accountService.RegisterWaiterAsync(registerRequest));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("RegisterAdministrator")]
        public async Task<IActionResult> RegisterAdministrator(RegisterRequest registerRequest)
        {
            return Ok(await _accountService.RegisterAdministratorAsync(registerRequest));
        }
    }
}
