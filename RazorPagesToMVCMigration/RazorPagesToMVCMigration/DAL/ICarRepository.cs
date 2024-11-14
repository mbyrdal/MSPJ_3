using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    public interface ICarRepository 
    {
        List<Car> GetAllCars();
        Car GetCarByID(int id);
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(string VINNumber);
        void Save();
    }
}
