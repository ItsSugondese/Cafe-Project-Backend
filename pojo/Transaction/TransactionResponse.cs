namespace BisleriumCafeBackend.pojo.Transaction
{
    public class TransactionResponse
    {
        public int id { get; set; }
        public string memberName { get; set; }
        public string coffeeName { get; set; }
        public DateOnly date { get; set; }
        public List<string> addInName { get; set; }
        public double price { get; set; }
    }
}
