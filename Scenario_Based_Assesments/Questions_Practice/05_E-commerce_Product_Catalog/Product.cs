namespace EcommerceProductCatalog
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }  // Electronics/Clothing/Books
        public double Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
