using FrontEnd.Repository;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly CarRepository _carRepository;

        public BaseController(CarRepository carRepository)
        {
            
           _carRepository = carRepository;
        }

        public IActionResult Index()
        {
            List<Car> allCars = _carRepository.GetAllCars();
            return View(allCars);
        }
    }
}
