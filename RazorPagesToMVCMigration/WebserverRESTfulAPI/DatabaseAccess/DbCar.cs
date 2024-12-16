using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCar : ICRUD_DB<Car>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCar(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<Car> GetAllEntities()
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM Car", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car carsInTable = new Car(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetInt32(reader.GetOrdinal("CarTemplateID")),
                                reader.GetString(reader.GetOrdinal("VINNumber")),
                                reader.GetDateTime(reader.GetOrdinal("ProductionYear")),
                                reader.GetInt32(reader.GetOrdinal("Mileage")));

                            cars.Add(carsInTable);
                        }
                    }
                }
                conn.Close();
            }
            return cars;
        }

        public Car GetByIdentifier(int ID)
        {
            Car car = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand readCommand = new SqlCommand("SELECT ID, CarTemplateID, VINNumber, ProductionYear, Mileage FROM Car WHERE ID = @ID", conn))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using(SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            car = new Car(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetInt32(reader.GetOrdinal("CarTemplateID")),
                                reader.GetString(reader.GetOrdinal("VINNumber")),
                                reader.GetDateTime(reader.GetOrdinal("ProductionYear")),
                                reader.GetInt32(reader.GetOrdinal("Mileage")));
                        }
                    }
                }
                conn.Close();
            }
            return car;
        }

        public int CreateEntity(Car newCar)
        {
            int numberOfRowsInserted;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Car (ID, CarTemplateID, VINNumber, ProductionYear, Mileage " +
                    "VALUES (@ID, @CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                {
                    createCommand.Parameters.AddWithValue("@ID", newCar.ID);
                    createCommand.Parameters.AddWithValue("@CarTemplateID", newCar.CarTemplateID);
                    createCommand.Parameters.AddWithValue("@VINNumber", newCar.VINNumber);
                    createCommand.Parameters.AddWithValue("@ProductionYear", newCar.ProductionYear);
                    createCommand.Parameters.AddWithValue("@Mileage", newCar.Mileage);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int CreateEntityDTO(CarDTO newCar)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO Car (CarTemplateID, VINNumber, ProductionYear, Mileage " +
                    "VALUES (@CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                {
                    createCommand.Parameters.AddWithValue("@CarTemplateID", newCar.CarTemplateID);
                    createCommand.Parameters.AddWithValue("@VINNumber", newCar.VINNumber);
                    createCommand.Parameters.AddWithValue("@ProductionYear", newCar.ProductionYear);
                    createCommand.Parameters.AddWithValue("@Mileage", newCar.Mileage);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(Car updateCar)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Car " +
                                                                     "SET ID=@ID, CarTemplateID=@CarTemplateID, VINNumber=@VINNumber, " +
                                                                     "ProductionYear=@ProductionYear, Mileage=@Mileage " +
                                                                     "WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCar.ID);
                        updateCommand.Parameters.AddWithValue("@CarTemplateID", updateCar.CarTemplateID);
                        updateCommand.Parameters.AddWithValue("@VINNumber", updateCar.VINNumber);
                        updateCommand.Parameters.AddWithValue("@ProductionYear", updateCar.ProductionYear);
                        updateCommand.Parameters.AddWithValue("@Mileage", updateCar.Mileage);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                return numberOfRowsUpdated;
            }
        }

        public int UpdateEntityDTO(CarDTO updateCar)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE Car " +
                                                                     "SET VINNumber=@VINNumber, " +
                                                                     "ProductionYear=@ProductionYear, Mileage=@Mileage " +
                                                                     "WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCar.ID);
                        updateCommand.Parameters.AddWithValue("@VINNumber", updateCar.VINNumber);
                        updateCommand.Parameters.AddWithValue("@ProductionYear", updateCar.ProductionYear);
                        updateCommand.Parameters.AddWithValue("@Mileage", updateCar.Mileage);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                return numberOfRowsUpdated;
            }
        }

        public bool DeleteEntity(int ID)
        {
            bool wasCarDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from Car WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasCarDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasCarDeleted;
        }

        internal bool CarExists(int ID, string VINNumber)
        {
            bool carExistsIdentifierExists = _dbHelper.EntityExists("Car", "ID", ID.ToString());
            bool carVinExists = _dbHelper.EntityExists("Car", "VINNumber", VINNumber);

            bool carExists = carExistsIdentifierExists && carVinExists;

            if (!carExists)
            {
                return false;
            }

            return carExists;
        }
    }
}
