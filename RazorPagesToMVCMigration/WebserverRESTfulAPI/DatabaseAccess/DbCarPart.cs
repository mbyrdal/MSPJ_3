using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCarPart : ICRUD_DB<CarPart>
    {
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCarPart(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<List<CarPart>> GetAllEntitiesAsync()
        {
            var carParts = new List<CarPart>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM CarPart", conn))
                    {
                        using (SqlDataReader reader = await readAllCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carPart = MapCarPartFromReader(reader);
                                carParts.Add(carPart);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving car parts: {ex.Message}");
            }
            return carParts;
        }

        public async Task<CarPart> GetByIdentifierAsync(int ID)
        {
            CarPart carPart = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readCommand = new SqlCommand(
                        "SELECT ID, CarID, Name, Notes FROM CarPart WHERE ID = @ID", conn))
                    {
                        readCommand.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = await readCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                carPart = MapCarPartFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving car part with ID {ID}: {ex.Message}");
            }
            return carPart;
        }

        public async Task<int> CreateEntityAsync(CarPart newCarPart)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO CarPart (CarID, Name, Notes) VALUES (@CarID, @Name, @Notes)", conn))
                    {
                        createCommand.Parameters.AddWithValue("@CarID", newCarPart.CarID);
                        createCommand.Parameters.AddWithValue("@Name", newCarPart.Name);
                        createCommand.Parameters.AddWithValue("@Notes", newCarPart.Notes);

                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating a car part: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> UpdateEntityAsync(CarPart updateCarPart)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarPart SET CarID = @CarID, Name = @Name, Notes = @Notes WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarPart.ID);
                        updateCommand.Parameters.AddWithValue("@CarID", updateCarPart.CarID);
                        updateCommand.Parameters.AddWithValue("@Name", updateCarPart.Name);
                        updateCommand.Parameters.AddWithValue("@Notes", updateCarPart.Notes);

                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating car part with ID {updateCarPart.ID}: {ex.Message}");
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
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM CarPart WHERE ID = @ID", conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@ID", ID);
                        isDeleted = await deleteCommand.ExecuteNonQueryAsync() == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting car part with ID {ID}: {ex.Message}");
            }
            return isDeleted;
        }

        public async Task<bool> CarPartExistsAsync(int ID)
        {
            try
            {
                return await _dbHelper.EntityExistsAsync("CarPart", "ID", ID.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while checking if car part exists with ID {ID}: {ex.Message}");
                return false;
            }
        }

        // Helper Method to Map CarPart from SqlDataReader
        private CarPart MapCarPartFromReader(SqlDataReader reader)
        {
            return new CarPart(
                reader.GetInt32(reader.GetOrdinal("ID")),
                reader.GetInt32(reader.GetOrdinal("CarID")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("Notes"))
            );
        }
    }
}
