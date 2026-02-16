# ADO.NET Architecture Guide - For Beginners

## 📚 Quick Overview

ADO.NET has **TWO main architectures** for working with databases:

### 1️⃣ **Connected Architecture** 
- Like talking to someone on the **phone** - you need to stay connected while talking
- File: `ConnectedArch.cs`

### 2️⃣ **Disconnected Architecture**
- Like using **email** - download messages, work offline, send replies later
- File: `DisconnectedArch.cs`

---

## 🔌 Connected Architecture (ConnectedArch.cs)

### How it Works:
```
1. Open Connection → 2. Read/Write Data → 3. Close Connection
    (MUST STAY CONNECTED)
```

### Key Components:
- **SqlConnection** - Opens connection to database
- **SqlCommand** - Executes SQL queries
- **SqlDataReader** - Reads data (forward-only, read-only)
- **ExecuteNonQuery()** - For INSERT/UPDATE/DELETE
- **ExecuteScalar()** - Returns single value (COUNT, SUM, etc.)
- **ExecuteReader()** - Returns SqlDataReader for SELECT

### Code Example:
```csharp
using (SqlConnection con = new SqlConnection(connectionString))
{
    string query = "SELECT * FROM Customers WHERE CustomerId = @Id";
    SqlCommand cmd = new SqlCommand(query, con);
    cmd.Parameters.AddWithValue("@Id", 101);
    
    con.Open();  // 🔓 Connection opens
    
    SqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine(reader["FullName"]);
    }
    reader.Close();
} // 🔒 Connection closes
```

### When to Use:
✅ Web applications (ASP.NET)  
✅ Real-time data requirement  
✅ Large datasets (reads row-by-row)  
✅ Simple read operations  
✅ When you need latest data always  

### Pros & Cons:
| ✅ Advantages | ❌ Disadvantages |
|--------------|------------------|
| Fast and lightweight | Must stay connected |
| Low memory usage | Cannot work offline |
| Always fresh data | Forward-only reading |
| Simple to use | Read-only with DataReader |

---

## 💾 Disconnected Architecture (DisconnectedArch.cs)

### How it Works:
```
1. Open Connection → 2. Load Data into DataSet → 3. Close Connection
                    ↓
4. Work with DataSet (Offline) → 5. Open Connection → 6. Send Changes → 7. Close
```

### Key Components:
- **SqlConnection** - Opens connection to database
- **SqlDataAdapter** - Bridge between database and DataSet
- **DataSet** - In-memory "mini-database" (can hold multiple tables)
- **DataTable** - Single table in DataSet
- **DataRow** - Single row in DataTable
- **SqlCommandBuilder** - Auto-generates INSERT/UPDATE/DELETE commands

### Code Example:
```csharp
DataSet ds = new DataSet();

// STEP 1: Load data (connection opens and closes automatically)
using (SqlConnection con = new SqlConnection(connectionString))
using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", con))
{
    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    adapter.Fill(ds);  // Loads data and closes connection
}

// STEP 2: Work offline (connection is CLOSED)
DataRow newRow = ds.Tables[0].NewRow();
newRow["FullName"] = "John Doe";
newRow["City"] = "Delhi";
ds.Tables[0].Rows.Add(newRow);

// STEP 3: Save changes back to database
using (SqlConnection con = new SqlConnection(connectionString))
using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", con))
{
    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
    adapter.Update(ds);  // Sends changes to database
}
```

### When to Use:
✅ Desktop applications (WinForms, WPF)  
✅ Need to work offline  
✅ Multiple table operations  
✅ Need to navigate data back and forth  
✅ Batch updates (multiple changes at once)  

### Pros & Cons:
| ✅ Advantages | ❌ Disadvantages |
|--------------|------------------|
| Works offline | Uses more memory |
| Multiple tables support | Slower for large data |
| Can navigate freely | More complex |
| Batch operations | Not always fresh data |

---

## 🆚 Quick Comparison Table

| Feature | Connected | Disconnected |
|---------|-----------|--------------|
| **Connection** | Stays open | Opens/closes as needed |
| **Main Classes** | SqlDataReader | DataSet, DataAdapter |
| **Memory Usage** | Low (streams data) | High (loads everything) |
| **Speed** | Faster | Slower |
| **Offline Work** | ❌ No | ✅ Yes |
| **Multiple Tables** | ❌ No | ✅ Yes |
| **Navigation** | Forward only | Any direction |
| **Data Freshness** | Always latest | May be stale |
| **Best For** | Web apps | Desktop apps |
| **Updates** | One by one | Batch updates |

---

## 🎯 When to Choose What?

### Choose **Connected** Architecture when:
- Building a **web application** (ASP.NET Core, MVC)
- You need **real-time data** (dashboard, live reports)
- Working with **large datasets** (millions of rows)
- Performing **simple read operations**
- Database server is **always available**

### Choose **Disconnected** Architecture when:
- Building a **desktop application** (WinForms, WPF)
- Users need to **work offline**
- Need to **edit multiple records** before saving
- Working with **related tables** (Parent-Child relationships)
- Want to **minimize database server load**

---

## 📖 Key ADO.NET Methods Summary

### ExecuteNonQuery() ⚡
```csharp
int rowsAffected = cmd.ExecuteNonQuery();
```
- **Use for:** INSERT, UPDATE, DELETE
- **Returns:** Number of rows affected
- **Example:** Insert new customer, update price, delete order

### ExecuteReader() 📖
```csharp
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read()) { ... }
```
- **Use for:** SELECT queries
- **Returns:** SqlDataReader (forward-only cursor)
- **Example:** Read customer list, get order details

### ExecuteScalar() 1️⃣
```csharp
int count = (int)cmd.ExecuteScalar();
```
- **Use for:** Aggregate queries (COUNT, SUM, AVG, MAX, MIN)
- **Returns:** First column of first row
- **Example:** Get total customers, find max price

---

## 🛡️ Best Practices

### 1. Always Use Parameters (Prevent SQL Injection)
```csharp
// ❌ WRONG - SQL Injection risk!
string query = $"SELECT * FROM Users WHERE Username = '{username}'";

// ✅ CORRECT - Safe!
string query = "SELECT * FROM Users WHERE Username = @Username";
cmd.Parameters.AddWithValue("@Username", username);
```

### 2. Use `using` Statement (Auto-close connections)
```csharp
// ✅ CORRECT - Connection auto-closes
using (SqlConnection con = new SqlConnection(cs))
{
    con.Open();
    // Your code
} // Connection closes here automatically
```

### 3. Handle Exceptions
```csharp
try
{
    con.Open();
    // Database operations
}
catch (SqlException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

### 4. Use Transactions for Multiple Operations
```csharp
SqlTransaction transaction = con.BeginTransaction();
try
{
    // Multiple operations
    transaction.Commit();
}
catch
{
    transaction.Rollback();
}
```

---

## 🚀 Running the Examples

### Run Connected Architecture:
1. Comment out `Main()` in `DisconnectedArch.cs`
2. Keep `Main()` active in `ConnectedArch.cs`
3. Run: `dotnet run`

### Run Disconnected Architecture:
1. Comment out `Main()` in `ConnectedArch.cs`
2. Keep `Main()` active in `DisconnectedArch.cs`
3. Run: `dotnet run`

---

## 📝 Practice Exercises

1. **Easy:** Modify `SelectAllCustomers()` to filter by City
2. **Medium:** Add a method to get customers by Segment using DataSet
3. **Hard:** Implement a method that updates multiple customers in one transaction

---

## 🎓 Learning Path

1. ✅ Start with **Connected Architecture** (simpler)
2. ✅ Practice CRUD operations with SqlDataReader
3. ✅ Learn **parameterized queries** (security!)
4. ✅ Move to **Disconnected Architecture**
5. ✅ Understand DataSet, DataTable, DataRow
6. ✅ Practice with **SqlDataAdapter** and **SqlCommandBuilder**
7. ✅ Learn **Transactions** for data integrity

---

## 🆘 Common Errors & Solutions

### Error: "Cannot open database"
**Solution:** Check connection string, ensure database exists

### Error: "Login failed for user"
**Solution:** Grant database permissions or use correct credentials

### Error: "SqlDataReader is already open"
**Solution:** Close previous reader before opening new one

### Error: "Column does not belong to table"
**Solution:** Check spelling, ensure column exists in database

---

## 📚 Additional Resources

- [Microsoft ADO.NET Documentation](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/)
- Practice with your `Customer-Order` database
- Try creating similar code for `Orders` table

---

**Happy Learning! 🎉**
