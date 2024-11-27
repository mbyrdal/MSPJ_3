using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

namespace ServiceAPI.DatabaseAccess
{
    public class DbCarTemplate : ICRUD_DB<CarTemplate>
    {
        // Configuration steps
        private string _connectionString;
        private readonly DbHelper _dbHelper;

        public DbCarTemplate(IConfiguration configuration)
        {
            _dbHelper = new DbHelper(configuration);
            ConnectionHelper helper = new ConnectionHelper(configuration);
            _connectionString = helper.GetDBConnectionString();
        }

        public List<CarTemplate> GetAllEntities()
        {
            List<CarTemplate> carTemplates = new List<CarTemplate>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand readAllCommand = new SqlCommand("SELECT * FROM CarTemplate", conn))
                {
                    using(SqlDataReader reader = readAllCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarTemplate carTemplatesInTable = new CarTemplate(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Brand")),
                                reader.GetString(reader.GetOrdinal("Model")),
                                reader.GetString(reader.GetOrdinal("CarType")));

                            carTemplates.Add(carTemplatesInTable);
                        }
                    }
                }
                conn.Close();
            }
            return carTemplates;
        }

        public CarTemplate GetByIdentifier(int ID)
        {
            CarTemplate carTemplate = null;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand readCommand = new SqlCommand("SELECT ID, Brand, Model, CarType FROM CarTemplate WHERE ID = @ID"))
                {
                    readCommand.Parameters.AddWithValue("@ID", ID);
                    using(SqlDataReader reader = readCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            carTemplate = new CarTemplate(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Brand")),
                                reader.GetString(reader.GetOrdinal("Model")),
                                reader.GetString(reader.GetOrdinal("CarType")));
                        }
                    }
                }
                conn.Close();
            }
            return carTemplate;
        }

        public int CreateEntity(CarTemplate newCarTemplate)
        {
            int numberOfRowsInserted;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using(SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarTemplate (ID, Brand, Model, CarType) " +
                    "VALUES (@ID, @Brand, @Model, @CarType)", conn))
                {
                    createCommand.Parameters.AddWithValue("@ID", newCarTemplate.ID);
                    createCommand.Parameters.AddWithValue("@Brand", newCarTemplate.Brand);
                    createCommand.Parameters.AddWithValue("@Model", newCarTemplate.Model);
                    createCommand.Parameters.AddWithValue("@CarType", newCarTemplate.CarType);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        // DTO VERSION
        public int CreateEntityDTO(CarTemplateViewModel newCarTemplate)
        {
            int numberOfRowsInserted;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand createCommand = new SqlCommand(
                    "INSERT INTO CarTemplate (Brand, Model, CarType) " +
                    "VALUES (@Brand, @Model, @CarType)", conn))
                {
                    createCommand.Parameters.AddWithValue("@Brand", newCarTemplate.Brand);
                    createCommand.Parameters.AddWithValue("@Model", newCarTemplate.Model);
                    createCommand.Parameters.AddWithValue("@CarType", newCarTemplate.CarType);
                    numberOfRowsInserted = createCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            return numberOfRowsInserted;
        }

        public int UpdateEntity(CarTemplate updateCarTemplate)
        {
            int numberOfRowsUpdated = 0;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarTemplate " +
                        "SET ID=@ID, Brand=@Brand, Model=@Model, CarType=@CarType " +
                        "WHERE ID=@ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@ID", updateCarTemplate.ID);
                        updateCommand.Parameters.AddWithValue("@Brand", updateCarTemplate.Brand);
                        updateCommand.Parameters.AddWithValue("@Model", updateCarTemplate.Model);
                        updateCommand.Parameters.AddWithValue("@CarType", updateCarTemplate.CarType);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
            }
        }

        // DTO VERSION
        public int UpdateEntityDTO(CarTemplateViewModel updateCarTemplate)
        {
            int numberOfRowsUpdated = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand updateCommand = new SqlCommand(
                        "UPDATE CarTemplate " +
                        "SET Brand=@Brand, Model=@Model, CarType=@CarType " +
                        "WHERE ID=@ID", conn))
                    {
                        updateCommand.Parameters.AddWithValue("@Brand", updateCarTemplate.Brand);
                        updateCommand.Parameters.AddWithValue("@Model", updateCarTemplate.Model);
                        updateCommand.Parameters.AddWithValue("@CarType", updateCarTemplate.CarType);

                        numberOfRowsUpdated = updateCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error occurred: {ex.Message}");
                }
            }
        }

        public bool DeleteEntity(int ID)
        {
            bool wasCarTemplateDeleted = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand deleteCommand = new SqlCommand("DELETE from CarTemplate WHERE ID = @ID", conn))
                {
                    deleteCommand.Parameters.AddWithValue("@ID", ID);
                    int numberOfRowsAffectedByDeletion = deleteCommand.ExecuteNonQuery();
                    wasCarTemplateDeleted = numberOfRowsAffectedByDeletion == 1;
                }
                conn.Close();
            }
            return wasCarTemplateDeleted;
        }
    }
}
