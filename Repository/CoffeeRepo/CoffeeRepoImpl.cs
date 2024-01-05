using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Coffee;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.CoffeeRepo
{
    public class CoffeeRepoImpl : ICoffeeRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public CoffeeRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.COFFEE));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public void deleteCoffee(int id)
        {
            List<Coffee> list = getAll();
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

        public Coffee? findById(int id)
        {
            return getAll().SingleOrDefault(addIn => addIn.Id == id);
        }

        public Coffee? findByName(string name)
        {
            return getAll().SingleOrDefault(coffee => coffee.Name.ToUpper() == name.ToUpper());
        }

        public List<Coffee> getAll()
        {
            return getFromDictionary.Select(e => new Coffee
            {
                Id = Convert.ToInt32(e["Id"]),
                Name = (e["Name"]).ToString(),
                Price = (double)e["Price"]
            }).ToList();
        }

        public void saveCoffee(Coffee coffee)
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
                worksheet.Cells[newRow, 1].Value = coffee.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = coffee.Name;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = (double)coffee.Price; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public void updateCoffee(Coffee coffee)
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

                    if (currentId == coffee.Id)
                    {
                        // Update the "Name" column for rows with "Id" equal to 2
                        worksheet.Cells[row, nameColumnIndex].Value = coffee.Name;
                        worksheet.Cells[row, priceColumnIndex].Value = (double)coffee.Price;
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
