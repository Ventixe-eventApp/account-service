using Microsoft.AspNetCore.Identity;
using Presentation.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Services;

public class AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

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

    public async Task<AccountResult<LoginAccountResponse>> LoginAsync(LoginAccountRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new AccountResult<LoginAccountResponse>
            {
                Succeeded = false,
                StatusCode = 404,
                Error = "User not found"
            };
        }
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        if (!result.Succeeded)
        {
            return new AccountResult<LoginAccountResponse>
            {
                Succeeded = false,
                StatusCode = 401,
                Error = "Invalid email or password"
            };
        }
        return new AccountResult<LoginAccountResponse>
        {
            Succeeded = true,
            Result = new LoginAccountResponse
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

    public async Task<AccountResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AccountResult
        {
            Succeeded = true,
            StatusCode = 200
        };
    }
}

