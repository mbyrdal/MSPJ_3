using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
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

        public CarModel GetByIdentifier(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(CarModel entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateEntity(CarModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
