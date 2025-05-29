
using Presentation.Models;

namespace Presentation.Services;

public interface IAccountService
{
    Task<bool> AlreadyExistsAsync(string email);
   
    Task<AccountResult<LoginAccountResponse>> LoginAsync(LoginAccountRequest request);
    Task<AccountResult> LogoutAsync();
    Task<AccountResult<RegisterAccountResponse>> RegisterAsync(RegisterAccountRequest request);
}
