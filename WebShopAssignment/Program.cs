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
            //Seeder.SeedBookTitles();
            

            
            var id = webShop.Login("Admin","Saga");
            Console.WriteLine(id);
            id = webShop.Login("Administrator", "CodicRulez");
            Console.WriteLine(id);

            var cat = webShop.GetCategories();

            foreach(var book in cat)
            {
                Console.WriteLine(book.Name);
            }
            //kolla vilka böcker som finns i en viss kategori, baserat på id
            var category = webShop.GetCategory(1);
            foreach(var b in category)
            {
                Console.WriteLine(b.Title);
            }

            //kolla om lager är större än 0
            var available = webShop.GetAvailableBooks();
            foreach(var a in available)
            {
                Console.WriteLine(a.Title);
            }

            var newUser = webShop.Register("Madde", "Test", "Test");

            webShop.BuyBook(1, 1);
        }
    }
}
