using Microsoft.Data.SqlClient;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCarPart : ICRUD_DB<CarPart>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCarPart(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<CarPart> GetAllEntities()
        {
            List<CarPart> carParts = new List<CarPart>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readAllCommand = new SqlCommand("SELECT * FROM CarPart", conn))
                {
                    using (SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarPart carPartsInTable = new CarPart(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetInt32(reader.GetOrdinal("CarID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("Notes")));

                            carParts.Add(carPartsInTable);
                        }
                    }
                }
                conn.Close();
            }
            return carParts;
        }

        public CarPart GetByIdentifier(int ID)
        {
            CarPart carPart = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand readCommand = new SqlCommand("SELECT ID, CarID, Name, Notes FROM CarPart WHERE ID = @ID", conn))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using (SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            carPart = new CarPart(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetInt32(reader.GetOrdinal("CarID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("Notes")));
                        }
                    }
                }
                conn.Close();
            }
            return carPart;
        }

        public int CreateEntity(CarPart newCarPart)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarPart (ID, CarID, Name, Notes) " +
                    "VALUES (@ID, @CarID, @Name, @Notes)", conn))
                {
                    createCommand.Parameters.AddWithValue("@ID", newCarPart.ID);
                    createCommand.Parameters.AddWithValue("@CarID", newCarPart.CarID);
                    createCommand.Parameters.AddWithValue("@Model", newCarPart.Name);
                    createCommand.Parameters.AddWithValue("@CarType", newCarPart.Notes);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        // DTO VERSION
        public int CreateEntityDTO(CarPartViewModel newCarPart)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarPart (CarID, Name, Notes) " +
                    "VALUES (@CarID, @Name, @Notes)", conn))
                {
                    createCommand.Parameters.AddWithValue("@CarID", newCarPart.CarID);
                    createCommand.Parameters.AddWithValue("@Model", newCarPart.Name);
                    createCommand.Parameters.AddWithValue("@CarType", newCarPart.Notes);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(CarPart updateCarPart)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarPart " +
                        "SET ID=@ID, CarID=@CarID, Name=@Name, Notes=@Notes " +
                        "WHERE ID=@ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarPart.ID);
                        updateCommand.Parameters.AddWithValue("@CarID", updateCarPart.CarID);
                        updateCommand.Parameters.AddWithValue("@Name", updateCarPart.Name);
                        updateCommand.Parameters.AddWithValue("@Notes", updateCarPart.Notes);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                return numberOfRowsUpdated;
            }
        }

        // DTO VERSION
        public int UpdateEntityDTO(CarPartViewModel updateCarPart)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarPart " +
                        "SET CarID=@CarID, Name=@Name, Notes=@Notes " +
                        "WHERE ID=@ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@CarID", updateCarPart.CarID);
                        updateCommand.Parameters.AddWithValue("@Name", updateCarPart.Name);
                        updateCommand.Parameters.AddWithValue("@Notes", updateCarPart.Notes);
                        updateCommand.Parameters.AddWithValue("@ID", updateCarPart.ID);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
                conn.Close();
            }
            return numberOfRowsUpdated;
        }

        public bool DeleteEntity(int ID)
        {
            bool wasCarPartDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from CarPart WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasCarPartDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasCarPartDeleted;
        }

        internal bool CarPartExists(int ID)
        {
            bool carPartIdentifierExists = _dbHelper.EntityExists("CarPart", "ID", ID.ToString());

            if (!carPartIdentifierExists)
            {
                return false;
            }

            return carPartIdentifierExists;
        }
    }
}
