
using Presentation.Models;

namespace Presentation.Services;

public interface IAccountService
{
    Task<bool> AlreadyExistsAsync(string email);
    Task<AccountResult<RegisterAccountResponse>> RegisterAsync(RegisterAccountRequest request);
}
