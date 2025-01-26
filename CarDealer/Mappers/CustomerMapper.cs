using Data.Entities;
using CarDealer.Models;

namespace CarDealer.Mappers
{
    public class CustomerMapper
    {
        public static CustomerViewModel FromEntity(CustomerEntity entity)
        {
            return new CustomerViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
            };
        }

        public static CustomerEntity ToEntity(CustomerViewModel model)
        {
            return new CustomerEntity()
            {
                Id = model.Id.HasValue ? model.Id.Value : 0,
                Name = model.Name,
                Email = model.Email,
            };
        }
    }
}
