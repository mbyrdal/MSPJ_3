using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbProduct : ICRUD_DB<Product>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbProduct(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
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
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                CarPartID = reader.GetInt32(reader.GetOrdinal("CarPartID")),
                                SaleID = reader.GetInt32(reader.GetOrdinal("SaleID")),
                                OEM = reader.GetString(reader.GetOrdinal("OEM")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Condition = reader.GetString(reader.GetOrdinal("Condition")),
                                ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription"))
                            };
                            products.Add(productInTable);
                        }
                    }
                }
                conn.Close();
            }
            return products;
        }

        public Product GetByIdentifier(int ID)
        {
            Product product = null; // Set product to null initially
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, CartPartID, SaleID, OEM, Price, DateAvailable, Condition, ItemDescription FROM Product WHERE ID = @ID", conn))
                {
                    // Bind value from string input OEM to parameter OEM from Product in DB.
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
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
                    "INSERT INTO Product (ID, CartPartID, SaleID, OEM, Price, DateAvailable, Condition, ItemDescription) "
                    + "VALUES (@ID, @CartPartID, @SaleID, @OEM, @Price, @DateAvailable, @Condition, @ItemDescription)", conn
                    ))
                {
                    // Mapping method input values to sql query input values
                    createCommand.Parameters.AddWithValue("@ID", newProduct.ID);
                    createCommand.Parameters.AddWithValue("@CartPartID", newProduct.CarPartID);
                    createCommand.Parameters.AddWithValue("@SaleID", newProduct.SaleID);
                    createCommand.Parameters.AddWithValue("@OEM", newProduct.OEM);
                    createCommand.Parameters.AddWithValue("@Price", newProduct.Price);
                    createCommand.Parameters.AddWithValue("@DateAvailable", newProduct.DateAvailable);
                    createCommand.Parameters.AddWithValue("@Condition", newProduct.Condition);
                    createCommand.Parameters.AddWithValue("@ItemDescription", newProduct.ItemDescription);

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
                                                                     "SET ID=@ID, CarPartID=@CarPartID, SaleID=@SaleID, OEM=@OEM, " +
                                                                     "Price=@Price, DateAvailable=@DateAvailable, Condition=@Condition, ItemDescription=@ItemDescription" +
                                                                     "WHERE ID = @ID AND CarPartID = @CarPartID AND SaleID = @SaleID AND OEM = @OEM", conn))
                    {
                        // Mapping method input values to sql query input values
                        updateCommand.Parameters.AddWithValue("@Price", updateProduct.Price);
                        updateCommand.Parameters.AddWithValue("@DateAvailable", updateProduct.DateAvailable);
                        updateCommand.Parameters.AddWithValue("@Condition", updateProduct.Condition);
                        updateCommand.Parameters.AddWithValue("@ItemDescription", updateProduct.ItemDescription);

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

        public bool DeleteEntity(int ID)
        {
            bool wasProductDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Product WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);

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
        internal bool ProductExists(int ID)
        {
            return _dbHelper.EntityExists("Product", "ID", ID.ToString());
        }
    }
}
