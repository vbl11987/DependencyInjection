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

        //inserting services directly to the action method, you can erase the parameter un the constructor
        //for ProductTotalizer and erase the dependency, you can use the service inside the action
        public ViewResult IndexActionInjection([FromServices]ProductTotalizer totalizer){
            ViewBag.HomeController = repository.ToString();
            ViewBag.ProductTotalizer = totalizer.Repository.ToString();
            return View(repository.Products);
        }
    }
}