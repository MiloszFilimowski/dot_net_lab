using Data;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Models;
using CarDealer.Mappers;
using CarDealer.Services;

namespace CarDealer.Controllers
{
    public class CarController : Controller
    {
        private ICarService _service;
        public CarController(ICarService service)
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
        public IActionResult Create(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                this._service.Add(car);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = _service.FindById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                _service.Update(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var car = _service.FindById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var car = _service.FindById(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
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