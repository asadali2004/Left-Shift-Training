namespace EmpMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsInStock { get; set; }
        public int CountOfProduct { get; set; }

    }
}
