using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

// ============================================================
// CONNECTION STRING - Customers Table
// ============================================================
string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Customer–Order;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

Console.WriteLine("========== ADO.NET & LINQ TO DATATABLE PRACTICE ==========\n");

// ============================================================
// CONCEPT 1: RETRIEVE DATA USING SqlDataAdapter & DataTable
// ============================================================
Console.WriteLine("\n--- CONCEPT 1: SqlDataAdapter & DataTable (Best for Multiple Operations) ---");
DataTable customers = new DataTable();

using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM Customers", con))
using (var da = new SqlDataAdapter(cmd))
{
    con.Open();
    da.Fill(customers); // ✅ Data copied into memory
    con.Close(); // Connection closed, but data remains
}

Console.WriteLine($"Total customers loaded: {customers.Rows.Count}");
foreach (DataRow row in customers.Rows)
{
    Console.WriteLine($"  {row["CustomerId"]} | {row["FullName"]} | {row["City"]} | {row["Segment"]} | Active: {row["IsActive"]} | Created: {row["CreatedOn"]}");
}

// ============================================================
// CONCEPT 2: RETRIEVE DATA USING SqlDataReader
// ============================================================
Console.WriteLine("\n--- CONCEPT 2: SqlDataReader (Best for Forward-Only Sequential Reading) ---");
string filterCity = "Mumbai";

using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("SELECT CustomerId, FullName, City, Segment FROM Customers WHERE City = @City", con))
{
    cmd.Parameters.AddWithValue("@City", filterCity);
    con.Open();
    
    using (var reader = cmd.ExecuteReader())
    {
        Console.WriteLine($"Customers in {filterCity}:");
        while (reader.Read())
        {
            Console.WriteLine($"  {reader["FullName"]} | Segment: {reader["Segment"]}");
        }
    }
}

// ============================================================
// CONCEPT 3: AGGREGATE FUNCTIONS - COUNT
// ============================================================
Console.WriteLine("\n--- CONCEPT 3: Aggregate - COUNT ---");
using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Customers", con))
{
    con.Open();
    int totalCustomers = (int)cmd.ExecuteScalar();
    Console.WriteLine($"Total customers in database: {totalCustomers}");
}

// ============================================================
// CONCEPT 4: AGGREGATE FUNCTIONS - SUM, AVG
// ============================================================
Console.WriteLine("\n--- CONCEPT 4: Aggregate - SUM & AVG ---");
using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("SELECT COUNT(*) as ActiveCount FROM Customers WHERE IsActive = 1", con))
{
    con.Open();
    int activeCount = (int)cmd.ExecuteScalar();
    Console.WriteLine($"Active customers: {activeCount}");
}

// ============================================================
// CONCEPT 5: INSERT NEW RECORD
// ============================================================
Console.WriteLine("\n--- CONCEPT 5: INSERT Operation ---");
using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand(@"
    INSERT INTO Customers (CustomerId, FullName, City, Segment, IsActive, CreatedOn)
    VALUES (@CustomerId, @Name, @City, @Segment, @IsActive, @CreatedOn)", con))
{
    cmd.Parameters.AddWithValue("@CustomerId", 7000 + new Random().Next(1, 1000)); // Generate unique ID
    cmd.Parameters.AddWithValue("@Name", "Rahul Sharma");
    cmd.Parameters.AddWithValue("@City", "Bangalore");
    cmd.Parameters.AddWithValue("@Segment", "Premium");
    cmd.Parameters.AddWithValue("@IsActive", true);
    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.Date);
    
    con.Open();
    int rowsInserted = cmd.ExecuteNonQuery();
    Console.WriteLine($"✅ Inserted {rowsInserted} row(s)");
}

// ============================================================
// CONCEPT 6: UPDATE EXISTING RECORD
// ============================================================
Console.WriteLine("\n--- CONCEPT 6: UPDATE Operation ---");
int customerId = 2;
string newSegment = "Gold";

using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("UPDATE Customers SET Segment = @Segment WHERE CustomerId = @Id", con))
{
    cmd.Parameters.AddWithValue("@Segment", newSegment);
    cmd.Parameters.AddWithValue("@Id", customerId);
    
    con.Open();
    int rowsUpdated = cmd.ExecuteNonQuery();
    Console.WriteLine($"✅ Updated {rowsUpdated} row(s) - Customer {customerId} now in {newSegment} segment");
}

// ============================================================
// CONCEPT 7: DELETE RECORD
// ============================================================
Console.WriteLine("\n--- CONCEPT 7: DELETE Operation ---");
// int customerIdToDelete = 5;
// 
// using (var con = new SqlConnection(connectionString))
// using (var cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @Id", con))
// {
//     cmd.Parameters.AddWithValue("@Id", customerIdToDelete);
//     con.Open();
//     int rowsDeleted = cmd.ExecuteNonQuery();
//     Console.WriteLine($"✅ Deleted {rowsDeleted} row(s)");
// }

// ============================================================
// CONCEPT 8: PARAMETERIZED QUERIES (SQL INJECTION PREVENTION)
// ============================================================
Console.WriteLine("\n--- CONCEPT 8: Parameterized Queries (Security) ---");
string searchCity = "Delhi";

using (var con = new SqlConnection(connectionString))
using (var cmd = new SqlCommand("SELECT FullName, City FROM Customers WHERE City = @City AND IsActive = @IsActive", con))
{
    // Use parameters to prevent SQL injection attacks
    cmd.Parameters.AddWithValue("@City", searchCity);
    cmd.Parameters.AddWithValue("@IsActive", true);
    
    con.Open();
    using (var reader = cmd.ExecuteReader())
    {
        Console.WriteLine($"Active customers in {searchCity}:");
        while (reader.Read())
        {
            Console.WriteLine($"  {reader["FullName"]}");
        }
    }
}

// ============================================================
// CONCEPT 9: TRANSACTION MANAGEMENT
// ============================================================
Console.WriteLine("\n--- CONCEPT 9: Transactions (COMMIT/ROLLBACK) ---");
using (var con = new SqlConnection(connectionString))
{
    con.Open();
    using (var tx = con.BeginTransaction())
    {
        try
        {
            // Step 1: Insert new customer
            using (var insertCmd = new SqlCommand(@"
                INSERT INTO Customers (FullName, City, Segment, IsActive, CreatedOn)
                VALUES (@Name, @City, @Segment, @IsActive, @CreatedOn)", con, tx))
            {
                insertCmd.Parameters.AddWithValue("@Name", "Priya Verma");
                insertCmd.Parameters.AddWithValue("@City", "Chennai");
                insertCmd.Parameters.AddWithValue("@Segment", "Standard");
                insertCmd.Parameters.AddWithValue("@IsActive", true);
                insertCmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.Date);
                insertCmd.ExecuteNonQuery();
            }

            // Step 2: Update another customer's segment
            using (var updateCmd = new SqlCommand("UPDATE Customers SET Segment = 'VIP' WHERE CustomerId = 1", con, tx))
            {
                updateCmd.ExecuteNonQuery();
            }

            tx.Commit();
            Console.WriteLine("✅ Transaction COMMITTED - Both operations succeeded");
        }
        catch (Exception ex)
        {
            tx.Rollback();
            Console.WriteLine($"❌ Transaction ROLLED BACK - Error: {ex.Message}");
        }
    }
}

// ============================================================
// CONCEPT 10: ERROR HANDLING & EXCEPTION MANAGEMENT
// ============================================================
Console.WriteLine("\n--- CONCEPT 10: Exception Handling ---");
try
{
    using (var con = new SqlConnection(connectionString))
    using (var cmd = new SqlCommand("SELECT TOP 5 FullName, Segment FROM Customers ORDER BY CreatedOn DESC", con))
    {
        con.Open();
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("Latest 5 customers:");
            while (reader.Read())
            {
                Console.WriteLine($"  {reader["FullName"]} | {reader["Segment"]}");
            }
        }
    }
}
catch (SqlException ex)
{
    Console.WriteLine($"❌ SQL Database Error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ General Error: {ex.Message}");
}

// ============================================================
// CONCEPT 11: LINQ TO DATATABLE - WHERE CLAUSE
// ============================================================
Console.WriteLine("\n--- CONCEPT 11: LINQ - WHERE (Filtering) ---");
var activeCustomers = customers.AsEnumerable()
    .Where(r => r.Field<bool>("IsActive") == true)
    .Select(r => r.Field<string>("FullName"))
    .ToList();

Console.WriteLine($"Active customers ({activeCustomers.Count}):");
activeCustomers.ForEach(name => Console.WriteLine($"  {name}"));

// ============================================================
// CONCEPT 12: LINQ TO DATATABLE - SELECT (Projection)
// ============================================================
Console.WriteLine("\n--- CONCEPT 12: LINQ - SELECT (Projection/Anonymous Types) ---");
var premiumCustomers = customers.AsEnumerable()
    .Where(r => r.Field<string>("Segment") == "Premium")
    .Select(r => new
    {
        Id = r.Field<int>("CustomerId"),
        Name = r.Field<string>("FullName"),
        City = r.Field<string>("City"),
        JoinedDate = r.Field<DateTime>("CreatedOn")
    })
    .ToList();

Console.WriteLine("Premium customers:");
foreach (var cust in premiumCustomers)
{
    Console.WriteLine($"  [{cust.Id}] {cust.Name} | {cust.City} | Joined: {cust.JoinedDate:yyyy-MM-dd}");
}

// ============================================================
// CONCEPT 13: LINQ TO DATATABLE - FILTERING BY CITY
// ============================================================
Console.WriteLine("\n--- CONCEPT 13: LINQ - WHERE with String Comparison ---");
string targetCity = "Mumbai";
var mumbaiBased = customers.AsEnumerable()
    .Where(r => r.Field<string>("City") == targetCity)
    .Select(r => r.Field<string>("FullName"))
    .ToList();

Console.WriteLine($"Customers in {targetCity}: {string.Join(", ", mumbaiBased)}");

// ============================================================
// CONCEPT 14: LINQ TO DATATABLE - ORDER BY
// ============================================================
Console.WriteLine("\n--- CONCEPT 14: LINQ - ORDER BY (Sorting) ---");
var sortedCustomers = customers.AsEnumerable()
    .OrderByDescending(r => r.Field<DateTime>("CreatedOn"))
    .ThenBy(r => r.Field<string>("FullName"))
    .Select(r => new
    {
        Name = r.Field<string>("FullName"),
        City = r.Field<string>("City"),
        Joined = r.Field<DateTime>("CreatedOn")
    })
    .ToList();

Console.WriteLine("Sorted by Join Date (Newest First), then by Name:");
foreach (var cust in sortedCustomers)
{
    Console.WriteLine($"  {cust.Name} | {cust.City} | {cust.Joined:yyyy-MM-dd}");
}

// ============================================================
// CONCEPT 15: LINQ TO DATATABLE - GROUP BY
// ============================================================
Console.WriteLine("\n--- CONCEPT 15: LINQ - GROUP BY (Aggregation) ---");
var byCity = customers.AsEnumerable()
    .GroupBy(r => r.Field<string>("City"))
    .Select(g => new
    {
        City = g.Key,
        TotalCustomers = g.Count(),
        ActiveCount = g.Count(x => x.Field<bool>("IsActive")),
        Segments = string.Join(", ", g.Select(x => x.Field<string>("Segment")).Distinct())
    })
    .OrderByDescending(x => x.TotalCustomers)
    .ToList();

Console.WriteLine("Customer Count by City:");
foreach (var group in byCity)
{
    Console.WriteLine($"  {group.City}: {group.TotalCustomers} total, {group.ActiveCount} active | Segments: {group.Segments}");
}

// ============================================================
// CONCEPT 16: LINQ TO DATATABLE - AGGREGATE FUNCTIONS
// ============================================================
Console.WriteLine("\n--- CONCEPT 16: LINQ - Aggregate Functions (COUNT, DISTINCT) ---");
int premiumCount = customers.AsEnumerable()
    .Count(r => r.Field<string>("Segment") == "Premium");

int uniqueCities = customers.AsEnumerable()
    .Select(r => r.Field<string>("City"))
    .Distinct()
    .Count();

Console.WriteLine($"Premium customers: {premiumCount}");
Console.WriteLine($"Cities represented: {uniqueCities}");

// ============================================================
// CONCEPT 17: LINQ TO DATATABLE - COMPLEX QUERIES
// ============================================================
Console.WriteLine("\n--- CONCEPT 17: LINQ - Complex Multi-Condition Queries ---");
var complexQuery = customers.AsEnumerable()
    .Where(r => r.Field<bool>("IsActive") && r.Field<string>("Segment") != "Standard")
    .GroupBy(r => r.Field<string>("Segment"))
    .Select(g => new
    {
        Segment = g.Key,
        Count = g.Count(),
        Cities = string.Join(", ", g.Select(x => x.Field<string>("City")).Distinct())
    })
    .OrderBy(x => x.Segment)
    .ToList();

Console.WriteLine("Active Non-Standard Customers by Segment:");
foreach (var seg in complexQuery)
{
    Console.WriteLine($"  {seg.Segment}: {seg.Count} customers in cities - {seg.Cities}");
}

Console.WriteLine("\n========== PRACTICE COMPLETE ==========");