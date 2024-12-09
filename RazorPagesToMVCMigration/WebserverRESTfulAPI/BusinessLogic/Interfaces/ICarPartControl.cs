using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarPartControl
    {
        CarPart GetCarPartByID(int ID);
        List<CarPart> GetAllCarParts();
        bool AddCarPart(CarPart model);
        bool AddCarPartDTO(CarPartViewModel model);
        bool UpdateCarPart(CarPart model);
        bool UpdateCarPartDTO(CarPartViewModel model);
        bool DeleteCarPart(int ID);
    }
}
