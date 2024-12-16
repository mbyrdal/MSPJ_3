using BrowserWebPage.DTOs;
using BrowserWebPage.Models;

namespace BrowserWebPage.BusinessLogic.Interfaces
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
