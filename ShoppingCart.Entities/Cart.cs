using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ShoppingCart.Entities
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int productId { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        public int piece { get; set; }
        public decimal price { get; set; }
        public decimal? totalPrice { get; set; }


    }
}
