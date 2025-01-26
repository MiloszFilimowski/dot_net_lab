using CarDealer.Models;

namespace CarDealer.Services
{
    public interface ICustomerService
    {
        int Add(CustomerViewModel item);
        void Delete(int id);
        void Update(CustomerViewModel item);
        List<CustomerViewModel> FindAll();
        CustomerViewModel? FindById(int id);
    }
}
