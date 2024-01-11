using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.Transaction;
using BisleriumCafeBackend.pojo.order;
using BisleriumCafeBackend.pojo.Transaction;
using BisleriumCafeBackend.Repository.MemberRepo;
using BisleriumCafeBackend.Repository.TransactionRepo;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace BisleriumCafeBackend.Services.TransactionService
{
    public class TransactionServiceImpl : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMemberRepo _memberRepo;
        private readonly ICoffeeRedeemCoupounRepo _coffeeRedeemCoupounRepo;
        public TransactionServiceImpl(ITransactionRepo transactionRepo, IOrderRepo orderRepo, IMemberRepo memberRepo, ICoffeeRedeemCoupounRepo coffeeRedeemCoupounRepo)
        {
            _transactionRepo = transactionRepo;
            _orderRepo = orderRepo;
            _memberRepo = memberRepo;
            _coffeeRedeemCoupounRepo = coffeeRedeemCoupounRepo;
        }

        public List<TransactionResponse> getTransactionDetails()
        {
            return _transactionRepo.getAllTransactionDetails();
        }

        public void saveTransaction(Transaction transaction)
        {
            Order order = _orderRepo.findById(transaction.OrderId);
            order.HasPaid = true;
            _orderRepo.updateOrder(order);

            Member member = _memberRepo.findById(transaction.MemberId);
            if (member.IsMember)
            {
                member.CoffeeCount = member.CoffeeCount + 1;
                _memberRepo.updateMember(member);
            }

            List<Transaction> transactionList = _transactionRepo.getAll();
            if (transaction.Id == null)
            {
                if (transactionList.Count() > 0)
                {
                    Transaction lastTransaction = transactionList.Last();
                    transaction.Id = lastTransaction.Id + 1;
                }
                else
                {
                    transaction.Id = 1;
                }
                transaction.Date = DateOnly.FromDateTime(DateTime.Now);
                _transactionRepo.saveTransaction(transaction);
            }
        }

        public void transactionDataForPdf()
        {
            string outputFile = "output.pdf";
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            
            List<Transaction> transactionList = _transactionRepo.getAll().Where(e => e.Date == today).ToList();
            Dictionary<string, int> coffeeCount = new Dictionary<string, int>();
            Dictionary<string, int> addInCount = new Dictionary<string, int>();
            double dayRevenue = 0;

            foreach (Transaction transaction in transactionList)
            {
                OrderResponse responseOrder =  _orderRepo.getSingleOrderDetails(transaction.OrderId);
                dayRevenue += responseOrder.Price;
                if (coffeeCount.ContainsKey(responseOrder.CoffeeName))
                {
                    coffeeCount[responseOrder.CoffeeName]++;
                }else
                {
                    coffeeCount[responseOrder.CoffeeName] = 1;
                }

                foreach(string addInName in responseOrder.AddInName)
                {
                    if (addInCount.ContainsKey(addInName))
                    {
                        addInCount[addInName]++;
                    }
                    else
                    {
                        addInCount[addInName] = 1;
                    }
                }

            }


            var sortedCoffeeDictionaryDesc = coffeeCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            var sortedAddInsDictionaryDesc = addInCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);




            List<Transaction> transactionMonthList = _transactionRepo.getAll().Where(e => e.Date.Month == today.Month).ToList();
            Dictionary<string, int> coffeeMonthCount = new Dictionary<string, int>();
            Dictionary<string, int> addInMonthCount = new Dictionary<string, int>();
            double monthRevenue = 0;
            foreach (Transaction transaction in transactionMonthList)
            {
                OrderResponse responseOrder = _orderRepo.getSingleOrderDetails(transaction.OrderId);
                monthRevenue += responseOrder.Price;
                if (coffeeMonthCount.ContainsKey(responseOrder.CoffeeName))
                {
                    coffeeMonthCount[responseOrder.CoffeeName]++;
                }
                else
                {
                    coffeeMonthCount[responseOrder.CoffeeName] = 1;
                }

                foreach (string addInName in responseOrder.AddInName)
                {
                    if (addInMonthCount.ContainsKey(addInName))
                    {
                        addInMonthCount[addInName]++;
                    }
                    else
                    {
                        addInMonthCount[addInName] = 1;
                    }
                }

            }


            var sortedCoffeeMonthDictionaryDesc = coffeeMonthCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            var sortedAddInsMonthDictionaryDesc = addInMonthCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);



            using (PdfWriter writer = new PdfWriter(outputFile).SetSmartMode(true))
            {
                // Create a PdfDocument object
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Create a Document object
                    var document = new Document(pdf);

                // Add date header
                    document.Add(new Paragraph($"Day ({today})").SetBold());
                document.Add(new Paragraph("Revenue (" + "Rs. " + dayRevenue.ToString() + ")").SetItalic());

                // Add Top 5 coffee
                AddTopList(document, "Top 5 coffee", sortedCoffeeDictionaryDesc);

                // Add Top 5 addins
                AddTopList(document, "Top 5 addins", sortedAddInsDictionaryDesc);

                    // Add spacing between day and month sections
                    document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));


                    document.Add(new Paragraph($"Month ({today.ToString("MMMM")})").SetBold());
                    document.Add(new Paragraph("Revenue (" + "Rs. " + monthRevenue.ToString() + ")"));
                    // Add month header

                // Add Top 5 coffee for the month
                AddTopList(document, "Top 5 coffee", sortedCoffeeMonthDictionaryDesc);

                // Add Top 5 addins for the month
                AddTopList(document, "Top 5 addins", sortedAddInsMonthDictionaryDesc);
                }
            }
            
        }

        private void AddTopList(Document document, string title, Dictionary<string, int> items)
        {
            // Add title
            document.Add(new Paragraph(title).SetBold());
            int i = 1;
            // Add items
            foreach (var item in items)
            {
                document.Add(new ListItem(item.Key + " (" + item.Value + ")"));

                if (i == 5)
                {
                    break;
                }
                i++;
            }
        }

        public List<string> GetTopCoffee()
        {
            // Replace this with your logic to fetch top coffee data
            return new List<string> { "Coffee 1", "Coffee 2", "Coffee 3", "Coffee 4", "Coffee 5" };
        }

        public List<string> GetTopAddins()
        {
            // Replace this with your logic to fetch top add-ins data
            return new List<string> { "Addin 1", "Addin 2", "Addin 3", "Addin 4", "Addin 5" };
        }
    }
}
