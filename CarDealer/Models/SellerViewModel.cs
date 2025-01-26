using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Models
{
    public class SellerViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is missing!")]
        public string Name { get; set; }

        [RegularExpression(pattern: ".+\\@.+\\.[a-z]{2,3}", ErrorMessage = "Incorrect email format!")]
        [Required(ErrorMessage = "Email is missing or incorrect!")]
        public string Email { get; set; }
    }
}