using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;

namespace ServiceAPI.DatabaseAccess.DatabaseEntities
{
    public class DbEntity_Product : ICRUD_DB<Product>
    {
        // Configuration steps
        private string _connectionString;

        public DbEntity_Product(IConfiguration configuration)
        {
            DbHelper helper = new DbHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        // AddProduct(Product product)
        public void Create(Product entity)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand sqlQuery = new SqlCommand(
                    "INSERT INTO Product (OEM, VINNumber, Name, Price, DateAvailable, Notes) " 
                    + "VALUES (@OEM, @VINNumber, @Name, @Price, @DateAvailable, @Notes)", conn
                    ))
                {
                    // Mapping method input values to sql query input values
                    sqlQuery.Parameters.AddWithValue("@OEM", entity.OEM);
                    sqlQuery.Parameters.AddWithValue("@VINNumber", entity.VINNumber);
                    sqlQuery.Parameters.AddWithValue("@Name", entity.Name);
                    sqlQuery.Parameters.AddWithValue("@Price", entity.Price);
                    sqlQuery.Parameters.AddWithValue("@DateAvailable", entity.DateAvailable);
                    sqlQuery.Parameters.AddWithValue("@Notes", entity.Notes);

                    // Insert, update, delete
                    // Use non query, because we are updating/changing the DB, not querying it
                    sqlQuery.ExecuteNonQuery();
                }
            }
        }

        // DeleteProduct (string OEM)
        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        // GetAllProducts() -> outputs a list of all products in the DB/inventory.
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
                                OEM = reader.GetString(0), // Column 1, OEM
                                VINNumber = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                DateAvailable = reader.GetDateTime(4),
                                Notes = reader.GetString(5)
                            };
                            allProducts.Add(productInTable);
                        }
                    }
                }
                conn.Close();
            }
            return allProducts;
        }

        // GetProductByID(string OEM)
        public Product GetById(string OEM)
        {
            Product product = null;

            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand sqlQuery = new SqlCommand("SELECT OEM, VINNumber, Name, Price, DateAvailable, Notes FROM Product WHERE OEM = @OEM"))
                {
                    // Bind value from string input OEM to parameter OEM from Product in DB.
                    sqlQuery.Parameters.AddWithValue("@OEM", OEM);

                    using(SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            product = new Product
                            {
                                OEM = reader.GetString(0),
                                VINNumber = reader.GetString(1),
                                Name = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                DateAvailable = reader.GetDateTime(4),
                                Notes = reader.GetString(5)
                            };
                        }
                    }
                }
                conn.Close();
            }
            return product;
        }

        // UpdateProduct (Product product) -> Updates an existing Product in the database.
        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
