using System;
using WebShopAssignment.Helpers;
using WebShopAssignment.Models;

namespace WebShopAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Seeder.Seed();
            //TODO: Lägg till användare i databasen.
            /*new Users
            {
                Name = "Administrator",
                Password = "CodicRulez",
                IsAdmin = true,
            };
            //TODO: Lägg till bokkategorierna i databasen, tabell BookCategory
            //TODO: Lägg till boktitlar och resterande info i databasen, tabell books

            var webShop = new WebShopAPI();
            */
            //var id = webShop.Login();
            //Console.WriteLine(id);
        }
    }
}
