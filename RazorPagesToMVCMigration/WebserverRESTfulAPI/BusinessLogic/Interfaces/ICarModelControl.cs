using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarModelControl
    {
        CarModel GetCarModelByID(int ID);
        List<CarModel> GetAllCarModels();
        bool AddCarModel(CarModel model);
        bool AddCarModelDTO(CarModelViewModel model);
        bool UpdateCarModel(CarModel model);
        bool UpdateCarModelDTO(CarModelViewModel model);
        bool DeleteCarModel(int ID);
    }
}
