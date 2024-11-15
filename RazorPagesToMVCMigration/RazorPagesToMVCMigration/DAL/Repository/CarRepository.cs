using Microsoft.Data.SqlClient;
using RazorPagesToMVCMigration.DAL.Interface;
using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL.Repository
{
    public class CarRepository : ICRUD<Car>
    {
        private string _connectionString;
        public CarRepository(IConfiguration configuration)
        {
            var configHelper = new ConfigurationHelper(configuration);
            _connectionString = configHelper.GetDBConnectionString();
        }

        public void Create(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAll()
        {
            List<Car> allCars = new List<Car>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand sqlQuery = new SqlCommand("SELECT * FROM Car", conn))
                {
                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car carInTable = new Car
                            {
                                VINNumber = reader.GetString(0), // Column 1, VINNumber ...
                                Brand = reader.GetString(1),
                                Model = reader.GetString(2),
                                ProductionYear = reader.GetInt32(3),
                                Mileage = reader.GetInt32(4)
                            };
                            allCars.Add(carInTable);
                        }
                    }
                }
            }
            return allCars;
        }

        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}
