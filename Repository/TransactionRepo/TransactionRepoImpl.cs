using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class TransactionRepoImpl : ITransactionRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public TransactionRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.TRANSACTION));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public List<Transaction> getAll()
        {
            return getFromDictionary.Select(e => new Transaction
            {
                Id = Convert.ToInt32(e["Id"]),
                MemberId = Convert.ToInt32(e["MemberId"]),
                OrderId = Convert.ToInt32(e["OrderId"])
            }).ToList();
        }

        public void saveTransaction(Transaction transaction)
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
                worksheet.Cells[newRow, 1].Value = transaction.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = transaction.OrderId;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = transaction.MemberId; // Assuming 'Price' is in column C
                

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
