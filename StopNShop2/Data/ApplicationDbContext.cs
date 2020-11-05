using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StopNShop2.Models;

namespace StopNShop2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StopNShop2.Models.Category> Category { get; set; }
        public DbSet<StopNShop2.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<StopNShop2.Models.Product> Product { get; set; }
        public DbSet<StopNShop2.Models.ProductReview> ProductReview { get; set; }
        public DbSet<StopNShop2.Models.ShoppingCart> ShoppingCart { get; set; }
        public DbSet<StopNShop2.Models.WishList> WishList { get; set; }
        public object FilesOnDatabase { get; internal set; }
        public object FilesOnFileSystem { get; internal set; }
        public DbSet<StopNShop2.Models.ImageUpload> ImageUpload { get; set; }
    }
}
