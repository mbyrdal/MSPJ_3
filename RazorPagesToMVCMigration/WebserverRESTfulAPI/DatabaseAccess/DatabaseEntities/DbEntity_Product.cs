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

        // Helper method that tests whether an entity (Product) entry exists in the database.
        // NOT IN USE YET
        internal bool ProductExists(string OEM)
        {
            bool prodExists = false;
            using (SqlConnection conn = new SqlConnection( _connectionString ))
            {
                conn.Open();
                string existQuery = "SELECT COUNT(1) FROM Product WHERE OEM = @OEM";
                using (SqlCommand checkCommand = new SqlCommand(existQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@OEM", OEM);
                    prodExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                }
                conn.Close();
            }
            return prodExists;
        }

        // AddProduct(Product product)
        public int Create(Product entity)
        {
            // bool productExistsInDB = false;
            int numberOfRowsInserted;

            // productExistsInDB = ProductExists(entity.OEM);
            /*
            if(!productExistsInDB)
            {
                // INSERT OK
                
            }
            else
            {
                // UPDATE -> throw error indicating existing Product in DB.
            }
            */
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Product (OEM, VINNumber, Name, Price, DateAvailable, Notes) "
                    + "VALUES (@OEM, @VINNumber, @Name, @Price, @DateAvailable, @Notes)", conn
                    ))
                {
                    // Mapping method input values to sql query input values
                    createCommand.Parameters.AddWithValue("@OEM", entity.OEM);
                    createCommand.Parameters.AddWithValue("@VINNumber", entity.VINNumber);
                    createCommand.Parameters.AddWithValue("@Name", entity.Name);
                    createCommand.Parameters.AddWithValue("@Price", entity.Price);
                    createCommand.Parameters.AddWithValue("@DateAvailable", entity.DateAvailable);
                    createCommand.Parameters.AddWithValue("@Notes", entity.Notes);

                    // Insert, update, delete
                    // Use non query, because we are updating/changing the DB, not querying it
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        // DeleteProduct (string OEM)
        public bool Delete(string OEM)
        {
            bool wasProductDeleted = false;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand deleteCommand = new SqlCommand("DELETE from Product WHERE OEM = @productOEM", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@productOEM", OEM);
                    
                    // Track number of rows affected (changes made to DB)
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();

                    // Succession criteria: only one (1) row should be affected, ie one Product deleted
                    wasProductDeleted = (numberOfRowsAffectedByDeletion == 1);
                }
                conn.Close();
            }
            return wasProductDeleted;
        }

        // GetAllProducts() -> outputs a list of all products in the DB/inventory.
        public IEnumerable<Product> GetAll()
        {
            List<Product> allProducts = new List<Product>();

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
                using (SqlCommand readCommand = new SqlCommand("SELECT OEM, FK_VINNumber, Name, Price, DateAvailable, Notes FROM Product WHERE OEM = @OEM", conn))
                {
                    // Bind value from string input OEM to parameter OEM from Product in DB.
                    readCommand.Parameters.AddWithValue("@OEM", OEM);

                    conn.Open();
                    using (SqlDataReader reader = readCommand.ExecuteReader())
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
                
            }
            return product;
        }

        // UpdateProduct (Product product) -> Updates an existing Product in the database.
        public int Update(Product entity)
        {
            // bool productExistInDB = true;
            int numberOfRowsUpdated;
            // productExistsInDB = ProductExists(entity.OEM);
            /*
            if(!productExistsInDB)
            {
                // INSERT OK
                
            }
            else
            {
                // UPDATE -> throw error indicating existing Product in DB.
            }
            */
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand updateCommand = new SqlCommand("UPDATE Product SET Name=@Name, Price=@Price, DateAvailable=@DateAvailable, Notes=@Notes", conn))
                {
                    // Mapping method input values to sql query input values
                    updateCommand.Parameters.AddWithValue("@Name", entity.Name);
                    updateCommand.Parameters.AddWithValue("@Price", entity.Price);
                    updateCommand.Parameters.AddWithValue("@DateAvailable", entity.DateAvailable);
                    updateCommand.Parameters.AddWithValue("@Notes", entity.Notes);

                    // Insert, update, delete
                    // Use non query, because we are updating/changing the DB, not querying it
                    numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsUpdated;
        }
    }
}
