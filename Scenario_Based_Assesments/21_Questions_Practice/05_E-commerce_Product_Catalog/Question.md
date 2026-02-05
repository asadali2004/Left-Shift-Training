# Question 5: E-commerce Product Catalog

## Scenario
An e-commerce store needs to manage products and categorize them for customers.

## Requirements

### In class Product:
- `string ProductCode`
- `string ProductName`
- `string Category` (Electronics/Clothing/Books)
- `double Price`
- `int StockQuantity`

### In class InventoryManager:

#### Method 1
```csharp
public void AddProduct(string name, string category, double price, int stock)
```
- Auto-generate ProductCode (P001, P002...)

#### Method 2
```csharp
public SortedDictionary<string, List<Product>> GroupProductsByCategory()
```
- Groups products by category

#### Method 3
```csharp
public bool UpdateStock(string productCode, int quantity)
```
- Updates stock, returns false if insufficient stock

#### Method 4
```csharp
public List<Product> GetProductsBelowPrice(double maxPrice)
```
- Returns products below specified price

#### Method 5
```csharp
public Dictionary<string, int> GetCategoryStockSummary()
```
- Returns total stock quantity per category

## Sample Use Cases:
1. Add products to different categories
2. Display products grouped by category
3. Update stock after sales
4. Find products under budget
5. Show inventory summary
