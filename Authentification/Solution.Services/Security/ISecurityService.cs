using ErrorOr;
using Solution.Domain.Models.Request.Security;
using Solution.Domain.Models.Responses;

namespace Solution.Services.Security;

public interface ISecurityService
{
    Task<ErrorOr<TokenResponseModel>> LoginAsync(LoginRequestModel model);

    Task<ErrorOr<Success>> RegiserAsync(RegisterRequestModel model);
}
