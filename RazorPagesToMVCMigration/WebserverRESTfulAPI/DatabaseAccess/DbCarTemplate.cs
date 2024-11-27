using Microsoft.Data.SqlClient;
using ServiceAPI.DatabaseAccess.Interfaces;
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

        public int CreateEntity(CarTemplate entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateEntity(CarTemplate entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
