using CarDealer.Models;

namespace CarDealer.Services
{
    public interface ICarService
    {
        int Add(CarViewModel item);
        void Delete(int id);
        void Update(CarViewModel item);
        List<CarViewModel> FindAll();
        CarViewModel? FindById(int id);
    }
}