namespace BisleriumCafeBackend.pojo.order
{
    public class OrderRequestPojo
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public int? ReedemId { get; set; }
        public double Price { get; set; }
        public int CoffeeId { get; set; }
        public List<int> addInIds = new List<int>();

    }
}
