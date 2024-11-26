using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCarModel : ICRUD_DB<CarModel>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCarModel(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<CarModel> GetAllEntities()
        {
            List<CarModel> carModels = new List<CarModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM CarModel", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarModel carModelsInTable = new CarModel(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetInt32(reader.GetOrdinal("CarTemplateID")),
                                reader.GetString(reader.GetOrdinal("VINNumber")),
                                reader.GetDateTime(reader.GetOrdinal("ProductionYear")),
                                reader.GetInt32(reader.GetOrdinal("Mileage")));

                            carModels.Add(carModelsInTable);
                        }
                    }
                }
                conn.Close();
            }
            return carModels;
        }

        public CarModel GetByIdentifier(int ID)
        {
            CarModel carModel = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand readCommand = new SqlCommand("SELECT ID, CarTemplateID, VINNumber, ProductionYear, Mileage FROM CarModel WHERE ID = @ID", conn))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using(SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            carModel = new CarModel(
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
            return carModel;
        }

        public int CreateEntity(CarModel newCarModel)
        {
            int numberOfRowsInserted;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarModel (ID, CarTemplateID, VINNumber, ProductionYear, Mileage " +
                    "VALUES (@ID, @CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                {
                    createCommand.Parameters.AddWithValue("@ID", newCarModel.ID);
                    createCommand.Parameters.AddWithValue("@CarTemplateID", newCarModel.CarTemplateID);
                    createCommand.Parameters.AddWithValue("@VINNumber", newCarModel.VINNumber);
                    createCommand.Parameters.AddWithValue("@ProductionYear", newCarModel.ProductionYear);
                    createCommand.Parameters.AddWithValue("@Mileage", newCarModel.Mileage);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int CreateEntityDTO(CarModelViewModel newCarModel)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarModel (CarTemplateID, VINNumber, ProductionYear, Mileage " +
                    "VALUES (@CarTemplateID, @VINNumber, @ProductionYear, @Mileage)", conn))
                {
                    createCommand.Parameters.AddWithValue("@CarTemplateID", newCarModel.CarTemplateID);
                    createCommand.Parameters.AddWithValue("@VINNumber", newCarModel.VINNumber);
                    createCommand.Parameters.AddWithValue("@ProductionYear", newCarModel.ProductionYear);
                    createCommand.Parameters.AddWithValue("@Mileage", newCarModel.Mileage);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(CarModel updateCarModel)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE CarModel " +
                                                                     "SET ID=@ID, CarTemplateID=@CarTemplateID, VINNumber=@VINNumber, " +
                                                                     "ProductionYear=@ProductionYear, Mileage=@Mileage " +
                                                                     "WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarModel.ID);
                        updateCommand.Parameters.AddWithValue("@CarTemplateID", updateCarModel.CarTemplateID);
                        updateCommand.Parameters.AddWithValue("@VINNumber", updateCarModel.VINNumber);
                        updateCommand.Parameters.AddWithValue("@ProductionYear", updateCarModel.ProductionYear);
                        updateCommand.Parameters.AddWithValue("@Mileage", updateCarModel.Mileage);

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

        public int UpdateEntityDTO(CarModelViewModel updateCarModel)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand("UPDATE CarModel " +
                                                                     "SET VINNumber=@VINNumber, " +
                                                                     "ProductionYear=@ProductionYear, Mileage=@Mileage " +
                                                                     "WHERE ID = @ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarModel.ID);
                        updateCommand.Parameters.AddWithValue("@VINNumber", updateCarModel.VINNumber);
                        updateCommand.Parameters.AddWithValue("@ProductionYear", updateCarModel.ProductionYear);
                        updateCommand.Parameters.AddWithValue("@Mileage", updateCarModel.Mileage);

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
            bool wasCarModelDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from CarModel WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasCarModelDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasCarModelDeleted;
        }

        internal bool CarModelExists(int ID, string VINNumber)
        {
            bool carModelExistsIdentifierExists = _dbHelper.EntityExists("CarModel", "ID", ID.ToString());
            bool carModelVinExists = _dbHelper.EntityExists("CarModel", "VINNumber", VINNumber);

            bool carModelExists = carModelExistsIdentifierExists && carModelVinExists;

            if (!carModelExists)
            {
                return false;
            }

            return carModelExists;
        }
    }
}
