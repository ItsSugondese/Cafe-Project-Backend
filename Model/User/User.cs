using BisleriumCafeBackend.enums;

namespace BisleriumCafeBackend.Model.User
{
    public class User
    {
        public UserType UserType { get; set; }
        public string Password { get; set; }
    }
}
