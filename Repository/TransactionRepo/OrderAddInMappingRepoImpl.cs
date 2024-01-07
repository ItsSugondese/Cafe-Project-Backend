using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Transaction;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class OrderAddInMappingRepoImpl : IOrderAddInMappingRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public OrderAddInMappingRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.ORDER_ADD_IN));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteOrderAddIn(int id)
        {
            List<OrderAddInMapping> list = getAll();
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

        public OrderAddInMapping? findById(int id)
        {
            return getAll().SingleOrDefault(addInMapping => addInMapping.Id == id);
        }

        public List<OrderAddInMapping> findByOrderId(int id)
        {
            return getAll().Where(addInMapping => addInMapping.OrderId == id).ToList();
        }

        public List<OrderAddInMapping> getAll()
        {
            return getFromDictionary.Select(e => new OrderAddInMapping
            {
                Id = Convert.ToInt32(e["Id"]),
                OrderId = Convert.ToInt32(e["OrderId"]),
                AddInId = Convert.ToInt32(e["AddInId"])
            }).ToList();
        }

        public void saveOrderAddIn(OrderAddInMapping orderAddIn)
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
                worksheet.Cells[newRow, 1].Value = orderAddIn.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = orderAddIn.OrderId;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = orderAddIn.AddInId; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public void updateOrderAddIn(OrderAddInMapping orderAddIn)
        {
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
                    int currentId = Convert.ToInt32(worksheet.Cells[newRow, idColumnIndex].Value);

                    if (currentId == orderAddIn.Id)
                    {
                        // Assuming 'Id' is in column A
                        worksheet.Cells[newRow, 2].Value = orderAddIn.OrderId;  // Assuming 'Name' is in column B
                        worksheet.Cells[newRow, 3].Value = orderAddIn.AddInId; // Assuming 'Price' is in column 
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }
    }
}
