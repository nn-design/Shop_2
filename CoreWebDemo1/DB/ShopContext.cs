using CoreWebDemo1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebDemo1.DB
{
    public class ShopContext: DbContext
    {
        //ef三种模式：database first，code first

        //code first

        //三个nugit包
        //Pomelo.EntityFrameworkCore.MySq
        //Microsoft.EntityFrameworkCore.Design
        //Microsoft.EntityFrameworkCore.Tools

        //两个命令
        //Add-Migration Init
        //Update-DataBase 





        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql("server=localhost;userid=root;pwd=123456;port=3306;database=core_demo;");
        //}
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductAttrKey> ProductAttrKeys { get; set; }
        public DbSet<ProductAttrValue> ProductAttrValues { get; set; }

    }
}

