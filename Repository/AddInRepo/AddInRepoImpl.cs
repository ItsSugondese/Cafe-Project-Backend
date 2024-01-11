using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.AddIn;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.AddInRepo
{
    public class AddInRepoImpl : IAddInRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public AddInRepoImpl() {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.ADD_IN));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public void saveAddin(AddIn addIn)
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
                worksheet.Cells[newRow, 3].Value = (double) addIn.Price; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 4].Value =  addIn.FilePath; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
        }




        public AddIn? findById(int id)
        {
            return getAll().SingleOrDefault(addIn => addIn.Id == id);

        }

        public List<AddIn> getAll()
        {

            return getFromDictionary.Select(e => new AddIn
            {
                Id = Convert.ToInt32(e["Id"]),
                Name = (e["Name"]).ToString(),
                Price = (double)e["Price"],
                FilePath = (e["FilePath"]).ToString()
            }).ToList();
        }

        public void updateAddin(AddIn addIn)
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
                        worksheet.Cells[row, priceColumnIndex].Value = (double) addIn.Price;
                        worksheet.Cells[row, 4].Value =  addIn.FilePath;
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }


            public void deleteAddin(int id)
            {
                List<AddIn> list = getAll();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Id == id)
                    {
                    string filePath = @fileName + ".xlsx";
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                        {
                            // Access the first worksheet
                            var worksheet = package.Workbook.Worksheets[0];
                        worksheet.DeleteRow(i+2);
                        package.Save();
                        break; 
                        }
                    }
                }
            }

        public AddIn? findByName(string name)
        {
            return getAll().SingleOrDefault(addIn => addIn.Name.ToUpper() == name.ToUpper());
        }
    }
    }
