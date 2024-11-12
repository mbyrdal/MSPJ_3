using FrontEnd.Models;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace FrontEnd.Services
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Car> GetAllCars()
        {
            // List of all cars in the dbo.Cars table
            var carsInDB = new List<Car>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Car", conn);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var carItem = new Car
                        {
                            VINNumber = reader.GetString(0),
                            Manufactor = reader.GetString(1),
                            Model = reader.GetString(2),
                            ProductionYear = reader.GetInt32(3),
                            Mileage = reader.GetInt32(4)
                        };
                        carsInDB.Add(carItem);
                    }
                }
                conn.Close();
            }
            return carsInDB;
        }
    }
}
