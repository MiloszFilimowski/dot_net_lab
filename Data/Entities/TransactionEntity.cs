using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Data.Entities
{
    [Table("transaction")]
    public class TransactionEntity
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        [Required]
        public CustomerEntity Customer { get; set; }


        public int SellerId { get; set; }
        [Required]
        public SellerEntity Seller { get; set; }


        public int CarId { get; set; }
        [Required]
        public CarEntity Car { get; set; }

        [Required]
        public int Price { get; set; }
    }

}
