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

        public IEnumerable<Product> GetAll()
        {
            List<Product> allProducts = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand sqlQuery = new SqlCommand("SELECT * FROM Product", conn))
                {
                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product productInTable = new Product
                            {
                                OEM = reader.GetString(0), // Column 1, VINNumber ...
                                VINNumber = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetInt32(3),
                                DateAvailable = reader.GetDateTime(4),
                                Notes = reader.GetString(5)
                            };
                            allProducts.Add(productInTable);
                        }
                    }
                }
            }
            return allProducts;
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
