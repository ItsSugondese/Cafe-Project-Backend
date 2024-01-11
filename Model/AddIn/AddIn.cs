namespace BisleriumCafeBackend.Model.AddIn
{
    public class AddIn
    {
        public int? Id { get; set; } 
        public string Name { get; set; }        
        public double Price { get; set; }
        public string FilePath { get; set; }

        public AddIn() { }
    }
}
