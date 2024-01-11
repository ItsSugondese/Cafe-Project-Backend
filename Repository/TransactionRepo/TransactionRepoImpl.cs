using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;
using BisleriumCafeBackend.pojo.Transaction;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.MemberRepo;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class TransactionRepoImpl : ITransactionRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        private readonly IOrderRepo _orderRepo;
        string fileName;

        public TransactionRepoImpl(IOrderRepo orderRepo)
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.TRANSACTION));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
            _orderRepo = orderRepo;
        }

        public List<Transaction> getAll()
        {
            return getFromDictionary.Select(e => new Transaction
            {
                Id = Convert.ToInt32(e["Id"]),
                MemberId = Convert.ToInt32(e["MemberId"]),
                OrderId = Convert.ToInt32(e["OrderId"]),
                Date =  DateOnly.FromDateTime(DateTime.Parse(e["Date"].ToString()))
            }).ToList();
        }

        public List<TransactionResponse> getAllTransactionDetails()
        {
            return getAll().Select(
            (e) => {
                OrderResponse orderData = _orderRepo.getSingleOrderDetails((int) e.Id);

                return new TransactionResponse
                {

                    id = (int)e.Id,
                    date = e.Date,
                    addInName = orderData.AddInName,
                    coffeeName = orderData.CoffeeName,
                    memberName = orderData.MemberName,
                    price = orderData.Price
                    };
                }
                ).ToList();
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
                worksheet.Cells[newRow, 4].Value = transaction.Date; // Assuming 'Price' is in column C
                

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
