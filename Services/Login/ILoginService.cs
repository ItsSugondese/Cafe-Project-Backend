using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.Model.User;

namespace BisleriumCafeBackend.Services.Login
{
    public interface ILoginService
    {
        bool Login(User user);
        void updatePassword(User requestPojo);
    }
}
