using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using OfficeOpenXml;
using System.Xml.Linq;

namespace BisleriumCafeBackend.Repository.MemberRepo
{
    public class MemberRepoImpl : IMemberRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public MemberRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.MEMBER));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteMember(int id)
        {
            List<Member> list = getAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id == id)
                {
                    string filePath = @fileName + ".xlsx";
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        // Access the first worksheet
                        var worksheet = package.Workbook.Worksheets[0];
                        worksheet.DeleteRow(i + 2);
                        package.Save();
                        break;
                    }
                }
            }
        }

        public Member? findByPhoneNumber(string contact)
        {
            return getAll().SingleOrDefault(member => member.PhoneNumber.ToUpper() == contact.ToUpper());
        }

        public Member? findById(int id)
        {
            return getAll().SingleOrDefault(member => member.Id == id);
        }

        public List<Member> getAll()
        {
            return getFromDictionary.Select(e => new Member
            {
                Id = Convert.ToInt32(e["Id"]),
                Name = (e["Name"]).ToString(),
                PhoneNumber = (e["PhoneNumber"]).ToString(),
                IsMember = Convert.ToBoolean(e["IsMember"]),
                CoffeeCount = Convert.ToInt32(e["CoffeeCount"]),
            }).ToList();
        }

        public void saveMember(Member member)
        {
            string filePath = @fileName + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                var worksheet = package.Workbook.Worksheets[0];

                // Determine the last used row
                int lastUsedRow = worksheet.Dimension.End.Row;

                // Increment the last used row to add a new row
                int newRow = lastUsedRow + 1;

                // Assuming 'id', 'Name', and 'Price' are variables with appropriate values
                worksheet.Cells[newRow, 1].Value = member.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = member.Name;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = member.PhoneNumber; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 4].Value = member.IsMember; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 5].Value = member.CoffeeCount; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public void updateMember(Member member)
        {
            string filePath = @fileName + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                var worksheet = package.Workbook.Worksheets[0];

                // Determine the last used row
                int idColumnIndex = 1; // Assuming "Id" is in the first column
                int nameColumnIndex = 2; // Assuming "Name" is in the third column
                int priceColumnIndex = 3; // Assuming "Name" is in the third column

                // Iterate through rows and update "Name" where "Id" is 2
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    int currentId = Convert.ToInt32(worksheet.Cells[row, idColumnIndex].Value);

                    if (currentId == member.Id)
                    {
                        // Update the "Name" column for rows with "Id" equal to 2
                        worksheet.Cells[row, 2].Value = member.Name;
                        worksheet.Cells[row, 3].Value = member.PhoneNumber; // Assuming 'Price' is in column C
                        worksheet.Cells[row, 4].Value = member.IsMember; // Assuming 'Price' is in column C
                        worksheet.Cells[row, 5].Value = member.CoffeeCount;
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
