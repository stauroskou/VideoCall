using VideoCall.Core.Account.Requests;

namespace VideoCall.Core.Interfaces;

public interface IAccountService
{
    Task<bool> RegisterUser(RegisterRequest request);
    bool LogoutUser(string userName);
}
