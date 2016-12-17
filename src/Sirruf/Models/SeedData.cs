using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sirruf.Data;
using System;
using System.Linq;

namespace Sirruf.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Purchase.Any())
                {
                    return;   // DB has been seeded
                }

                var categories = new Category[]
                {
                    new Category {Name = "Category1", IsPublic = true},
                    new Category {Name = "Category2", IsPublic = true},
                    new Category {Name = "Category3"},
                };
                foreach (Category c in categories)
                {
                    context.Category.Add(c);
                }
                context.SaveChanges();

                var shops = new Shop[]
                {
                    new Shop {Name = "Lidl", Grade = 3 },
                    new Shop {Name = "Real", Grade = 6 },
                    new Shop {Name = "Tesco", Grade = 9 },
                };
                foreach (Shop s in shops)
                {
                    context.Shop.Add(s);
                }
                context.SaveChanges();

                var brands = new Brand[]
                {
                    new Brand {Name = "Nike", Grade = 2 },
                    new Brand {Name = "Awidzi", Grade = 4 },
                    new Brand {Name = "4f", Grade = 6 },
                };
                foreach (Brand b in brands)
                {
                    context.Brand.Add(b);
                }
                context.SaveChanges();

                var purchases = new Purchase[]
                {
                    new Purchase {ShopID = 1, BrandID = 1, CategoryID = 1, Name = "Purchase1", Grade = 10, Price = 1, IsPublic = true, PurchaseDate = DateTime.Parse("2016-12-12") },
                    new Purchase {ShopID = 2, BrandID = 2, CategoryID = 1, Name = "Purchase2", Grade = 9, Price = 3, IsPublic = true, PurchaseDate = DateTime.Parse("2016-11-11") },
                    new Purchase {ShopID = 3, BrandID = 3, CategoryID = 2, Name = "Purchase3", Grade = 8, Price = 5, PurchaseDate = DateTime.Parse("2016-10-10") }
                };
                foreach (Purchase p in purchases)
                {
                    context.Purchase.Add(p);
                }
                context.SaveChanges();

                /*var comments = new Comment[]
                {
                    new Comment {Content = "10/10, polecam allegrowicza", IsPublic = false, CreationDate = DateTime.Parse("2017-11-21") },
                    new Comment {Content = "tak", IsPublic = true, CreationDate = DateTime.Parse("2017-11-20") },
                    new Comment {Content = "nie", IsPublic = true, CreationDate = DateTime.Parse("2017-11-17") },
                };
                foreach (Comment c in comments)
                {
                    context.Comment.Add(c);
                }
                context.SaveChanges();*/
            }
        }
    }
}