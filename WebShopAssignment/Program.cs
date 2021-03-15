using System;
using WebShopAssignment.Helpers;
using WebShopAssignment.Models;

namespace WebShopAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var webShop = new WebShopAPI();
            
           // Seeder.SeedUsers();
           // Seeder.SeedCategories();
            //TODO: Lägg till boktitlar och resterande info i databasen, tabell books
            

            
            var id = webShop.Login("Admin","Saga");
            Console.WriteLine(id);
            id = webShop.Login("Administrator", "CodicRulez");
            Console.WriteLine(id);

            var cat = webShop.GetCategories();

            foreach(var book in cat)
            {
                Console.WriteLine(book.Name);
            }
        }
    }
}
