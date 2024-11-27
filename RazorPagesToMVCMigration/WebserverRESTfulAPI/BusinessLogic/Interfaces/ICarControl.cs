using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarControl
    {
        Car GetCarByID(int ID);
        List<Car> GetAllCars();
        bool AddCar(Car model);
        bool AddCarDTO(CarModelViewModel model);
        bool UpdateCar(Car model);
        bool UpdateCarDTO(CarModelViewModel model);
        bool DeleteCar(int ID);
    }
}
