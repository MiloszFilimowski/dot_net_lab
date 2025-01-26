using Data.Entities;
using CarDealer.Models;

namespace CarDealer.Mappers
{
    public class CarMapper
    {
        public static CarViewModel FromEntity(CarEntity entity)
        {
            return new CarViewModel()
            {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                Price = entity.Price
            };
        }

        public static CarEntity ToEntity(CarViewModel model)
        {
            return new CarEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Brand = model.Brand,
                Model = model.Model,
                Price = model.Price
            };
        }
    }
}