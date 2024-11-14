using Microsoft.Data.SqlClient;
using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    public class CarRepository : ICarRepository
    {
        // Fetch connection string from appsettings.json
        string connectionString = ConfigurationHelper.GetDBConnectionString();
        public List<Car> GetAllCars()
        {
            List<Car> allCars = new List<Car>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlCommand sqlQuery = new SqlCommand("SELECT * FROM Cars"))
                {
                    using(SqlDataReader reader = sqlQuery.ExecuteReader())
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

        public void CreateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(string VINNumber)
        {
            throw new NotImplementedException();
        }

        public Car GetCarByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
