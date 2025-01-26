using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Models
{
    public class CarViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Brand is missing!")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is missing!")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Price is missing!")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number!")]
        public int Price { get; set; }
    }
}