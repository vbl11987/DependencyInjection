using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    public class HomeDIController : Controller
    {
        private IRepository repository;
        private ProductTotalizer totalizer;

        public HomeDIController(IRepository repo, ProductTotalizer total){
            repository = repo;
            totalizer = total;
        }

        public ViewResult Index() {
            ViewBag.Total = totalizer.Total.ToString();
            ViewBag.HomeController = repository.ToString();
            return View(repository.Products);
        }
    }
}