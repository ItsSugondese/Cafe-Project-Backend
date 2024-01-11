using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.AddIn;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;
using BisleriumCafeBackend.Repository.AddInRepo;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.MemberRepo;
using OfficeOpenXml;

namespace BisleriumCafeBackend.Repository.TransactionRepo
{
    public class OrderRepoImpl : IOrderRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;
        private readonly IAddInRepo _addInRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly ICoffeeRepo _coffeeRepo;
        private readonly IOrderAddInMappingRepo _orderAddInMappingRepo;

        public OrderRepoImpl(IAddInRepo addInRepo, ICoffeeRepo coffeeRepo, IMemberRepo memberRepo, IOrderAddInMappingRepo addInMappingRepo)
        {
            _addInRepo = addInRepo;
            _memberRepo = memberRepo;
            _coffeeRepo = coffeeRepo;
            _orderAddInMappingRepo = addInMappingRepo;

            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.ORDER));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }
        public void deleteOrder(int id)
        {
            List<Order> list = getAll();
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

        public Order? findById(int id)
        {
            return getAll().SingleOrDefault(order => order.Id == id);
        }

        public List<Order> getAll()
        {
            return getFromDictionary.Select(e => new Order
            {
                Id = Convert.ToInt32(e["Id"]),
                MemberId = Convert.ToInt32(e["MemberId"]),
                Date = DateOnly.Parse(e["Date"].ToString()),
                HadDiscount = Convert.ToBoolean(e["HadDiscount"]),
                WasRedeem = Convert.ToBoolean(e["WasRedeem"]),
                HadAddIn = Convert.ToBoolean(e["HadAddIn"]),
                Price = (double)e["Price"],
                CoffeeId = Convert.ToInt32(e["CoffeeId"]),
                RedeemId =  e["RedeemId"] == DBNull.Value? null : Convert.ToInt32(e["RedeemId"]),
                HasPaid=   Convert.ToBoolean(e["HasPaid"])
                
                
            }).ToList();
        }

        public void saveOrder(Order order)
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
                worksheet.Cells[newRow, 1].Value = order.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = order.MemberId;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = order.Date; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 4].Value = order.HadDiscount; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 5].Value = order.WasRedeem; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 6].Value = order.Price; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 7].Value = order.CoffeeId; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 8].Value = order.HadAddIn; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 9].Value = order.RedeemId; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 10].Value = order.HasPaid; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public void updateOrder(Order order)
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

                    if (currentId == order.Id)
                    {
                       // Assuming 'Id' is in column A
                        worksheet.Cells[newRow, 2].Value = order.MemberId;  // Assuming 'Name' is in column B
                        worksheet.Cells[newRow, 3].Value = order.Date; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 4].Value = order.HadDiscount; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 5].Value = order.WasRedeem; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 6].Value = order.Price; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 7].Value = order.CoffeeId; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 8].Value = order.HadAddIn; // Assuming 'Price' is in column C
                        worksheet.Cells[newRow, 9].Value = order.RedeemId;
                        worksheet.Cells[newRow, 10].Value = order.HasPaid;
                        break;
                    }
                }

                // Save the changes to the Excel file
                package.Save();
            }
        }

        public List<OrderResponse> getAllOrdersDetails()
        {
           return getAll().Where(filter => !filter.HasPaid).Select(
                (e) => {
                    List<string> addInsName = _orderAddInMappingRepo.findByOrderId((int)e.Id)
                    .Select(e => _addInRepo.findById(e.AddInId).Name).ToList();
                    Member member = _memberRepo.findById(e.MemberId);
                    return new OrderResponse
                    {
                        Id = (int)e.Id,
                        Date = e.Date,
                        AddInName = addInsName,
                        CoffeeName = _coffeeRepo.findById(e.CoffeeId).Name,
                        MemberName = member.Name,
                        Price = e.Price,
                        MemberId = (int) member.Id
                    };
                }
                ).ToList();
            }

        public OrderResponse getSingleOrderDetails(int id)
        {
            Order order = findById(id);
            
                    List<string> addInsName = _orderAddInMappingRepo.findByOrderId((int)order.Id).Select(e => _addInRepo.findById(e.AddInId).Name).ToList();
                    Member member = _memberRepo.findById(order.MemberId);
            return new OrderResponse
            {
                Id = (int)order.Id,
                Date = order.Date,
                AddInName = addInsName,
                CoffeeName = _coffeeRepo.findById(order.CoffeeId).Name,
                MemberName = member.Name,
                Price = order.Price,
                MemberId = (int)member.Id
            };
             
        }
    }
    }
