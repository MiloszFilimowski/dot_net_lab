using Data;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Models;
using CarDealer.Mappers;
using CarDealer.Services;
using Microsoft.AspNetCore.Authorization;

namespace CarDealer.Controllers
{
    [Authorize(Roles = "admin")]
    public class TransactionController : Controller
    {
        private ITransactionService _service;
        private ICustomerService _customerService;
        private ISellerService _sellerService;
        private ICarService _carService;

        public TransactionController(
            ITransactionService service,
            ICustomerService customerService,
            ISellerService sellerService,
            ICarService carService)
        {
            _service = service;
            _customerService = customerService;
            _sellerService = sellerService;
            _carService = carService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_service.FindAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Customers = _customerService.FindAll();
            ViewBag.Sellers = _sellerService.FindAll();
            ViewBag.Cars = _carService.FindAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                this._service.Add(transaction);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = _customerService.FindAll();
            ViewBag.Sellers = _sellerService.FindAll();
            ViewBag.Cars = _carService.FindAll();
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var transaction = _service.FindById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewBag.Customers = _customerService.FindAll();
            ViewBag.Sellers = _sellerService.FindAll();
            ViewBag.Cars = _carService.FindAll();
            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                _service.Update(transaction);
                return RedirectToAction("Index");
            }
            ViewBag.Customers = _customerService.FindAll();
            ViewBag.Sellers = _sellerService.FindAll();
            ViewBag.Cars = _carService.FindAll();
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var transaction = _service.FindById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var transaction = _service.FindById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
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