using FrontEnd.Models;
using FrontEnd.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CarRepository _repository;

        public IndexModel(CarRepository repository)
        {
            _repository = repository;
        }

        public List<Car> Cars { get; set; }

        public void OnGet()
        {
            Cars = _repository.GetAllCars();
        }
    }
}
