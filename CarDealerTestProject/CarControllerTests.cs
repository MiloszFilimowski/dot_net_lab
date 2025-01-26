using Microsoft.AspNetCore.Mvc;
using Moq;
using CarDealer.Controllers;
using CarDealer.Models;
using CarDealer.Services;

namespace WebApplicationTestProject
{
    public class CarControllerTests
    {
        List<CarViewModel> cars;
        Mock<ICarService> carServiceMock;

        [SetUp]
        public void Setup()
        {
            // fill cars model mock
            cars = new List<CarViewModel>();
            cars.Add(new CarViewModel() { Id = 1, Brand = "Toyota", Model = "Corolla", Price = 20000 });
            cars.Add(new CarViewModel() { Id = 2, Brand = "Honda", Model = "Civic", Price = 22000 });

            // create service mock
            carServiceMock = new Mock<ICarService>();
        }

        [Test]
        public void TestIndexAction()
        {
            carServiceMock.Setup(m => m.FindAll()).Returns(cars);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Index();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<CarViewModel>>());
            var model = viewResult.Model as List<CarViewModel>;
            Assert.That(model, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestCreateGet()
        {
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Create();

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestCreatePost_ValidModel()
        {
            var car = new CarViewModel { Brand = "Toyota", Model = "Camry", Price = 25000 };
            carServiceMock.Setup(m => m.Add(It.IsAny<CarViewModel>())).Returns(1);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Create(car);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            carServiceMock.Verify(m => m.Add(It.IsAny<CarViewModel>()), Times.Once);
        }

        [Test]
        public void TestCreatePost_InvalidModel()
        {
            var car = new CarViewModel { Brand = "", Model = "", Price = -1000 };
            var carController = new CarController(carServiceMock.Object);
            carController.ModelState.AddModelError("Brand", "Brand is required");

            var result = carController.Create(car);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestEditGet_ExistingCar()
        {
            var car = new CarViewModel { Id = 1, Brand = "Toyota", Model = "Camry", Price = 25000 };
            carServiceMock.Setup(m => m.FindById(1)).Returns(car);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Edit(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CarViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Toyota"));
        }

        [Test]
        public void TestEditGet_NonExistingCar()
        {
            carServiceMock.Setup(m => m.FindById(999)).Returns((CarViewModel)null);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Edit(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestEditPost_ValidModel()
        {
            var car = new CarViewModel { Id = 1, Brand = "Toyota", Model = "Camry", Price = 25000 };
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Edit(car);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            carServiceMock.Verify(m => m.Update(It.IsAny<CarViewModel>()), Times.Once);
        }

        [Test]
        public void TestEditPost_InvalidModel()
        {
            var car = new CarViewModel { Id = 1, Brand = "", Model = "", Price = -1000 };
            var carController = new CarController(carServiceMock.Object);
            carController.ModelState.AddModelError("Brand", "Brand is required");

            var result = carController.Edit(car);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
        }

        [Test]
        public void TestDetails_ExistingCar()
        {
            var car = new CarViewModel { Id = 1, Brand = "Toyota", Model = "Camry", Price = 25000 };
            carServiceMock.Setup(m => m.FindById(1)).Returns(car);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Details(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CarViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Toyota"));
        }

        [Test]
        public void TestDetails_NonExistingCar()
        {
            carServiceMock.Setup(m => m.FindById(999)).Returns((CarViewModel)null);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Details(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteGet_ExistingCar()
        {
            var car = new CarViewModel { Id = 1, Brand = "Toyota", Model = "Camry", Price = 25000 };
            carServiceMock.Setup(m => m.FindById(1)).Returns(car);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Delete(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Delete");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CarViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Toyota"));
        }

        [Test]
        public void TestDeleteGet_NonExistingCar()
        {
            carServiceMock.Setup(m => m.FindById(999)).Returns((CarViewModel)null);
            var carController = new CarController(carServiceMock.Object);

            var result = carController.Delete(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteConfirmed()
        {
            var carController = new CarController(carServiceMock.Object);

            var result = carController.DeleteConfirmed(1);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            carServiceMock.Verify(m => m.Delete(1), Times.Once);
        }
    }
}