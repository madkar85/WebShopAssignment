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
            //Seeder.SeedBookTitles();
            

            
            var id = webShop.Login("Admin","Saga");
            Console.WriteLine(id);
            id = webShop.Login("Administrator", "CodicRulez");
            Console.WriteLine(id);

            webShop.Logout(1);


            var cat = webShop.GetCategories();

            foreach(var book in cat)
            {
                Console.WriteLine("Kategorier som finns: " + book.Name);
            }
            //kolla vilka böcker som finns i en viss kategori, baserat på id
            var category = webShop.GetCategory(1);
            foreach(var b in category)
            {
                Console.WriteLine("Katergori med bok i lager: " + b.Title);
            }

            //kolla om lager är större än 0
            var available = webShop.GetAvailableBooks();
            foreach(var a in available)
            {
                Console.WriteLine("Bok i lager: " + a.Title);
            }

            //var newUser = webShop.Register("Madde", "Test", "Test");

            // webShop.BuyBook(1, 1);
            
            var bookInfo = webShop.GetBook(2);
            foreach(var info in bookInfo)
            {
            Console.WriteLine($"Information: {info}");
            }

            var books = webShop.GetBooks("The");
            foreach (var b in books)
            {
                Console.WriteLine($"Titlar som matchar: {b.Title}");
            }

            var author = webShop.GetAuthors("king");
            foreach(var a in author)
            {
                Console.WriteLine($"författare av: {a.Title}");
            }

           // var newBook = webShop.AddBook(1, "Last Continent", "Terry Pratchett", 170, 5);

            var users = webShop.ListUser(1);
            foreach (var u in users)
            {
                Console.WriteLine($"Användare: {u.Name}");
            }

            users = webShop.FindUser(1, "Cust");
            foreach (var u in users)
            {
                Console.WriteLine(u.Name);
            }
            
        }
    }
}
