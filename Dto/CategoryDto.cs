using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Dto
{
    public class CategoryDto
    {

        public int CategoryId { get; set; }


        [Required]
        public string CategoryName { get; set; }

    }
}
