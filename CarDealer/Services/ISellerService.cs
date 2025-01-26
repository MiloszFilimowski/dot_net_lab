using CarDealer.Models;

namespace CarDealer.Services
{
    public interface ISellerService
    {
        int Add(SellerViewModel item);
        void Delete(int id);
        void Update(SellerViewModel item);
        List<SellerViewModel> FindAll();
        SellerViewModel? FindById(int id);
    }
} 