using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.User;
using BisleriumCafeBackend.Services.Users;

namespace BisleriumCafeBackend.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        
        public LoginService(IUserService userService) {
            _userService = userService;
        } 
        public bool Login(User requestPojo)
        {
            List<User> users =  _userService.convertUserFromDictionary(ExcelLoaderHelper.GetExcelService(fileName: FileNameEnum.GetEnumDescription((FileNameEnum.FileName.LOGIN_DETAILS))));

            foreach (User user in users)
            {
                if(user.UserType == requestPojo.UserType)
                {
                    if (user.Password == requestPojo.Password) {
                        return true;
                    }
                }
                if(user.Password == requestPojo.Password && user.UserType == requestPojo.UserType)
                {
                    return true;
                }
            }

           

            throw new Exception("password is wrong");
        }
    }
}
