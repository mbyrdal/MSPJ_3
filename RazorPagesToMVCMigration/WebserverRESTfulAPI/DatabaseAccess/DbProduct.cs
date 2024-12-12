using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Diagnostics;

namespace ServiceAPI.DatabaseAccess
{
    public class DbProduct : IDbProduct
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
                                CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                                OEM = reader.GetString(reader.GetOrdinal("OEM")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Condition = reader.GetString(reader.GetOrdinal("Condition")),
                                ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                                ItemAvailable = reader.GetBoolean(reader.GetOrdinal("ItemAvailable"))
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
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, CarPartID, CarID, OEM, Price, DateAvailable, " +
                                                               "Condition, ItemDescription, ItemAvailable FROM Product " +
                                                               "WHERE ID = @ID", conn))
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
                                CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                                OEM = reader.GetString(reader.GetOrdinal("OEM")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Condition = reader.GetString(reader.GetOrdinal("Condition")),
                                ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                                ItemAvailable = reader.GetBoolean(reader.GetOrdinal("ItemAvailable"))
                            };
                        }
                    }
                }
                conn.Close();
            }
            return product;
        }

        public Product GetByInputs(int ID, int carPartID, int carID)
        {
            Product product = null; // Set product to null initially
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, CarPartID, CarID, OEM, Price, DateAvailable, " +
                                                               "Condition, ItemDescription, ItemAvailable FROM Product " +
                                                               "WHERE ID = @ID AND CarPartID = @CarPartID AND CarID = @CarID", conn))
                {
                    // Bind value from string input OEM to parameter OEM from Product in DB.
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    readCommand.Parameters.AddWithValue("@CarPartID", carPartID);
                    readCommand.Parameters.AddWithValue("@CarID", carID);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                CarPartID = reader.GetInt32(reader.GetOrdinal("CarPartID")),
                                CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                                OEM = reader.GetString(reader.GetOrdinal("OEM")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                DateAvailable = reader.GetDateTime(reader.GetOrdinal("DateAvailable")),
                                Condition = reader.GetString(reader.GetOrdinal("Condition")),
                                ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                                ItemAvailable = reader.GetBoolean(reader.GetOrdinal("ItemAvailable"))
                            };
                        }
                    }
                }
                conn.Close();
            }
            return product;
        }

        public int CreateEntity(ProductViewModel newProduct, int carPartID, int carID) 
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Product (CarPartID, CarID, OEM, Price, DateAvailable, Condition, ItemDescription, ItemAvailable) "
                    + "VALUES (@CarPartID, @CarID, @OEM, @Price, @DateAvailable, @Condition, @ItemDescription, @ItemAvailable)", conn
                    ))
                {
                    // Mapping method input values to sql query input values
                    createCommand.Parameters.AddWithValue("@CarPartID", carPartID);
                    createCommand.Parameters.AddWithValue("@CarID", carID);
                    createCommand.Parameters.AddWithValue("@OEM", newProduct.OEM);
                    createCommand.Parameters.AddWithValue("@Price", newProduct.Price);
                    createCommand.Parameters.AddWithValue("@DateAvailable", newProduct.DateAvailable);
                    createCommand.Parameters.AddWithValue("@Condition", newProduct.Condition);
                    createCommand.Parameters.AddWithValue("@ItemDescription", newProduct.ItemDescription);
                    createCommand.Parameters.AddWithValue("@ItemAvailable", newProduct.ItemAvailable);

                    // Use non query because we are updating/changing the DB, not querying it
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(ProductViewModel updateProduct, int productID, int carPartID, int carID)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Product " +
                                                                     "SET OEM=@OEM, " +
                                                                     "Price=@Price, DateAvailable=@DateAvailable, Condition=@Condition, " +
                                                                     "ItemDescription=@ItemDescription, ItemAvailable=@ItemAvailable " +
                                                                     "WHERE ID = @ID AND CarPartID = @CarPartID AND CarID = @CarID AND ItemAvailable = 1", conn))
                    {
                        // Mapping method input values to sql query input values
                        updateCommand.Parameters.AddWithValue("@ID", productID);
                        updateCommand.Parameters.AddWithValue("@CarPartID", carPartID);
                        updateCommand.Parameters.AddWithValue("@CarID", carID);
                        updateCommand.Parameters.AddWithValue("@OEM", updateProduct.OEM);
                        updateCommand.Parameters.AddWithValue("@Price", updateProduct.Price);
                        updateCommand.Parameters.AddWithValue("@DateAvailable", updateProduct.DateAvailable);
                        updateCommand.Parameters.AddWithValue("@Condition", updateProduct.Condition);
                        updateCommand.Parameters.AddWithValue("@ItemDescription", updateProduct.ItemDescription);
                        updateCommand.Parameters.AddWithValue("@ItemAvailable", updateProduct.ItemAvailable);

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

        internal bool CarAndCarPartExists(string carPartName, string carVINNumber)
        {
            bool carPartExists = false;
            bool carExists = false;
            bool canProductBeCreated = false;

            carPartExists = _dbHelper.EntityExists("CarPart", "Name", carPartName);
            carExists = _dbHelper.EntityExists("Car", "VINNumber", carVINNumber);

            canProductBeCreated = carPartExists && carExists;

            return canProductBeCreated;

        }

        internal int GetCarPartIDByName(string value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string IDSelectorQuery = $"SELECT ID FROM CarPart WHERE Name = @Name";
                    using (SqlCommand checkCommand = new SqlCommand(IDSelectorQuery, conn))
                    {
                        // Add parameters to the query to prevent SQL injection
                        checkCommand.Parameters.AddWithValue("@Name", value);
                        var tempID = checkCommand.ExecuteScalar();
                        if(tempID != null && int.TryParse(tempID.ToString(), out int id))
                        {
                            return id;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error has occurred: {ex.Message}");
            }
            return 0;
        }

        internal int GetCarByVINNumber(string value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string IDSelectorQuery = $"SELECT ID FROM Car WHERE VINNumber = @VINNumber";
                    using (SqlCommand checkCommand = new SqlCommand(IDSelectorQuery, conn))
                    {
                        // Add parameters to the query to prevent SQL injection
                        checkCommand.Parameters.AddWithValue("@VINNumber", value);
                        var tempID = checkCommand.ExecuteScalar();
                        if (tempID != null && int.TryParse(tempID.ToString(), out int id))
                        {
                            return id;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error has occurred: {ex.Message}");
            }
            return 0; // Only returns 0 if there does not exist a product with the given OEM
        }

        internal int GetProductIDByOEM(string value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string IDSelectorQuery = $"SELECT ID FROM Product WHERE OEM = @OEM";
                    using (SqlCommand checkCommand = new SqlCommand(IDSelectorQuery, conn))
                    {
                        // Add parameters to the query to prevent SQL injection
                        checkCommand.Parameters.AddWithValue("@OEM", value);
                        var tempID = checkCommand.ExecuteScalar();
                        if (tempID != null && int.TryParse(tempID.ToString(), out int id))
                        {
                            return id;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error has occurred: {ex.Message}");
            }
            return 0; // Only returns 0 if there does not exist a product with the given OEM
        }

        internal string GetProductNameByID(int id)
        {
            string tempName = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string nameSelectorQuery = $"SELECT CarPart.Name FROM CarPart WHERE ID = @CarPartID";
                    using(SqlCommand getNameCommand = new SqlCommand(nameSelectorQuery, conn))
                    {

                        getNameCommand.Parameters.AddWithValue("@CarPartID", id);
                        var cmdResult = getNameCommand.ExecuteScalar();
                        if(cmdResult == null || cmdResult == DBNull.Value)
                        {
                            Debug.WriteLine("The CarPart.Name is NULL in the database.");
                            return string.Empty;
                        }
                        tempName = cmdResult.ToString();
                        if(string.IsNullOrWhiteSpace(tempName))
                        {
                            Debug.WriteLine($"The Name retrieved using ID '{id}' is erroneous.");
                            throw new InvalidDataException("Invalid Name retrieved.");
                        }
                    }
                    conn.Close();
                    return tempName;
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"An SQL error has likely occurred: {ex.Message}.");
            }
            return tempName;
        }

        internal string GetProductVINNumberByID(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string vinSelectorQuery = $"SELECT VINNumber FROM Car WHERE ID = @CarID";
                    using (SqlCommand getVinCommand = new SqlCommand(vinSelectorQuery, conn))
                    {
                        getVinCommand.Parameters.AddWithValue("@CarID", id);
                        var tempVin = getVinCommand.ExecuteScalar();
                        if (tempVin != null && !string.IsNullOrWhiteSpace(tempVin.ToString()))
                        {
                            return tempVin.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"An SQL error has likely occurred: {ex.Message}.");
            }
            return "";
        }
    }
}
