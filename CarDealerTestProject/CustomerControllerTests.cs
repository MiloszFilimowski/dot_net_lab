using Microsoft.AspNetCore.Mvc;
using Moq;
using CarDealer.Controllers;
using CarDealer.Models;
using CarDealer.Services;

namespace WebApplicationTestProject
{
    public class CustomerControllerTests
    {
        List<CustomerViewModel> customers;
        Mock<ICustomerService> customerServiceMock;

        [SetUp]
        public void Setup()
        {
            // fill customers model mock
            customers = new List<CustomerViewModel>();
            customers.Add(new CustomerViewModel() { Id = 1, Name = "Asterix", Email = "asterix@gmail.com" });
            customers.Add(new CustomerViewModel() { Id = 2, Name = "Obelix", Email = "obelix@gmail.com" });

            // create service mock
            customerServiceMock = new Mock<ICustomerService>();
        }

        [Test]
        public void TestIndexAction()
        {
            customerServiceMock.Setup(m => m.FindAll()).Returns(customers);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Index();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<CustomerViewModel>>());
            var model = viewResult.Model as List<CustomerViewModel>;
            Assert.That(model, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestCreateGet()
        {
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Create();

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestCreatePost_ValidModel()
        {
            var customer = new CustomerViewModel { Name = "John Doe", Email = "john@example.com" };
            customerServiceMock.Setup(m => m.Add(It.IsAny<CustomerViewModel>())).Returns(1);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Create(customer);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            customerServiceMock.Verify(m => m.Add(It.IsAny<CustomerViewModel>()), Times.Once);
        }

        [Test]
        public void TestCreatePost_InvalidModel()
        {
            var customer = new CustomerViewModel { Name = "", Email = "invalid-email" };
            var customerController = new CustomerController(customerServiceMock.Object);
            customerController.ModelState.AddModelError("Name", "Name is required");

            var result = customerController.Create(customer);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
        }

        [Test]
        public void TestEditGet_ExistingCustomer()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
            customerServiceMock.Setup(m => m.FindById(1)).Returns(customer);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Edit(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CustomerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Doe"));
        }

        [Test]
        public void TestEditGet_NonExistingCustomer()
        {
            customerServiceMock.Setup(m => m.FindById(999)).Returns((CustomerViewModel)null);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Edit(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestEditPost_ValidModel()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Edit(customer);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            customerServiceMock.Verify(m => m.Update(It.IsAny<CustomerViewModel>()), Times.Once);
        }

        [Test]
        public void TestEditPost_InvalidModel()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "", Email = "invalid-email" };
            var customerController = new CustomerController(customerServiceMock.Object);
            customerController.ModelState.AddModelError("Name", "Name is required");

            var result = customerController.Edit(customer);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
        }

        [Test]
        public void TestDetails_ExistingCustomer()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
            customerServiceMock.Setup(m => m.FindById(1)).Returns(customer);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Details(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CustomerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Doe"));
        }

        [Test]
        public void TestDetails_NonExistingCustomer()
        {
            customerServiceMock.Setup(m => m.FindById(999)).Returns((CustomerViewModel)null);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Details(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteGet_ExistingCustomer()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
            customerServiceMock.Setup(m => m.FindById(1)).Returns(customer);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Delete(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Delete");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as CustomerViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
            Assert.That(model.Name, Is.EqualTo("John Doe"));
        }

        [Test]
        public void TestDeleteGet_NonExistingCustomer()
        {
            customerServiceMock.Setup(m => m.FindById(999)).Returns((CustomerViewModel)null);
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.Delete(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteConfirmed()
        {
            var customerController = new CustomerController(customerServiceMock.Object);

            var result = customerController.DeleteConfirmed(1);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            customerServiceMock.Verify(m => m.Delete(1), Times.Once);
        }

    }
}