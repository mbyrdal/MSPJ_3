using BrowserWebPage.Utilities;
using Microsoft.Data.SqlClient;
using BrowserWebPage.DTOs;

public class DbHelper
{
    private readonly string _connectionString;

    public DbHelper(IConfiguration configuration)
    {
        ConnectionHelper helper = new ConnectionHelper(configuration);
        _connectionString = helper.GetDBConnectionString();
    }

    public bool EntityExists(string tableName, string columnName, string columnValue)
    {
        bool entityExists = false;
        using(SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string entityExistsQuery = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @value";
            using (SqlCommand checkCommand = new SqlCommand(entityExistsQuery, conn))
            {
                // Add parameters to the query to prevent SQL injection
                checkCommand.Parameters.AddWithValue("@value", columnValue);
                entityExists = Convert.ToInt32(checkCommand.ExecuteScalar()) == 1;
            }
            conn.Close();
        }
        return entityExists;
    }

    // Executes a query and returns a list of ProductDTOs
    public List<ProductDTO> ExecuteQuery(string query, params SqlParameter[] parameters)
    {
        List<ProductDTO> products = new List<ProductDTO>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Add parameters to the query to prevent SQL injection
                cmd.Parameters.AddRange(parameters);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductDTO
                        {
                            CarPartID = reader.GetInt32(0),
                            CarID = reader.GetInt32(1),
                            OEM = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            DateAvailable = reader.GetDateTime(4),
                            Condition = reader.GetString(5),
                            ItemDescription = reader.GetString(6)
                        });
                    }
                }
            }
        }

        return products;
    }
}
