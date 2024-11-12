using FrontEnd.Repository;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly CarRepository _repository;

        public BaseController(CarRepository repository)
        {
           _repository = repository;
        }

        public IActionResult Index()
        {
            List<Car> allCars = _repository.GetAllCars();
            return View(allCars);
        }
    }
}
