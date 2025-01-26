using Data;
using Data.Entities;
using CarDealer.Mappers;
using CarDealer.Models;

namespace CarDealer.Services
{
    public class CarService : ICarService
    {
        private CarDealerContext _context;

        public CarService(CarDealerContext context)
        {
            _context = context;
        }

        public int Add(CarViewModel car)
        {
            var e = _context.Cars.Add(CarMapper.ToEntity(car));
            _context.SaveChanges();
            return e.Entity.Id;
        }

        public void Delete(int id)
        {
            CarEntity? find = _context.Cars.Find(id);
            if (find != null)
            {
                _context.Cars.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<CarViewModel> FindAll()
        {
            return _context.Cars.Select(e => CarMapper.FromEntity(e)).ToList();
        }

        public CarViewModel? FindById(int id)
        {
            return CarMapper.FromEntity(_context.Cars.Find(id));
        }

        public void Update(CarViewModel car)
        {
            _context.Cars.Update(CarMapper.ToEntity(car));
            _context.SaveChanges();
        }
    }
} 