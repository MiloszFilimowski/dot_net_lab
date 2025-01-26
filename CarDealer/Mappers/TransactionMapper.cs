using Data.Entities;
using CarDealer.Models;

namespace CarDealer.Mappers
{
    public class TransactionMapper
    {
        public static TransactionViewModel FromEntity(TransactionEntity entity)
        {
            return new TransactionViewModel()
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                SellerId = entity.SellerId,
                CarId = entity.CarId,
                Price = entity.Price,
                Customer = CustomerMapper.FromEntity(entity.Customer),
                Seller = SellerMapper.FromEntity(entity.Seller),
                Car = CarMapper.FromEntity(entity.Car)
            };
        }

        public static TransactionEntity ToEntity(TransactionViewModel model)
        {
            return new TransactionEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                CustomerId = model.CustomerId,
                SellerId = model.SellerId,
                CarId = model.CarId,
                Price = model.Price
            };
        }
    }
}