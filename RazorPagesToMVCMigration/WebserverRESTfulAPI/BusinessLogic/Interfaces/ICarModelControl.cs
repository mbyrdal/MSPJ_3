using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarModelControl
    {
        Car GetCarModelByID(int ID);
        List<Car> GetAllCarModels();
        bool AddCarModel(Car model);
        bool AddCarModelDTO(CarModelViewModel model);
        bool UpdateCarModel(Car model);
        bool UpdateCarModelDTO(CarModelViewModel model);
        bool DeleteCarModel(int ID);
    }
}
