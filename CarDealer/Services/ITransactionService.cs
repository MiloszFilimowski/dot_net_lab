using CarDealer.Models;

namespace CarDealer.Services
{
    public interface ITransactionService
    {
        int Add(TransactionViewModel item);
        void Delete(int id);
        void Update(TransactionViewModel item);
        List<TransactionViewModel> FindAll();
        TransactionViewModel? FindById(int id);
    }
} 