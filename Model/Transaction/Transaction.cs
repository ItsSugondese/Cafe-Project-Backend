namespace BisleriumCafeBackend.Model.Transaction
{
    public class Transaction
    {
        public int? Id { get; set; }
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; }
    }
}
