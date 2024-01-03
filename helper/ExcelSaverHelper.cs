using BisleriumCafeBackend.Model.AddIn;
using ExcelDataReader;
using OfficeOpenXml;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace BisleriumCafeBackend.helper
{
    public class ExcelSaverHelper
    {
        public static void saveExcel(String fileName, AddIn addIn)
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
                        worksheet.Cells[newRow, 1].Value = addIn.Id;         // Assuming 'Id' is in column A
                        worksheet.Cells[newRow, 2].Value = addIn.Name;  // Assuming 'Name' is in column B
                        worksheet.Cells[newRow, 3].Value = addIn.Price; // Assuming 'Price' is in column C

                        // Save the changes to the Excel file
                        package.Save();
                    }
        }


        public static void updateExcel(String fileName, AddIn addIn)
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

                    if (currentId == addIn.Id)
                    {
                        // Update the "Name" column for rows with "Id" equal to 2
                        worksheet.Cells[row, nameColumnIndex].Value = addIn.Name;
                        worksheet.Cells[row, priceColumnIndex].Value = addIn.Price;
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
