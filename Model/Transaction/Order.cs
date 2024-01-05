namespace BisleriumCafeBackend.Model.Transaction
{
    public class Order
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
    }
}
