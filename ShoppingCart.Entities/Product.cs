using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ShoppingCart.Entities
{
    public class Products
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int stock { get; set; }
        

    }
}
