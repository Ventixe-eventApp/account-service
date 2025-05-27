
using Presentation.Models;

namespace Presentation.Services;

public interface IAccountService
{
    Task<bool> AlreadyExistsAsync(string email);
    Task<AccountResult> LoginAsync(LoginAccountRequest request);
    Task<AccountResult<RegisterAccountResponse>> RegisterAsync(RegisterAccountRequest request);
}
