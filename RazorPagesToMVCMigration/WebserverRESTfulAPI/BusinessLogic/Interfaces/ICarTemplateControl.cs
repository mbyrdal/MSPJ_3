using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarTemplateControl
    {
        CarTemplate GetCarTemplateByID(int ID);
        List<CarTemplate> GetAllCarTemplates();
        bool AddCarTemplate(CarTemplate model);
        bool AddCarTemplateDTO(CarTemplateViewModel model);
        bool UpdateCarTemplate(CarTemplate model);
        bool UpdateCarTemplateDTO(CarTemplateViewModel model);
        bool DeleteCarTemplate(int ID);
    }
}
