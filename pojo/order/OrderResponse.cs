namespace BisleriumCafeBackend.pojo.order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public int MemberId { get; set; }
        public DateOnly Date { get; set; } 
        public double Price { get; set; }
        public string CoffeeName { get; set; }
        public List<string> AddInName { get; set; }
        
    }
}
