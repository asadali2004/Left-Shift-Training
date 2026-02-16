using System;
using System.Data;
using Microsoft.Data.SqlClient;

/*
 * ===========================================================================================
 * DISCONNECTED ARCHITECTURE - ADO.NET
 * ===========================================================================================
 * 
 * KEY CONCEPTS:
 * 1. Uses DataSet and DataAdapter to work with data
 * 2. Connection opens ONLY to fetch/update data, then closes immediately
 * 3. All operations (INSERT/UPDATE/DELETE) work on in-memory DataSet first
 * 4. Changes are sent to database in BATCH when you call adapter.Update()
 * 5. Best for: Desktop apps, offline scenarios, multiple table operations
 * 
 * ADVANTAGES:
 * ✓ Works with data offline (no active connection needed)
 * ✓ Multiple tables supported (DataSet can hold many tables)
 * ✓ Can navigate forward and backward through data
 * ✓ Less load on database (connection opens only when needed)
 * ✓ Supports complex filtering, sorting, and relationships
 * 
 * DISADVANTAGES:
 * ✗ Uses more memory (entire dataset loaded in RAM)
 * ✗ Slower for large datasets
 * ✗ More overhead than Connected Architecture
 * ===========================================================================================
 */

class Program1
{
    // static void Main()
    // {
    //     // Connection String - Contains database server and authentication info
    //     string cs = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Customer–Order;Integrated Security=True;Encrypt=False;TrustServerCertificate = True;";
        
    //     // SQL Query - Can fetch multiple tables in one go (separated by semicolon)
    //     string sql = "SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers; SELECT OrderId, CustomerId, OrderDate, Amount, Status, PaymentMode FROM dbo.Orders";
        
    //     // DataSet - In-memory cache that can hold multiple tables
    //     // Think of it as a "mini-database" in memory
    //     DataSet ds = new DataSet();
        
    //     // STEP 1: FETCH DATA FROM DATABASE
    //     using (var con = new SqlConnection(cs))  // Create connection
    //     using (var cmd = new SqlCommand(sql, con))  // Create command
    //     {
    //         con.Open();  // Open connection temporarily
            
    //         // SqlDataAdapter - Bridge between database and DataSet
    //         // It automatically opens/closes connection and fills DataSet
    //         SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    //         adapter.Fill(ds);  // Fetch data and fill DataSet
            
    //     } // Connection automatically closes here! (using statement)
        
    //     // IMPORTANT: Connection is now CLOSED, but we still have data in DataSet
    //     // This is the KEY concept of Disconnected Architecture
        
    //     // Export DataSet to XML file (optional - for demo purpose)
    //     // Export DataSet to XML file (optional - for demo purpose)
    //     ds.WriteXml("TestData");

    //     // STEP 2: WORK WITH DATA (Connection is CLOSED!)
    //     // Display how many tables were loaded
    //     Console.WriteLine(ds.Tables.Count);  // Should print: 2 (Customers and Orders)
        
    //     // Display column names of first table (Customers)
    //     foreach(DataColumn item in ds.Tables[0].Columns){
    //         System.Console.WriteLine(item.ColumnName+"\t");
    //     }
        
    //     // Display all rows from first table
    //     foreach (DataRow row in ds.Tables[0].Rows)
    //     {
    //         foreach (var item in row.ItemArray)
    //         {
    //             Console.Write(item + "\t");
    //         }
    //         Console.WriteLine();
    //     }
        
    //     // STEP 3: PERFORM CRUD OPERATIONS
    //     // All operations work on in-memory DataSet first, then sync with DB
        
    //     // INSERT Operation
    //     Console.WriteLine("\n--- INSERT OPERATION ---");
    //     InsertCustomer(ds, cs);
    //     Console.WriteLine("\nData After INSERT:");
    //     DisplayTableData(ds);

    //     // UPDATE Operation
    //     Console.WriteLine("\n--- UPDATE OPERATION ---");
    //     UpdateCustomer(ds, cs);
    //     Console.WriteLine("\nData After UPDATE:");
    //     DisplayTableData(ds);

    //     // DELETE Operation
    //     Console.WriteLine("\n--- DELETE OPERATION ---");
    //     DeleteCustomer(ds, cs);
    //     Console.WriteLine("\nData After DELETE:");
    //     DisplayTableData(ds);
    // }
    
    // ===========================================================================================
    // HELPER METHOD: Display DataSet Table Data
    // ===========================================================================================
    static void DisplayTableData(DataSet ds)
    {
        // Check if table has any data
        if (ds.Tables[0].Rows.Count == 0)
        {
            Console.WriteLine("No records found!");
            return;
        }

        // Print column headers
        foreach (DataColumn col in ds.Tables[0].Columns)
        {
            Console.Write(col.ColumnName + "\t");
        }
        Console.WriteLine();

        // Print all rows
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
    }

    // ===========================================================================================
    // INSERT OPERATION - Disconnected Way
    // ===========================================================================================
    static void InsertCustomer(DataSet ds, string cs)
    {
        // STEP 1: Create new row in memory (No database connection yet!)
        int newId = new Random().Next(1000, 9999);
        DataRow newRow = ds.Tables[0].NewRow();  // Create empty row based on table schema
        
        // STEP 2: Fill row with data
        newRow["CustomerId"] = newId;
        newRow["FullName"] = "John Doe";
        newRow["City"] = "Bangalore";
        newRow["Segment"] = "Premium";
        newRow["IsActive"] = true;
        newRow["CreatedOn"] = DateTime.Now;
        
        // STEP 3: Add row to DataSet (Still in memory, not in database!)
        ds.Tables[0].Rows.Add(newRow);
        
        // STEP 4: Sync changes with database
        using (var con = new SqlConnection(cs))
        using (var cmd = new SqlCommand("SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers", con))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            
            // SqlCommandBuilder - Automatically generates INSERT/UPDATE/DELETE commands
            // So you don't have to write them manually!
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
            
            // adapter.Update() - Sends all pending changes to database
            // It figures out what changed (insert/update/delete) and sends only those changes
            adapter.Update(ds, "Table");
            Console.WriteLine($"Record Inserted Successfully! (CustomerId: {newId})");

            // STEP 5: Reload fresh data from database
            ds.Tables[0].Clear();
            adapter.Fill(ds.Tables[0]);
        } // Connection closes here
    }

    // ===========================================================================================
    // UPDATE OPERATION - Disconnected Way
    // ===========================================================================================
    static void UpdateCustomer(DataSet ds, string cs)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            // STEP 1: Modify data in memory (No database connection!)
            ds.Tables[0].Rows[0]["FullName"] = "Updated Name";
            ds.Tables[0].Rows[0]["City"] = "Mumbai";
            ds.Tables[0].Rows[0]["Segment"] = "Gold";
            ds.Tables[0].Rows[0]["IsActive"] = false;
            
            // STEP 2: Sync changes with database
            using (var con = new SqlConnection(cs))
            using (var cmd = new SqlCommand("SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers", con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                
                // Send UPDATE to database
                adapter.Update(ds, "Table");
                Console.WriteLine("Record Updated Successfully!");

                // Reload fresh data from database
                ds.Tables[0].Clear();
                adapter.Fill(ds.Tables[0]);
            } // Connection closes here
        }
    }

    // ===========================================================================================
    // DELETE OPERATION - Disconnected Way
    // ===========================================================================================
    static void DeleteCustomer(DataSet ds, string cs)
    {
        // STEP 1: Find and mark row for deletion in memory
        // Select() method - Filters rows like SQL WHERE clause
        DataRow[] rowsToDelete = ds.Tables[0].Select("FullName = 'John Doe'");
        
        if (rowsToDelete.Length > 0)
        {
            // Mark row as deleted (doesn't remove from memory yet)
            rowsToDelete[0].Delete();
            
            // STEP 2: Sync changes with database
            using (var con = new SqlConnection(cs))
            using (var cmd = new SqlCommand("SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers", con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                
                // Send DELETE to database
                adapter.Update(ds, "Table");
                Console.WriteLine("Record Deleted Successfully!");

                // Reload fresh data from database
                ds.Tables[0].Clear();
                adapter.Fill(ds.Tables[0]);
            } // Connection closes here
        }
        else
        {
            Console.WriteLine("Record with FullName 'John Doe' not found!");
        }
    }
}