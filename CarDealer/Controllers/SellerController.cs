using Data;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Models;
using CarDealer.Mappers;
using CarDealer.Services;

namespace CarDealer.Controllers
{
    public class SellerController : Controller
    {
        private ISellerService _service;
        public SellerController(ISellerService service)
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
        public IActionResult Create(SellerViewModel seller)
        {
            if (ModelState.IsValid)
            {
                this._service.Add(seller);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var seller = _service.FindById(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpPost]
        public IActionResult Edit(SellerViewModel seller)
        {
            if (ModelState.IsValid)
            {
                _service.Update(seller);
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var seller = _service.FindById(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var seller = _service.FindById(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
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