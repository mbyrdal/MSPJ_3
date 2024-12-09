using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarPartControl
    {
        CarPart GetCarPartByID(int ID);
        List<CarPart> GetAllCarParts();
        bool AddCarPart(CarPart carPart);
        bool AddCarPartDTO(CarPartViewModel carPart);
        bool UpdateCarPart(CarPart carPart);
        bool UpdateCarPartDTO(CarPartViewModel carPart);
        bool DeleteCarPart(int ID);
        string GetCarPartName(int ID);
    }
}
