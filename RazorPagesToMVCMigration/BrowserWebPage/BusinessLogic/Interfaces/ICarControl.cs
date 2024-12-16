using BrowserWebPage.DTOs;
using BrowserWebPage.Models;

namespace BrowserWebPage.BusinessLogic.Interfaces
{
    public interface ICarControl
    {
        Car GetCarByID(int ID);
        List<Car> GetAllCars();
        bool AddCar(Car model);
        bool AddCarDTO(CarViewModel model);
        bool UpdateCar(Car model);
        bool UpdateCarDTO(CarViewModel model);
        bool DeleteCar(int ID);
    }
}
