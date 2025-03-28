using VideoCall.Core.Account.Requests;
using VideoCall.Core.Entities;
using VideoCall.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace VideoCall.Application.Account;

public class AccountService(UserManager<User> _userManager) : IAccountService
{
    public bool LogoutUser(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterUser(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null) {
            return false;
        }
        var user = new User
        {
            UserName = request.UserName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return false;
        }

        return true;
    }
}
