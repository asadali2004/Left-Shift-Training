namespace RestaurantMenuManagement
{
    public class MenuItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }  // Appetizer/Main Course/Dessert
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
    }
}
