using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarControl
    {
        Car GetCarByID(int ID);
        List<Car> GetAllCars();
        bool AddCar(Car model);
        bool AddCarDTO(CarDTO model);
        bool UpdateCar(Car model);
        bool UpdateCarDTO(CarDTO model);
        bool DeleteCar(int ID);
    }
}
