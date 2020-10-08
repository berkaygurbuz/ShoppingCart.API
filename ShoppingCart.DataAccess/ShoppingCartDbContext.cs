
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataAccess
{
    public class ShoppingCartDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //DEĞİŞECEK KISIM KENDİ SERVER ADINI VE USERNAME PASSWORD GİRİNİZ.
            optionsBuilder.UseSqlServer("Server=DESKTOP-0Q2H04V; Database=ShoppingCartDb;uid=sa;pwd=1234;MultipleActiveResultSets=true;");    
    }
        public DbSet<Products> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}
