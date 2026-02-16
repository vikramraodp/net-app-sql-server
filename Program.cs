using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NetAppSqlServer;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Error: Connection string 'DefaultConnection' not found in appsettings.json.");
            return;
        }

        Console.WriteLine("SQL Server Query Tool");
        Console.WriteLine("---------------------");

        while (true)
        {
            Console.WriteLine("\nEnter your SQL query (or type 'exit' to quit):");
            string? query = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(query) || query.Trim().ToLower() == "exit")
            {
                break;
            }

            try
            {
                ExecuteAndPrintQuery(connectionString, query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }
    }

    static void ExecuteAndPrintQuery(string connectionString, string query)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var command = new SqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("Query executed successfully, but returned no results or it was not a SELECT statement.");
            if (reader.RecordsAffected >= 0)
            {
                Console.WriteLine($"{reader.RecordsAffected} row(s) affected.");
            }
            return;
        }

        DataTable dataTable = new DataTable();
        dataTable.Load(reader);

        PrintTable(dataTable);
    }

    static void PrintTable(DataTable table)
    {
        if (table.Rows.Count == 0) return;

        // Calculate column widths
        int[] columnWidths = new int[table.Columns.Count];
        for (int i = 0; i < table.Columns.Count; i++)
        {
            columnWidths[i] = table.Columns[i].ColumnName.Length;
            foreach (DataRow row in table.Rows)
            {
                int length = row[i]?.ToString()?.Length ?? 0;
                if (length > columnWidths[i])
                {
                    columnWidths[i] = length;
                }
            }
            columnWidths[i] += 2; // Add some padding
        }

        // Print header
        PrintSeparator(columnWidths);
        for (int i = 0; i < table.Columns.Count; i++)
        {
            Console.Write($"| {table.Columns[i].ColumnName.PadRight(columnWidths[i] - 1)}");
        }
        Console.WriteLine("|");
        PrintSeparator(columnWidths);

        // Print rows
        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write($"| {row[i]?.ToString()?.PadRight(columnWidths[i] - 1)}");
            }
            Console.WriteLine("|");
        }
        PrintSeparator(columnWidths);
    }

    static void PrintSeparator(int[] columnWidths)
    {
        foreach (int width in columnWidths)
        {
            Console.Write("+" + new string('-', width));
        }
        Console.WriteLine("+");
    }
}
