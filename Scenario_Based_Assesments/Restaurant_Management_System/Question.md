# Question 4: Restaurant Menu Management

## 📘 Scenario
A restaurant needs to manage menu items and categorize them by course.

## 🛠️ Requirements

### In class MenuItem, implement the following properties:
- `string ItemName`
- `string Category` (Appetizer/Main Course/Dessert)
- `double Price`
- `bool IsVegetarian`

### In class MenuManager, implement the following methods:

#### Method 1
```csharp
public void AddMenuItem(string name, string category, double price, bool isVeg)
```
- Adds item with validation for price > 0

#### Method 2
```csharp
public Dictionary<string, List<MenuItem>> GroupItemsByCategory()
```
- Groups items by category

#### Method 3
```csharp
public List<MenuItem> GetVegetarianItems()
```
- Returns all vegetarian items

#### Method 4
```csharp
public double CalculateAveragePriceByCategory(string category)
```
- Returns average price of items in category

## Sample Use Cases
1. Add appetizers, main courses, desserts
2. Display menu categorized by course type
3. Show vegetarian-only menu
4. Calculate average prices per category
