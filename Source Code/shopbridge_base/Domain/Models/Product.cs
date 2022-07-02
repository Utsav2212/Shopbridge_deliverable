using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Product_Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(4000)")]
        [StringLength(4000, ErrorMessage = "Please enter valid product name!", MinimumLength = 3)]
        public string ProductName { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Please enter valid description name!")]
        public string Description { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Please enter valid comapny name!")]
        public string ComapnyName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool IsActive { get; set; }


    }
}
