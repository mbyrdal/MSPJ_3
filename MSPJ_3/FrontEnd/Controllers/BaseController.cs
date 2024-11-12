using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private readonly Repository _repository;

        public BaseController(Repository repository)
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
