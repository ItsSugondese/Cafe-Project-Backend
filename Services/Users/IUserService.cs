using BisleriumCafeBackend.Model.User;

namespace BisleriumCafeBackend.Services.Users
{
    public interface IUserService
    {
        List<User> convertUserFromDictionary(List<Dictionary<string, object>> dic);
    }
}
