using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCar : ICRUD_DB<Car>
    {
        private readonly string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCar(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public async Task<List<Car>> GetAllEntitiesAsync()
        {
            var cars = new List<Car>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Car", conn))
                    {
                        using (SqlDataReader reader = await readAllCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var car = MapCarFromReader(reader);
                                cars.Add(car);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return cars;
        }

        public async Task<Car> GetByIdentifierAsync(int id)
        {
            Car car = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand readCommand = new SqlCommand(
                        "SELECT ID, CarTemplateID, VINNumber, ProductionYear, Mileage FROM Car WHERE ID = @ID", conn))
                    {
                        readCommand.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = await readCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                car = MapCarFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return car;
        }

        public async Task<int> CreateEntityAsync(Car newCar)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO Car (CarTemplateID, VINNumber, ProductionYear, Mileage) " +
                        "VALUES (@CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                    {
                        createCommand.Parameters.AddWithValue("@CarTemplateID", newCar.CarTemplateID);
                        createCommand.Parameters.AddWithValue("@VINNumber", newCar.VINNumber);
                        createCommand.Parameters.AddWithValue("@ProductionYear", newCar.ProductionYear);
                        createCommand.Parameters.AddWithValue("@Mileage", newCar.Mileage);

                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> CreateEntityDTOAsync(CarViewModel newCar)
        {
            int rowsInserted = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand createCommand = new SqlCommand(
                        "INSERT INTO Car (CarTemplateID, VINNumber, ProductionYear, Mileage) " +
                        "VALUES (@CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                    {
                        createCommand.Parameters.AddWithValue("@CarTemplateID", newCar.CarTemplateID);
                        createCommand.Parameters.AddWithValue("@VINNumber", newCar.VINNumber);
                        createCommand.Parameters.AddWithValue("@ProductionYear", newCar.ProductionYear);
                        createCommand.Parameters.AddWithValue("@Mileage", newCar.Mileage);

                        rowsInserted = await createCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return rowsInserted;
        }

        public async Task<int> UpdateEntityAsync(Car updatedCar)
        {
            int rowsUpdated = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE Car SET CarTemplateID=@CarTemplateID, VINNumber=@VINNumber, " +
                        "ProductionYear=@ProductionYear, Mileage=@Mileage WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updatedCar.ID);
                        updateCommand.Parameters.AddWithValue("@CarTemplateID", updatedCar.CarTemplateID);
                        updateCommand.Parameters.AddWithValue("@VINNumber", updatedCar.VINNumber);
                        updateCommand.Parameters.AddWithValue("@ProductionYear", updatedCar.ProductionYear);
                        updateCommand.Parameters.AddWithValue("@Mileage", updatedCar.Mileage);

                        rowsUpdated = await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return rowsUpdated;
        }

        public async Task<bool> DeleteEntityAsync(int id)
        {
            bool isDeleted = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Car WHERE ID = @ID", conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@ID", id);
                        isDeleted = await deleteCommand.ExecuteNonQueryAsync() == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error occurred: {ex.Message}");
            }
            return isDeleted;
        }

        // Check if a car exists
        public async Task<bool> CarExistsAsync(int id, string vinNumber)
        {
            var idExists = await _dbHelper.EntityExistsAsync("Car", "ID", id.ToString());
            var vinExists = await _dbHelper.EntityExistsAsync("Car", "VINNumber", vinNumber);
            return idExists && vinExists;
        }

        // Helper method to map Car from SqlDataReader
        private Car MapCarFromReader(SqlDataReader reader)
        {
            return new Car
            (
                reader.GetInt32(reader.GetOrdinal("ID")),
                reader.GetInt32(reader.GetOrdinal("CarTemplateID")),
                reader.GetString(reader.GetOrdinal("VINNumber")),
                reader.GetDateTime(reader.GetOrdinal("ProductionYear")),
                reader.GetInt32(reader.GetOrdinal("Mileage"))
            );
        }
    }
}
