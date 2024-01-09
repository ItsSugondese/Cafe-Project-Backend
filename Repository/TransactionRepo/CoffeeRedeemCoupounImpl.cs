using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class CoffeeRedeemCoupounImpl : ICoffeeRedeemCoupounRepo
    {

        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public CoffeeRedeemCoupounImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.COFFEE_REDEEM));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public void deleteCoffeeRedeem(int id)
        {
            throw new NotImplementedException();
        }

        public CoffeeRedeemCoupoun findById(Guid id)
        {
            return getAll().SingleOrDefault(crc => crc.Id == id);
        }

        public CoffeeRedeemCoupoun findByMemberId(int id)
        {
            return getAll().SingleOrDefault(crc => crc.MemberId == id);
        }

        public List<CoffeeRedeemCoupoun> getAll()
        {
            return getFromDictionary.Select(e => new CoffeeRedeemCoupoun
            {
                Id = (Guid) e["Id"],
                MemberId = Convert.ToInt32(e["MemberId"]),
                Date = (DateOnly) e["Date"],
                IsRedeem = Convert.ToBoolean(e["IsRedeem"])
            }).ToList();
        }

        public void saveCoffeeRedeem(CoffeeRedeemCoupoun crc)
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
                worksheet.Cells[newRow, 1].Value = crc.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = crc.MemberId;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = crc.Date; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 4].Value = crc.IsRedeem; // Assuming 'Price' is in column C
                

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public void updateCoffeeRedeem(Guid id)
        {
            CoffeeRedeemCoupoun crc =  findById(id);
            string filePath = @fileName + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                var worksheet = package.Workbook.Worksheets[0];

                // Determine the last used row
                int idColumnIndex = 1; // Assuming "Id" is in the first column


                // Iterate through rows and update "Name" where "Id" is 2
                for (int newRow = worksheet.Dimension.Start.Row + 1; newRow <= worksheet.Dimension.End.Row; newRow++)
                {
                    Guid currentId = (Guid) (worksheet.Cells[newRow, idColumnIndex].Value);

                    if (currentId == crc.Id)
                    {
                        // Assuming 'Id' is in column A
                        worksheet.Cells[newRow, 2].Value = crc.MemberId;  // Assuming 'Name' is in column B
                        worksheet.Cells[newRow, 3].Value = crc.Date; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 4].Value = crc.IsRedeem; // Assuming 'Price' is in column 
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
