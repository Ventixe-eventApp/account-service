using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _accountService.RegisterAsync(request);

        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }


    [HttpGet("exists")]
    public async Task<IActionResult> EmailExists([FromQuery] string email)
    {
        var exists = await _accountService.AlreadyExistsAsync(email);

        return Ok(new AccountResult
        {
            Succeeded = exists
        });
    }
}
