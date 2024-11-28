using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICarTemplateControl
    {
        CarTemplate GetCarTemplateByID(int ID);
        List<CarTemplate> GetAllCarTemplates();
        bool AddCarTemplate(CarTemplate model);
        bool UpdateCarTemplate(CarTemplate model);
        bool DeleteCarTemplate(int ID);
    }
}
