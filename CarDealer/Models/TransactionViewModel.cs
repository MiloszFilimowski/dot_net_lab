using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CarDealer.Models
{
    public class TransactionViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Customer is required!")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Seller is required!")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "Car is required!")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Price is missing!")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number!")]
        public int Price { get; set; }

        [ValidateNever]
        public CustomerViewModel Customer { get; set; }

        [ValidateNever]
        public SellerViewModel Seller { get; set; }

        [ValidateNever]
        public CarViewModel Car { get; set; }
    }
}