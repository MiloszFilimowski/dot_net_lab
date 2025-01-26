using Microsoft.AspNetCore.Mvc;
using Moq;
using CarDealer.Controllers;
using CarDealer.Models;
using CarDealer.Services;

namespace WebApplicationTestProject
{
    public class SellerControllerTests
    {
        List<SellerViewModel> sellers;
        Mock<ISellerService> sellerServiceMock;

        [SetUp]
        public void Setup()
        {
            // fill sellers model mock
            sellers = new List<SellerViewModel>();
            sellers.Add(new SellerViewModel() { Id = 1, Name = "John Smith", Email = "john@gmail.com" });
            sellers.Add(new SellerViewModel() { Id = 2, Name = "Jane Doe", Email = "jane@gmail.com" });

            // create service mock
            sellerServiceMock = new Mock<ISellerService>();
        }

        [Test]
        public void TestIndexAction()
        {
            sellerServiceMock.Setup(m => m.FindAll()).Returns(sellers);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Index();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<SellerViewModel>>());
            var model = viewResult.Model as List<SellerViewModel>;
            Assert.That(model, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestCreateGet()
        {
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Create();

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestCreatePost_ValidModel()
        {
            var seller = new SellerViewModel { Name = "John Smith", Email = "john@example.com" };
            sellerServiceMock.Setup(m => m.Add(It.IsAny<SellerViewModel>())).Returns(1);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Create(seller);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            sellerServiceMock.Verify(m => m.Add(It.IsAny<SellerViewModel>()), Times.Once);
        }

        [Test]
        public void TestCreatePost_InvalidModel()
        {
            var seller = new SellerViewModel { Name = "", Email = "invalid-email" };
            var sellerController = new SellerController(sellerServiceMock.Object);
            sellerController.ModelState.AddModelError("Name", "Name is required");

            var result = sellerController.Create(seller);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestEditGet_ExistingSeller()
        {
            var seller = new SellerViewModel { Id = 1, Name = "John Smith", Email = "john@example.com" };
            sellerServiceMock.Setup(m => m.FindById(1)).Returns(seller);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Edit(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as SellerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Smith"));
        }

        [Test]
        public void TestEditGet_NonExistingSeller()
        {
            sellerServiceMock.Setup(m => m.FindById(999)).Returns((SellerViewModel)null);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Edit(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestEditPost_ValidModel()
        {
            var seller = new SellerViewModel { Id = 1, Name = "John Smith", Email = "john@example.com" };
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Edit(seller);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            sellerServiceMock.Verify(m => m.Update(It.IsAny<SellerViewModel>()), Times.Once);
        }

        [Test]
        public void TestEditPost_InvalidModel()
        {
            var seller = new SellerViewModel { Id = 1, Name = "", Email = "invalid-email" };
            var sellerController = new SellerController(sellerServiceMock.Object);
            sellerController.ModelState.AddModelError("Name", "Name is required");

            var result = sellerController.Edit(seller);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
        }

        [Test]
        public void TestDetails_ExistingSeller()
        {
            var seller = new SellerViewModel { Id = 1, Name = "John Smith", Email = "john@example.com" };
            sellerServiceMock.Setup(m => m.FindById(1)).Returns(seller);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Details(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as SellerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Smith"));
        }

        [Test]
        public void TestDetails_NonExistingSeller()
        {
            sellerServiceMock.Setup(m => m.FindById(999)).Returns((SellerViewModel)null);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Details(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteGet_ExistingSeller()
        {
            var seller = new SellerViewModel { Id = 1, Name = "John Smith", Email = "john@example.com" };
            sellerServiceMock.Setup(m => m.FindById(1)).Returns(seller);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Delete(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Delete");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as SellerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Smith"));
        }

        [Test]
        public void TestDeleteGet_NonExistingSeller()
        {
            sellerServiceMock.Setup(m => m.FindById(999)).Returns((SellerViewModel)null);
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.Delete(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteConfirmed()
        {
            var sellerController = new SellerController(sellerServiceMock.Object);

            var result = sellerController.DeleteConfirmed(1);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            sellerServiceMock.Verify(m => m.Delete(1), Times.Once);
        }
    }
}