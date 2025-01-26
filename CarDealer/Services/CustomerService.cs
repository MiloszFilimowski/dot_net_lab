using Data;
using Data.Entities;
using System;
using CarDealer.Mappers;
using CarDealer.Models;

namespace CarDealer.Services
{
    public class CustomerService : ICustomerService
    {
        private CarDealerContext _context;

        public CustomerService(CarDealerContext context)
        {
            _context = context;
        }

        public int Add(CustomerViewModel contact)
        {
            var e = _context.Customers.Add(CustomerMapper.ToEntity(contact));
            _context.SaveChanges();
            return e.Entity.Id;
        }

        public void Delete(int id)
        {
            CustomerEntity? find = _context.Customers.Find(id);
            if (find != null)
            {
                _context.Customers.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<CustomerViewModel> FindAll()
        {
            return _context.Customers.Select(e => CustomerMapper.FromEntity(e)).ToList(); ;
        }

        public CustomerViewModel? FindById(int id)
        {
            return CustomerMapper.FromEntity(_context.Customers.Find(id));
        }

        public void Update(CustomerViewModel contact)
        {
            _context.Customers.Update(CustomerMapper.ToEntity(contact));
            _context.SaveChanges();
        }
    }
}
