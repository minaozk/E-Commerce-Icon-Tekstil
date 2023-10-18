
using System;
using System.Reflection;
using IconTekstil.Models;
using IconTekstil.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IconTekstil.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
		}

		public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Kadın", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Yeni Gelenler", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Büyük Beden", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Company>().HasData(
	            new Company
	            {
					Id = 1,
					Name = "ICON TEKSTIL",
					StreetAddress = "Defterdar Mah. Otakçılar Cad.",
					City = "İstanbul",
					PostalCode = "34394",
					State = "Eyüp",
					PhoneNumber = "02125013889"
	            }
	         
            );


			modelBuilder.Entity<ShoppingCart>().HasKey(t => t.Id);

			modelBuilder.Entity<Product>().HasData(

	            new Product
	            {
		            Id = 1,
		            Title = "Icon 1655 Kadın Ceket",
		            Description = "KADIN CEKET",
		            ISBN = "8680910472931",
		            Price = 2930,
					CategoryId = 1,
					

	            },
	            new Product
	            {
		            Id = 2,
		            Title = "Icon KB-BB Kadın Şimal Mont",
		            Description = "KADIN ŞİMAL MONT",
		            ISBN = "8680910456641",
		            Price = 3000,
					CategoryId = 1,
					

				},
	            new Product
	            {
		            Id = 3,
		            Title = "Icon 1648 KB-BB Kadın Şimal Mont",
		            Description = "KADIN ŞİMAL MONT",
		            ISBN = "8680910456283",
		            Price = 3040,
					CategoryId = 1,
					

				},
	            new Product
	            {
		            Id = 4,
		            Title = "Icon 1644 Kadın Bambu Trençkot",
		            Description = "KADIN BAMBU TRENÇKOT",
		            ISBN = "8680910472290",
		            Price = 2930,
					CategoryId = 2,
					

				},
	            new Product
	            {
		            Id = 5,
		            Title = "Icon 1099 KB-BB Kadın Mont",
		            Description = "KADIN MONT",
		            ISBN = "8680910451561",
		            Price = 3800,
					CategoryId = 1,
					

				},
	            new Product
	            {
		            Id = 6,
		            Title = "Icon 1641 Kadın Ceket",
		            Description = "KADIN CEKET",
		            ISBN = "8680910455660",
		            Price = 2930,
					CategoryId = 2,
					

				});
		}

        }
    }

