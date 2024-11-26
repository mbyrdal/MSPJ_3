using Microsoft.Data.SqlClient;
using ServiceAPI.DTOs;
using ServiceAPI.Utilities;

public class DbHelper
{
    private readonly string _connectionString;

    public DbHelper(IConfiguration configuration)
    {
        ConnectionHelper helper = new ConnectionHelper(configuration);
        _connectionString = helper.GetDBConnectionString();
    }

    // Executes a query and returns a list of ProductViewModels
    public List<ProductViewModel> ExecuteQuery(string query, params SqlParameter[] parameters)
    {
        List<ProductViewModel> products = new List<ProductViewModel>();

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
                        products.Add(new ProductViewModel
                        {
                            CarPartID = reader.GetInt32(0),
                            SaleID = reader.GetInt32(1),
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
