using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppPelda.Models;
using WebAppPelda.Services;

namespace WebAppPelda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Vasarlo(int id)
        {
            Customer customer = new CustomerService().GetById(id);
            return View(customer);
        }

        public IActionResult Sajat()
        {
            List<Customer> customers = new CustomerService().GetAllCustomer();
            return View(customers);
        }

        //CreateCustomer dolgai
        [HttpGet]
        public IActionResult CreateCustomer() 
        {
            Customer emptyCustomer = new Customer();
            return View(emptyCustomer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer) 
        {
            string result = new CustomerService().PostCustomer(customer);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(CreateCustomer));
        }

        //PutCustomer dolgai
        [HttpGet]
        public IActionResult PutCustomer(int? id)
        {
            Customer customer = new Customer();
            if (id.HasValue)
            {
                customer = new CustomerService().GetById(id.Value);
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult PutCustomer(Customer customer)
        {
            string result = new CustomerService().PutCustomer(customer);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(PutCustomer), new { id = customer.Id });
        }

        //DeleteCustomer dolgai
        [HttpGet]
        public IActionResult DeleteCustomer(int? id)
        {
            Customer customer = new Customer();
            if (id.HasValue)
            {
                customer = new CustomerService().GetById(id.Value);
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(Customer customer)
        {
            string result = new CustomerService().DeleteCustomer(customer.Id);
            TempData["SuccessMessage"] = result;


            return RedirectToAction(nameof(DeleteCustomer));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
