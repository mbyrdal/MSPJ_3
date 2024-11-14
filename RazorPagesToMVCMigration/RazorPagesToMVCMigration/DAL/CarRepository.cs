using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    ConfigurationHelper cfgHelper = new ConfigurationHelper;
    public class CarRepository : ICarRepository
    {
        public void CreateCar(Car car)
        {
            throw new NotImplementedException();
        }

        public void DeleteCar(string VINNumber)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllCars()
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
