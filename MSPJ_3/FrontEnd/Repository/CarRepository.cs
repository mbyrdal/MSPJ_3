using FrontEnd.Models;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace FrontEnd.Repository
{
    public class CarRepository
    {
        private readonly string _connectionString;

        public CarRepository(IConfiguration configuration)
        {
            // Retrieve the connection string from the appsettings.json file.
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Car> GetAllCars()
        {
            // List of all cars in the dbo.Cars table
            var carsInDB = new List<Car>();

            // Using ADO.NET to connect and fetch DB data.
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // SQL query (command) to fetch all rows from the table dbo.Car
                SqlCommand cmd = new SqlCommand("SELECT * FROM Car", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var carItem = new Car
                        {
                            VINNumber = reader.GetString(0), // Column index 0, 1, 2 ...
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
