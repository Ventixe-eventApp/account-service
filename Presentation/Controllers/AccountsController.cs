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
            return Ok(result.Result);
        }
        return BadRequest(result);
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _accountService.LoginAsync(request);
        if (result.Succeeded)
            return Ok(result);

        return StatusCode(result.StatusCode, new { message = result.Error ?? "Failed to log in" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
         var result = await _accountService.LogoutAsync();
        if(!result.Succeeded)
            return Ok(new  { Succeeded = true, Message = "Logged out successfully."  });

        return StatusCode(result.StatusCode, new { message = result.Error ?? "Logout failed" });
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
