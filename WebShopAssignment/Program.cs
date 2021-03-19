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
            id = webShop.Login("TestCustomer", "CodicRulez");

            //webShop.Logout(1);
            var check = webShop.Ping(2);
            Console.WriteLine(check);
            webShop.Ping(1);

            /*
            var cats = webShop.GetCategories();
            foreach(var cat in cats)
            {
                Console.WriteLine($"Kategorier som finns: ID {cat.Id} Namn: { cat.Name }");
            }
            */
            //kolla vilka böcker som finns i en viss kategori, baserat på id
            /*
            var category = webShop.GetCategory(1);
            foreach(var b in category)
            {
                Console.WriteLine($"Katergori med bok i lager: {b.Title}");
            }
            */
            //kolla om lager är större än 0
            /*
            var available = webShop.GetAvailableBooks();
            foreach(var a in available)
            {
                Console.WriteLine($"Bok i lager: {a.Title}");
            }
            */

            //var newUser = webShop.Register("Skrutt", "Test", "Test");

            // webShop.BuyBook(1, 15);
            /*
            var bookInfo = webShop.GetBook(2);
            foreach(var info in bookInfo)
            {
                UserHelper.PrintAllInformation(info);
            }
            */
            /*
            var books = webShop.GetBooks("The");
            foreach (var b in books)
            {
                Console.WriteLine($"Titlar som matchar: {b.Title}");
            }
            */
            /*
            var author = webShop.GetAuthors("king");
            foreach(var a in author)
            {
                Console.WriteLine($"författare av: {a.Title}");
            }
            */
            webShop.BuyBook(2, 4);
            //var newBook = webShop.AddBook(1, "Horton Hears A Who", "Dr Seuss", 100, 2);
            /*
            var users = webShop.ListUser(1);
            foreach (var u in users)
            {
                Console.WriteLine($"Användare: {u.Name}");
            }
            */
            /*
            users = webShop.FindUser(1, "Cust");
            foreach (var u in users)
            {
                Console.WriteLine(u.Name);
            }
            */
            //webShop.SetAmount(1, 3, 8);
            //webShop.UpdateBook(1, 3, "Doctor Sleep", "Stephen King", 150);
            //webShop.DeleteBook(1, 16, 10);           
            //webShop.AddCategory(1, "Kids");
            //webShop.AddBookToCategory(1, 17, 5);
            //webShop.UpdateCategory(1, 7, "ANIMALIA");
            //webShop.DeleteCategory(1,6);
            //webShop.AddUser(1, "maja", "Codic2021");

        }
    }
}
