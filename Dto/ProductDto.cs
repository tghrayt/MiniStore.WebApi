using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Dto
{
    public class ProductDto
    {

        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public DateTime Productmanufacturing { get; set; }
        [Required]
        public int Category_Prodcut_Id { get; set; }

    }
}
