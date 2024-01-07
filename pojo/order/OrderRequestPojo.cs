namespace BisleriumCafeBackend.pojo.order
{
    public class OrderRequestPojo
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
        public bool HadDiscount { get; set; }
        public bool WasRedeem { get; set; }
        public double Price { get; set; }
        public int CoffeeId { get; set; }
        public bool HadAddIn {  get; set; }
        public int? RedeemId { get; set; }
        public List<int> AddInsId { get; set; }

        public OrderRequestPojo()
        {
            // Initialize AddInsId in the constructor
            AddInsId = new List<int>();
        }

    }
}
