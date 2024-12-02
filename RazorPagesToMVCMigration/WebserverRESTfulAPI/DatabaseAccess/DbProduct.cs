using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess
{
    public class DbProduct : ICRUD_DB<Product>
    {
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbProduct(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<List<Product>> GetAllEntitiesAsync()
        {
            var products = new List<Product>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Product", conn))
                    {
                        using (SqlDataReader reader = await readAllCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var product = MapProductFromReader(reader);
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
            }
            return products;
        }

        public async Task<Product> GetByIdentifierAsync(int ID)
        {
            Product product = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readCommand = new SqlCommand(
                        "SELECT ID, CarPartID, CarID, OEM, Price, DateAvailable, Condition, ItemDescription, ItemAvailable " +
                        "FROM Product WHERE ID = @ID", conn))
                    {
                        readCommand.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = await readCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                product = MapProductFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product with ID {ID}: {ex.Message}");
            }
            return product;
        }

        public async Task<int> CreateEntityAsync(Product newProduct)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO Product (CarPartID, CarID, OEM, Price, DateAvailable, Condition, ItemDescription, ItemAvailable) " +
                        "VALUES (@CarPartID, @CarID, @OEM, @Price, @DateAvailable, @Condition, @ItemDescription, @ItemAvailable)", conn))
                    {
                        AddProductParameters(createCommand, newProduct);
                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> UpdateEntityAsync(Product updateProduct)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE Product SET OEM = @OEM, Price = @Price, DateAvailable = @DateAvailable, Condition = @Condition, " +
                        "ItemDescription = @ItemDescription, ItemAvailable = @ItemAvailable WHERE ID = @ID", conn))
                    {
                        AddProductParameters(updateCommand, updateProduct);
                        updateCommand.Parameters.AddWithValue("@ID", updateProduct.ID);
                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product with ID {updateProduct.ID}: {ex.Message}");
            }
            return rowsUpdated;
        }

        public async Task<bool> DeleteEntityAsync(int ID)
        {
            bool isDeleted = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Product WHERE ID = @ID", conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@ID", ID);
                        isDeleted = await deleteCommand.ExecuteNonQueryAsync() == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product with ID {ID}: {ex.Message}");
            }
            return isDeleted;
        }

        public async Task<int> GetProductIDByOEMAsync(string OEM)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SELECT ID FROM Product WHERE OEM = @OEM", conn))
                    {
                        command.Parameters.AddWithValue("@OEM", OEM);
                        var result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            return id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product ID by OEM {OEM}: {ex.Message}");
            }
            return 0;
        }

        public async Task<bool> ProductExistsAsync(int ID)
        {
            try
            {
                return await _dbHelper.EntityExistsAsync("Product", "ID", ID.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existence of product with ID {ID}: {ex.Message}");
                return false;
            }
        }

        // Helper Methods
        private Product MapProductFromReader(SqlDataReader reader)
        {
            return new Product
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

        private void AddProductParameters(SqlCommand command, Product product)
        {
            command.Parameters.AddWithValue("@CarPartID", product.CarPartID);
            command.Parameters.AddWithValue("@CarID", product.CarID);
            command.Parameters.AddWithValue("@OEM", product.OEM);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@DateAvailable", product.DateAvailable);
            command.Parameters.AddWithValue("@Condition", product.Condition);
            command.Parameters.AddWithValue("@ItemDescription", product.ItemDescription);
            command.Parameters.AddWithValue("@ItemAvailable", product.ItemAvailable);
        }
    }
}
