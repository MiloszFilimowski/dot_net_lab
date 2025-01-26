using Microsoft.AspNetCore.Mvc;
using Moq;
using CarDealer.Controllers;
using CarDealer.Models;
using CarDealer.Services;

namespace WebApplicationTestProject
{
    public class TransactionControllerTests
    {
        List<TransactionViewModel> transactions;
        Mock<ITransactionService> transactionServiceMock;
        Mock<ICustomerService> customerServiceMock;
        Mock<ISellerService> sellerServiceMock;
        Mock<ICarService> carServiceMock;

        [SetUp]
        public void Setup()
        {
            var customer = new CustomerViewModel { Id = 1, Name = "John Doe", Email = "john@example.com" };
            var seller = new SellerViewModel { Id = 1, Name = "Jane Smith", Email = "jane@example.com" };
            var car = new CarViewModel { Id = 1, Brand = "Toyota", Model = "Corolla", Price = 20000 };

            transactions = new List<TransactionViewModel>
            {
                new TransactionViewModel 
                { 
                    Id = 1, 
                    CustomerId = 1, 
                    SellerId = 1, 
                    CarId = 1, 
                    Price = 20000,
                    Customer = customer,
                    Seller = seller,
                    Car = car
                }
            };

            transactionServiceMock = new Mock<ITransactionService>();
            customerServiceMock = new Mock<ICustomerService>();
            sellerServiceMock = new Mock<ISellerService>();
            carServiceMock = new Mock<ICarService>();

            customerServiceMock.Setup(m => m.FindAll()).Returns(new List<CustomerViewModel> { customer });
            sellerServiceMock.Setup(m => m.FindAll()).Returns(new List<SellerViewModel> { seller });
            carServiceMock.Setup(m => m.FindAll()).Returns(new List<CarViewModel> { car });
        }

        [Test]
        public void TestIndexAction()
        {
            transactionServiceMock.Setup(m => m.FindAll()).Returns(transactions);
            var controller = CreateTransactionController();

            var result = controller.Index();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.TypeOf<List<TransactionViewModel>>());
            var model = viewResult.Model as List<TransactionViewModel>;
            Assert.That(model, Has.Count.EqualTo(1));
        }

             [Test]
        public void TestCreateGet()
        {
            var controller = CreateTransactionController();

            var result = controller.Create();

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
            Assert.IsNotNull(viewResult.ViewData["Customers"]);
            Assert.IsNotNull(viewResult.ViewData["Sellers"]);
            Assert.IsNotNull(viewResult.ViewData["Cars"]);
        }

        [Test]
        public void TestCreatePost_InvalidModel()
        {
            var transaction = new TransactionViewModel { Price = -1000 };
            var controller = CreateTransactionController();
            controller.ModelState.AddModelError("Price", "Price must be positive");

            var result = controller.Create(transaction);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create");
            Assert.IsNotNull(viewResult.ViewData["Customers"]);
            Assert.IsNotNull(viewResult.ViewData["Sellers"]);
            Assert.IsNotNull(viewResult.ViewData["Cars"]);
        }

        [Test]
        public void TestEditGet_ExistingTransaction()
        {
            transactionServiceMock.Setup(m => m.FindById(1)).Returns(transactions[0]);
            var controller = CreateTransactionController();

            var result = controller.Edit(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.Model);
            Assert.IsNotNull(viewResult.ViewData["Customers"]);
            Assert.IsNotNull(viewResult.ViewData["Sellers"]);
            Assert.IsNotNull(viewResult.ViewData["Cars"]);
            var model = viewResult.Model as TransactionViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestEditPost_InvalidModel()
        {
            var transaction = new TransactionViewModel { Id = 1, Price = -1000 };
            var controller = CreateTransactionController();
            controller.ModelState.AddModelError("Price", "Price must be positive");

            var result = controller.Edit(transaction);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
            Assert.IsNotNull(viewResult.ViewData["Customers"]);
            Assert.IsNotNull(viewResult.ViewData["Sellers"]);
            Assert.IsNotNull(viewResult.ViewData["Cars"]);
        }

        [Test]
        public void TestDetails_ExistingTransaction()
        {
            transactionServiceMock.Setup(m => m.FindById(1)).Returns(transactions[0]);
            var controller = CreateTransactionController();

            var result = controller.Details(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as TransactionViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestDetails_NonExistingTransaction()
        {
            transactionServiceMock.Setup(m => m.FindById(999)).Returns((TransactionViewModel)null);
            var controller = CreateTransactionController();

            var result = controller.Details(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteGet_ExistingTransaction()
        {
            transactionServiceMock.Setup(m => m.FindById(1)).Returns(transactions[0]);
            var controller = CreateTransactionController();

            var result = controller.Delete(1);

            Assert.IsNotNull(result);
            Assert.True(result is ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Delete");
            Assert.IsNotNull(viewResult.Model);
            var model = viewResult.Model as TransactionViewModel;
            Assert.That(model.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestDeleteGet_NonExistingTransaction()
        {
            transactionServiceMock.Setup(m => m.FindById(999)).Returns((TransactionViewModel)null);
            var controller = CreateTransactionController();

            var result = controller.Delete(999);

            Assert.IsNotNull(result);
            Assert.True(result is NotFoundResult);
        }

        [Test]
        public void TestDeleteConfirmed()
        {
            var controller = CreateTransactionController();

            var result = controller.DeleteConfirmed(1);

            Assert.IsNotNull(result);
            Assert.True(result is RedirectToActionResult);
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            transactionServiceMock.Verify(m => m.Delete(1), Times.Once);
        }

        private TransactionController CreateTransactionController()
        {
            return new TransactionController(
                transactionServiceMock.Object,
                customerServiceMock.Object,
                sellerServiceMock.Object,
                carServiceMock.Object
            );
        }
    }
}
