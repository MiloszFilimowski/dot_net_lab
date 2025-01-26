using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Data.Entities
{
    [Table("car")]
    public class CarEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Brand { get; set; }

        [MaxLength(50)]
        [Required]
        public string Model { get; set; }

        [Required]
        public int Price { get; set; }

        public ICollection<TransactionEntity> Transactions { get; set; }
    }
}
