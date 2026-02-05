````md
# Question 17: Inventory Stock Management

## Scenario
A warehouse needs to manage product inventory and track stock movements.

---

## Requirements

### Class: `Product`
- `string ProductCode`
- `string ProductName`
- `string Category`
- `string Supplier`
- `double UnitPrice`
- `int CurrentStock`
- `int MinimumStockLevel`

---

### Class: `StockMovement`
- `int MovementId`
- `string ProductCode`
- `DateTime MovementDate`
- `string MovementType` (In / Out)
- `int Quantity`
- `string Reason` (Purchase / Sale / Return)

---

### Class: `InventoryManager`

```csharp
public void AddProduct(string code, string name, string category,
                      string supplier, double price, int stock, int minLevel);
public bool UpdateStock(string productCode, string movementType,
                        int quantity, string reason);
public Dictionary<string, List<Product>> GroupProductsByCategory();
public List<Product> GetLowStockProducts();
public Dictionary<string, int> GetStockValueByCategory();
````

---

## Use Cases

* Add products with stock levels
* Update stock (in/out movements)
* Group products by category
* Identify low-stock items
* Calculate inventory value

