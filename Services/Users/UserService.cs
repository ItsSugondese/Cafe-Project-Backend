using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.Model.User;

namespace BisleriumCafeBackend.Services.Users
{
    public class UserService : IUserService
    {
        public UserService() { }

        public List<User> convertUserFromDictionary(List<Dictionary<string, object>> dic)
        {
            return dic.Select(e => new User
            {
                UserType = Enum.Parse<UserType>((String) e["UserType"], true),
                Password = (string)e["Password"],
            }).ToList();
        }
    }
}
