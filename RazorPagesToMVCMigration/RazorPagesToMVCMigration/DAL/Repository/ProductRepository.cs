using Microsoft.Data.SqlClient;
using RazorPagesToMVCMigration.DAL.Interface;
using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL.Repository
{
    public class ProductRepository : ICRUD<Product>
    {
        private string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            var configHelper = new ConfigurationHelper(configuration);
            _connectionString = configHelper.GetDBConnectionString();
        }

        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
{
    List<Product> allProducts = new List<Product>();

    using (SqlConnection conn = new SqlConnection(_connectionString))
    {
        await conn.OpenAsync();
        using (SqlCommand sqlQuery = new SqlCommand("SELECT * FROM Product", conn))
        {
            using (SqlDataReader reader = await sqlQuery.ExecuteReaderAsync())
            {
                if (!reader.HasRows)  // Check if no rows are returned
                {
                    throw new InvalidOperationException("Error, No products returned"); //muligvis ændres 
                }

                while (await reader.ReadAsync())
                {
                    Product productInTable = new Product
                    {
                       ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                CarPartID = reader.GetInt32(reader.GetOrdinal("CarPartID")),
                                SaleID = reader.GetInt32(reader.GetOrdinal("SaleID")),
                                OEM = reader.GetString(reader.GetOrdinal("OEM")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Condition = reader.GetString(reader.GetOrdinal("Condition")),
                                ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription"))
                    };
                    allProducts.Add(productInTable);
                }
            }
        }
    }

    return allProducts;
}


        public IEnumerable<Product> GetAll()
        {
            return GetAllAsync().Result;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
