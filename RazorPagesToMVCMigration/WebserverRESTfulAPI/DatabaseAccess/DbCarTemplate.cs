using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCarTemplate : ICRUD_DB<CarTemplate>
    {
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCarTemplate(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<List<CarTemplate>> GetAllEntitiesAsync()
        {
            var carTemplates = new List<CarTemplate>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM CarTemplate", conn))
                    {
                        using (SqlDataReader reader = await readAllCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carTemplate = MapCarTemplateFromReader(reader);
                                carTemplates.Add(carTemplate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving car templates: {ex.Message}");
            }
            return carTemplates;
        }

        public async Task<CarTemplate> GetByIdentifierAsync(int ID)
        {
            CarTemplate carTemplate = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readCommand = new SqlCommand(
                        "SELECT ID, Brand, Model, CarType FROM CarTemplate WHERE ID = @ID", conn))
                    {
                        readCommand.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = await readCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                carTemplate = MapCarTemplateFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving car template with ID {ID}: {ex.Message}");
            }
            return carTemplate;
        }

        public async Task<int> CreateEntityAsync(CarTemplate newCarTemplate)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO CarTemplate (Brand, Model, CarType) VALUES (@Brand, @Model, @CarType)", conn))
                    {
                        createCommand.Parameters.AddWithValue("@Brand", newCarTemplate.Brand);
                        createCommand.Parameters.AddWithValue("@Model", newCarTemplate.Model);
                        createCommand.Parameters.AddWithValue("@CarType", newCarTemplate.CarType);

                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating a car template: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> UpdateEntityAsync(CarTemplate updateCarTemplate)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarTemplate SET Brand = @Brand, Model = @Model, CarType = @CarType WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarTemplate.ID);
                        updateCommand.Parameters.AddWithValue("@Brand", updateCarTemplate.Brand);
                        updateCommand.Parameters.AddWithValue("@Model", updateCarTemplate.Model);
                        updateCommand.Parameters.AddWithValue("@CarType", updateCarTemplate.CarType);

                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating car template with ID {updateCarTemplate.ID}: {ex.Message}");
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
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM CarTemplate WHERE ID = @ID", conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@ID", ID);
                        isDeleted = await deleteCommand.ExecuteNonQueryAsync() == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting car template with ID {ID}: {ex.Message}");
            }
            return isDeleted;
        }

        public async Task<bool> CarTemplateExistsAsync(int ID)
        {
            try
            {
                return await _dbHelper.EntityExistsAsync("CarTemplate", "ID", ID.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while checking if car template exists with ID {ID}: {ex.Message}");
                return false;
            }
        }

        // Helper Method to Map CarTemplate from SqlDataReader
        private CarTemplate MapCarTemplateFromReader(SqlDataReader reader)
        {
            return new CarTemplate(
                reader.GetInt32(reader.GetOrdinal("ID")),
                reader.GetString(reader.GetOrdinal("Brand")),
                reader.GetString(reader.GetOrdinal("Model")),
                reader.GetString(reader.GetOrdinal("CarType"))
            );
        }
    }
}
