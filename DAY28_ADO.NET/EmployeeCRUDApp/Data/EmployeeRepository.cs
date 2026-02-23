using System.Data;
using EmployeeApp.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeApp.Data;

public class EmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // ── CREATE ──────────────────────────────────────────────────────────────

    public int Add(Employee emp)
    {
        const string sql = @"
            INSERT INTO Employees (Name, Email, Department, Salary)
            VALUES (@Name, @Email, @Department, @Salary);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlConnection con = new(_connectionString);
        using SqlCommand    cmd = new(sql, con);

        cmd.Parameters.Add("@Name",       SqlDbType.NVarChar, 100).Value = emp.Name;
        cmd.Parameters.Add("@Email",      SqlDbType.NVarChar, 100).Value = emp.Email;
        cmd.Parameters.Add("@Department", SqlDbType.NVarChar,  50).Value = emp.Department;
        cmd.Parameters.Add("@Salary",     SqlDbType.Decimal       ).Value = emp.Salary;
        cmd.Parameters["@Salary"].Precision = 10;
        cmd.Parameters["@Salary"].Scale     = 2;

        con.Open();
        object? result = cmd.ExecuteScalar();
        return result is null ? 0 : Convert.ToInt32(result);
    }

    // ── READ ─────────────────────────────────────────────────────────────────

    public List<Employee> GetAll()
    {
        const string sql = @"
            SELECT EmployeeId, Name, Email, Department, Salary
            FROM   Employees
            WHERE  IsActive = 1
            ORDER  BY EmployeeId;";

        using SqlConnection  con     = new(_connectionString);
        using SqlDataAdapter adapter = new(sql, con);

        DataTable table = new();
        adapter.Fill(table);

        List<Employee> list = new();
        foreach (DataRow row in table.Rows)
            list.Add(MapRow(row));

        return list;
    }

    public Employee? GetById(int id)
    {
        const string sql = @"
            SELECT EmployeeId, Name, Email, Department, Salary
            FROM   Employees
            WHERE  EmployeeId = @Id AND IsActive = 1;";

        using SqlConnection con = new(_connectionString);
        using SqlCommand    cmd = new(sql, con);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        con.Open();
        using SqlDataReader reader = cmd.ExecuteReader();
        return reader.Read() ? MapReader(reader) : null;
    }

    // ── UPDATE ───────────────────────────────────────────────────────────────

    public bool Update(Employee emp)
    {
        const string sql = @"
            UPDATE Employees
            SET    Name       = @Name,
                   Email      = @Email,
                   Department = @Department,
                   Salary     = @Salary
            WHERE  EmployeeId = @Id AND IsActive = 1;";

        using SqlConnection con = new(_connectionString);
        using SqlCommand    cmd = new(sql, con);

        cmd.Parameters.Add("@Id",         SqlDbType.Int           ).Value = emp.EmployeeId;
        cmd.Parameters.Add("@Name",       SqlDbType.NVarChar, 100 ).Value = emp.Name;
        cmd.Parameters.Add("@Email",      SqlDbType.NVarChar, 100 ).Value = emp.Email;
        cmd.Parameters.Add("@Department", SqlDbType.NVarChar,  50 ).Value = emp.Department;
        cmd.Parameters.Add("@Salary",     SqlDbType.Decimal       ).Value = emp.Salary;
        cmd.Parameters["@Salary"].Precision = 10;
        cmd.Parameters["@Salary"].Scale     = 2;

        con.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    // ── DELETE (Soft) ────────────────────────────────────────────────────────

    public bool Delete(int id)
    {
        const string sql = @"
            UPDATE Employees
            SET    IsActive = 0
            WHERE  EmployeeId = @Id AND IsActive = 1;";

        using SqlConnection con = new(_connectionString);
        using SqlCommand    cmd = new(sql, con);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        con.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static Employee MapRow(DataRow row) => new()
    {
        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
        Name       = Convert.ToString(row["Name"])       ?? string.Empty,
        Email      = Convert.ToString(row["Email"])      ?? string.Empty,
        Department = Convert.ToString(row["Department"]) ?? string.Empty,
        Salary     = Convert.ToDecimal(row["Salary"])
    };

    private static Employee MapReader(SqlDataReader r) => new()
    {
        EmployeeId = Convert.ToInt32(r["EmployeeId"]),
        Name       = Convert.ToString(r["Name"])       ?? string.Empty,
        Email      = Convert.ToString(r["Email"])      ?? string.Empty,
        Department = Convert.ToString(r["Department"]) ?? string.Empty,
        Salary     = Convert.ToDecimal(r["Salary"])
    };
}