using Data;
using Data.Entities;
using CarDealer.Mappers;
using CarDealer.Models;

namespace CarDealer.Services
{
    public class SellerService : ISellerService
    {
        private CarDealerContext _context;

        public SellerService(CarDealerContext context)
        {
            _context = context;
        }

        public int Add(SellerViewModel seller)
        {
            var e = _context.Sellers.Add(SellerMapper.ToEntity(seller));
            _context.SaveChanges();
            return e.Entity.Id;
        }

        public void Delete(int id)
        {
            SellerEntity? find = _context.Sellers.Find(id);
            if (find != null)
            {
                _context.Sellers.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<SellerViewModel> FindAll()
        {
            return _context.Sellers.Select(e => SellerMapper.FromEntity(e)).ToList();
        }

        public SellerViewModel? FindById(int id)
        {
            return SellerMapper.FromEntity(_context.Sellers.Find(id));
        }

        public void Update(SellerViewModel seller)
        {
            _context.Sellers.Update(SellerMapper.ToEntity(seller));
            _context.SaveChanges();
        }
    }
} 