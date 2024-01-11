using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.Model.User;
using BisleriumCafeBackend.Services.Users;
using OfficeOpenXml;
using static BisleriumCafeBackend.constants.FileNameEnum;

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

        public void updatePassword(User requestPojo)
        {
            List<User> users = _userService.convertUserFromDictionary(ExcelLoaderHelper.GetExcelService(fileName: FileNameEnum.GetEnumDescription((FileNameEnum.FileName.LOGIN_DETAILS))));

            string filePath = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.LOGIN_DETAILS)) + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                var worksheet = package.Workbook.Worksheets[0];

                // Determine the last used row
                int idColumnIndex = 1; // Assuming "Id" is in the first column


                // Iterate through rows and update "Name" where "Id" is 2
                for (int newRow = worksheet.Dimension.Start.Row + 1; newRow <= worksheet.Dimension.End.Row; newRow++)
                {
                    string role = (worksheet.Cells[newRow, idColumnIndex].Value).ToString();

                    if (role == requestPojo.UserType.ToString())
                    {
                        // Assuming 'Id' is in column A
                        worksheet.Cells[newRow, 2].Value = requestPojo.Password;  // Assuming 'Name' is in column 
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
