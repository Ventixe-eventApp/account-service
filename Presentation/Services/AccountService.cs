using Microsoft.AspNetCore.Identity;
using Presentation.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager) : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<AccountResult<RegisterAccountResponse>> RegisterAsync(RegisterAccountRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new AccountResult<RegisterAccountResponse>
            {
                Succeeded = false,
                StatusCode = 400,

            };
        }
        return new AccountResult<RegisterAccountResponse>
        {
            Succeeded = true,
            Result = new RegisterAccountResponse
            {
                UserId = user.Id
            }
        };
    }

    public async Task<bool> AlreadyExistsAsync(string email)
    {
        var exists = await _userManager.FindByEmailAsync(email);
        if (exists != null)
            return true;

        return false;



    }
}

