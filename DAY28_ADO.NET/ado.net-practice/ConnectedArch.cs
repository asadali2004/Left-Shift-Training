using System;
using System.Data;
using Microsoft.Data.SqlClient;

/*
 * ===========================================================================================
 * CONNECTED ARCHITECTURE - ADO.NET
 * ===========================================================================================
 * 
 * KEY CONCEPTS:
 * 1. Uses SqlDataReader to read data directly from database
 * 2. Connection remains OPEN while reading data (must stay connected)
 * 3. All operations execute immediately on the database (no caching)
 * 4. Changes go directly to database one at a time
 * 5. Best for: Web apps, real-time data, read-only scenarios
 * 
 * ADVANTAGES:
 * ✓ Fast and lightweight (minimal memory usage)
 * ✓ Perfect for large datasets (read row-by-row)
 * ✓ Direct database access (always fresh data)
 * ✓ Simple and straightforward
 * ✓ Immediate execution of commands
 * 
 * DISADVANTAGES:
 * ✗ Must maintain active connection (more load on database)
 * ✗ Cannot work offline
 * ✗ Forward-only cursor (can't go back)
 * ✗ Read-only with DataReader (can't modify while reading)
 * ✗ One operation at a time
 * ===========================================================================================
 */

namespace ConnectedArchitecture
{
    class ConnectedArch
    {
        // Connection String - Contains database server and authentication info
        private static string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Customer–Order;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        // ===========================================================================================
        // MAIN METHOD - Entry point for CONNECTED ARCHITECTURE
        // ===========================================================================================
        /*  COMMENT THIS OUT to run CONNECTED ARCHITECTURE
        static void Main(string[] args)
        {
            Console.WriteLine("========== CONNECTED ARCHITECTURE - ADO.NET ==========\n");

            // 1. SELECT Operation - Read All Customers
            Console.WriteLine("--- SELECT - All Customers ---");
            SelectAllCustomers();

            // 2. SELECT Operation - Read Customer by ID
            Console.WriteLine("\n--- SELECT - Customer by ID (101) ---");
            SelectCustomerById(101);

            // 3. INSERT Operation
            Console.WriteLine("\n--- INSERT - New Customer ---");
            int newCustomerId = InsertCustomer("Rajesh Kumar", "Delhi", "Premium", true);
            Console.WriteLine($"New Customer Inserted with ID: {newCustomerId}");

            // 4. UPDATE Operation
            Console.WriteLine("\n--- UPDATE - Customer Details ---");
            bool updateSuccess = UpdateCustomer(newCustomerId, "Rajesh Kumar Updated", "Mumbai", "Gold", false);
            Console.WriteLine($"Update Status: {(updateSuccess ? "Success" : "Failed")}");

            // 5. DELETE Operation
            Console.WriteLine("\n--- DELETE - Customer ---");
            bool deleteSuccess = DeleteCustomer(newCustomerId);
            Console.WriteLine($"Delete Status: {(deleteSuccess ? "Success" : "Failed")}");

            // 6. Scalar Query - Count customers
            Console.WriteLine("\n--- SCALAR QUERY - Total Customers ---");
            int totalCustomers = GetTotalCustomersCount();
            Console.WriteLine($"Total Customers: {totalCustomers}");

            // 7. Non-Query with Transactions
            Console.WriteLine("\n--- TRANSACTION EXAMPLE ---");
            InsertMultipleCustomersWithTransaction();

            Console.WriteLine("\n========== COMPLETED ==========");
        }
        */  // End of Commented Main

        // ===========================================================================================
        // SELECT ALL - Read data using SqlDataReader (CONNECTION STAYS OPEN!)
        // ===========================================================================================
        // 1. SqlDataReader is FORWARD-ONLY (like streaming - can't go back)
        // 2. READ-ONLY (can't modify data while reading)
        // 3. CONNECTION MUST STAY OPEN until you're done reading
        // 4. Very FAST and MEMORY EFFICIENT (reads one row at a time)
        // ===========================================================================================
        static void SelectAllCustomers()
        {
            // Create connection object
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // SQL Query to fetch data
                string query = "SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers";
                
                // Create command object
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    // STEP 1: Open connection
                    con.Open();
                    
                    // STEP 2: Execute query and get DataReader
                    // IMPORTANT: Connection stays OPEN while using reader
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("CustomerId\tFullName\t\tCity\t\tSegment\t\tIsActive\tCreatedOn");
                    Console.WriteLine(new string('-', 100));

                    // STEP 3: Read data row by row (like streaming)
                    while (reader.Read())  // Returns true if there's a row to read
                    {
                        // Two ways to access data:
                        // Method 1: By index (faster) - reader.GetInt32(0)
                        int customerId = reader.GetInt32(0);      // Column 0
                        string fullName = reader.GetString(1);     // Column 1
                        string city = reader.GetString(2);         // Column 2
                        string segment = reader.GetString(3);      // Column 3
                        bool isActive = reader.GetBoolean(4);      // Column 4
                        DateTime createdOn = reader.GetDateTime(5); // Column 5

                        Console.WriteLine($"{customerId}\t\t{fullName,-20}\t{city,-15}\t{segment,-15}\t{isActive}\t{createdOn.ToShortDateString()}");
                    }

                    // STEP 4: Close reader
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
            } // Connection automatically closes here (using statement)
        }

        // ===========================================================================================
        // SELECT BY ID - Parameterized Query with SqlDataReader
        // ===========================================================================================
        // Using PARAMETERS (@CustomerId) prevents SQL Injection attacks!
        // Never concatenate user input directly into SQL query
        // ===========================================================================================
        static void SelectCustomerById(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Query with parameter placeholder @CustomerId
                string query = "SELECT CustomerId, FullName, City, Segment, IsActive, CreatedOn FROM dbo.Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, con);
                
                // Add parameter - SAFE from SQL Injection
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())  // Read single row
                    {
                        // Method 2: Access by column name (more readable, slightly slower)
                        Console.WriteLine($"Customer ID: {reader["CustomerId"]}");
                        Console.WriteLine($"Full Name: {reader["FullName"]}");
                        Console.WriteLine($"City: {reader["City"]}");
                        Console.WriteLine($"Segment: {reader["Segment"]}");
                        Console.WriteLine($"Is Active: {reader["IsActive"]}");
                        Console.WriteLine($"Created On: {Convert.ToDateTime(reader["CreatedOn"]).ToShortDateString()}");
                    }
                    else
                    {
                        Console.WriteLine($"Customer with ID {customerId} not found.");
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
            }
        }

        // ===========================================================================================
        // INSERT - Add new record using ExecuteNonQuery()
        // ===========================================================================================
        // ExecuteNonQuery() - Used for INSERT, UPDATE, DELETE commands
        // Returns: Number of rows affected
        // Does NOT return data (use ExecuteReader for SELECT)
        // ===========================================================================================
        static int InsertCustomer(string fullName, string city, string segment, bool isActive)
        {
            int newCustomerId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // INSERT query with parameters
                string query = @"INSERT INTO dbo.Customers (CustomerId, FullName, City, Segment, IsActive, CreatedOn) 
                                VALUES (@CustomerId, @CustomerId, @FullName, @City, @Segment, @IsActive, @CreatedOn);
                                SELECT @CustomerId;";

                SqlCommand cmd = new SqlCommand(query, con);

                // Generate random CustomerId
                newCustomerId = new Random().Next(1000, 9999);

                // Add parameters - ALWAYS use parameters, never concatenate!
                cmd.Parameters.AddWithValue("@CustomerId", newCustomerId);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Segment", segment);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                try
                {
                    con.Open();  // Open connection
                    
                    // ExecuteNonQuery() - Executes INSERT/UPDATE/DELETE
                    // Returns number of rows affected
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Customer '{fullName}' inserted successfully!");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Insert Error: {ex.Message}");
                    return 0;
                }
            } // Connection closes here

            return newCustomerId;
        }

        // ===========================================================================================
        // UPDATE - Modify existing record using ExecuteNonQuery()
        // ===========================================================================================
        static bool UpdateCustomer(int customerId, string fullName, string city, string segment, bool isActive)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // UPDATE query with parameters
                string query = @"UPDATE dbo.Customers 
                                SET FullName = @FullName, 
                                    City = @City, 
                                    Segment = @Segment, 
                                    IsActive = @IsActive 
                                WHERE CustomerId = @CustomerId";

                SqlCommand cmd = new SqlCommand(query, con);
                
                // Add parameters
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Segment", segment);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                try
                {
                    con.Open();
                    
                    // Execute UPDATE and get number of rows affected
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Customer ID {customerId} updated successfully! ({rowsAffected} row(s) affected)");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Customer ID {customerId} not found.");
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Update Error: {ex.Message}");
                    return false;
                }
            }
        }

        // ===========================================================================================
        // DELETE - Remove record using ExecuteNonQuery()
        // ===========================================================================================
        static bool DeleteCustomer(int customerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // DELETE query with parameter
                string query = "DELETE FROM dbo.Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                try
                {
                    con.Open();
                    
                    // Execute DELETE and get number of rows affected
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Customer ID {customerId} deleted successfully! ({rowsAffected} row(s) affected)");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Customer ID {customerId} not found.");
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Delete Error: {ex.Message}");
                    return false;
                }
            }
        }

        // ===========================================================================================
        // SCALAR QUERY - Get single value using ExecuteScalar()
        // ===========================================================================================
        // ExecuteScalar() - Returns first column of first row
        // Perfect for: COUNT, SUM, AVG, MAX, MIN queries
        // ===========================================================================================
        static int GetTotalCustomersCount()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Aggregate query
                string query = "SELECT COUNT(*) FROM dbo.Customers";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    
                    // ExecuteScalar() - Returns single value (first column of first row)
                    int count = (int)cmd.ExecuteScalar();
                    return count;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
        }

        // ===========================================================================================
        // TRANSACTION - Ensure multiple operations succeed or fail together
        // ===========================================================================================
        // Transaction ensures ACID properties:
        // A - Atomicity (all or nothing)
        // C - Consistency (valid state)
        // I - Isolation (isolated from other transactions)
        // D - Durability (permanent once committed)
        // ===========================================================================================
        static void InsertMultipleCustomersWithTransaction()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                
                // BEGIN TRANSACTION
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string query = @"INSERT INTO dbo.Customers (CustomerId, FullName, City, Segment, IsActive, CreatedOn) 
                                    VALUES (@CustomerId, @FullName, @City, @Segment, @IsActive, @CreatedOn)";

                    // Insert First Customer
                    SqlCommand cmd1 = new SqlCommand(query, con, transaction);
                    cmd1.Parameters.AddWithValue("@CustomerId", new Random().Next(5000, 5999));
                    cmd1.Parameters.AddWithValue("@FullName", "Transaction Customer 1");
                    cmd1.Parameters.AddWithValue("@City", "Pune");
                    cmd1.Parameters.AddWithValue("@Segment", "Corporate");
                    cmd1.Parameters.AddWithValue("@IsActive", true);
                    cmd1.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd1.ExecuteNonQuery();

                    // Insert Second Customer
                    SqlCommand cmd2 = new SqlCommand(query, con, transaction);
                    cmd2.Parameters.AddWithValue("@CustomerId", new Random().Next(6000, 6999));
                    cmd2.Parameters.AddWithValue("@FullName", "Transaction Customer 2");
                    cmd2.Parameters.AddWithValue("@City", "Kolkata");
                    cmd2.Parameters.AddWithValue("@Segment", "Retail");
                    cmd2.Parameters.AddWithValue("@IsActive", true);
                    cmd2.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    cmd2.ExecuteNonQuery();

                    // COMMIT TRANSACTION - Make changes permanent
                    transaction.Commit();
                    Console.WriteLine("Transaction completed successfully! 2 customers inserted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Transaction failed: {ex.Message}");
                    
                    // ROLLBACK TRANSACTION - Undo all changes
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back.");
                }
            }
        }
    }
}
