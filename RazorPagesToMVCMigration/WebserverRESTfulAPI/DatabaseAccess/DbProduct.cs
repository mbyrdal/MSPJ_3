using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DatabaseAccess.Utilities;
using ServiceAPI.Models;

namespace ServiceAPI.DatabaseAccess
{
    public class DbProduct : ICRUD_DB<Product>
    {
        // Configuration steps
        private string _connectionString;
        public DbProduct(IConfiguration configuration)
        {
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }
        public List<Product> GetAllEntities()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Product", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product productInTable = new Product
                            {
                                OEM = reader.GetString(reader.GetOrdinal("OEM")), // Column 1, Primary key OEM.
                                VINNumber = reader.GetString(reader.GetOrdinal("FK_VINNumber")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes"))
                            };
                            products.Add(productInTable);
                        }
                    }
                }
                conn.Close();
            }
            return products;
        }
        public Product GetByIdentifier(string OEM)
        {
            Product product = new Product();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT OEM, FK_VINNumber, Name, Price, DateAvailable, Notes FROM Product WHERE OEM = @OEM", conn))
                {
                    // Bind value from string input OEM to parameter OEM from Product in DB.
                    readCommand.Parameters.AddWithValue("@OEM", OEM);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                OEM = reader.GetString(reader.GetOrdinal("OEM")), // Column 1, Primary key OEM.
                                VINNumber = reader.GetString(reader.GetOrdinal("FK_VINNumber")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes"))
                            };
                        }
                    }
                }
                conn.Close();
            }
            return product;
        }
        public int CreateEntity(Product newProduct) 
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Product (OEM, FK_VINNumber, Name, Price, DateAvailable, Notes) "
                    + "VALUES (@OEM, @FK_VINNumber, @Name, @Price, @DateAvailable, @Notes)", conn
                    ))
                {
                    // Mapping method input values to sql query input values
                    createCommand.Parameters.AddWithValue("@OEM", newProduct.OEM);
                    createCommand.Parameters.AddWithValue("@FK_VINNumber", newProduct.VINNumber);
                    createCommand.Parameters.AddWithValue("@Name", newProduct.Name);
                    createCommand.Parameters.AddWithValue("@Price", newProduct.Price);
                    createCommand.Parameters.AddWithValue("@DateAvailable", newProduct.DateAvailable);
                    createCommand.Parameters.AddWithValue("@Notes", newProduct.Notes);

                    // Use non query because we are updating/changing the DB, not querying it
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }
        public int UpdateEntity(Product updateProduct)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Product " +
                                                                     "SET Name=@Name, Price=@Price, DateAvailable=@DateAvailable, Notes=@Notes " +
                                                                     "WHERE OEM = @OEM AND FK_VINNumber = @FK_VINNumber", conn))
                    {
                        // Mapping method input values to sql query input values
                        updateCommand.Parameters.AddWithValue("@OEM", updateProduct.OEM);
                        updateCommand.Parameters.AddWithValue("@FK_VINNumber", updateProduct.VINNumber);
                        updateCommand.Parameters.AddWithValue("@Name", updateProduct.Name);
                        updateCommand.Parameters.AddWithValue("@Price", updateProduct.Price);
                        updateCommand.Parameters.AddWithValue("@DateAvailable", updateProduct.DateAvailable);
                        updateCommand.Parameters.AddWithValue("@Notes", updateProduct.Notes);

                        // Use non query because we are updating/changing the DB, not querying it
                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    // Log or handle the exception (logging to console for now)
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                return numberOfRowsUpdated;
            }
        }
        public bool DeleteEntity(string OEM)
        {
            bool wasProductDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Product WHERE OEM = @OEM", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@OEM", OEM);

                    // Track number of rows affected (changes made to DB)
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();

                    // Succession criteria: only one (1) row should be affected, ie one Product deleted
                    wasProductDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasProductDeleted;
        }
        // Helper method that tests whether an entity (Product) entry exists in the database.
        internal bool ProductExists(string OEM)
        {
            bool prodExists = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string existQuery = "SELECT COUNT(1) FROM Product WHERE OEM = @OEM";
                using (SqlCommand checkCommand = new SqlCommand(existQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@OEM", OEM);
                    // Returns the first column of the first row of the Product table in the DB.
                    // Boolean that determines whether a given product with a specific OEM exists.
                    prodExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                }
                conn.Close();
            }
            return prodExists;
        }
    }
}
