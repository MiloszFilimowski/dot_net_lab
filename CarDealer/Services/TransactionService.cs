using Data;
using Data.Entities;
using CarDealer.Mappers;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class TransactionService : ITransactionService
    {
        private CarDealerContext _context;

        public TransactionService(CarDealerContext context)
        {
            _context = context;
        }

        public int Add(TransactionViewModel transaction)
        {
            var e = _context.Transactions.Add(TransactionMapper.ToEntity(transaction));
            _context.SaveChanges();
            return e.Entity.Id;
        }

        public void Delete(int id)
        {
            TransactionEntity? find = _context.Transactions.Find(id);
            if (find != null)
            {
                _context.Transactions.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<TransactionViewModel> FindAll()
        {
            return _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Seller)
                .Include(t => t.Car)
                .Select(e => TransactionMapper.FromEntity(e))
                .ToList();
        }

        public TransactionViewModel? FindById(int id)
        {
            return TransactionMapper.FromEntity(
                _context.Transactions
               .Include(t => t.Customer)
               .Include(t => t.Seller)
               .Include(t => t.Car)
               .First(e => e.Id == id)
            );
        }

        public void Update(TransactionViewModel transaction)
        {
            _context.Transactions.Update(TransactionMapper.ToEntity(transaction));
            _context.SaveChanges();
        }
    }
}