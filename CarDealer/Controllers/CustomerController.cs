using Data;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Models;
using CarDealer.Mappers;
using CarDealer.Services;

namespace CarDealer.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_service.FindAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                this._service.Add(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _service.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                _service.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var customer = _service.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = _service.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}