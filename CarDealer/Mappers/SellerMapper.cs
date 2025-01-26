using Data.Entities;
using CarDealer.Models;

namespace CarDealer.Mappers
{
    public class SellerMapper
    {
        public static SellerViewModel FromEntity(SellerEntity entity)
        {
            return new SellerViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
        }

        public static SellerEntity ToEntity(SellerViewModel model)
        {
            return new SellerEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Name = model.Name,
                Email = model.Email
            };
        }
    }
}