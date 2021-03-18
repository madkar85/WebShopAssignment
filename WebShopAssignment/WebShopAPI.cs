using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShopAssignment.Database;
using WebShopAssignment.Helpers;
using WebShopAssignment.Models;


namespace WebShopAssignment
{
    public class WebShopAPI
    {
        private static MyDatabase db = new MyDatabase();
        /// <summary>
        /// Loggar in användaren
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string userName, string password)
        {
            //Lägg till IsActive i syntaxen
            var user = db.Users.FirstOrDefault(u => u.Name == userName && u.Password == password);

            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                user.SessionTimer = DateTime.Now;
                db.Users.Update(user);
                db.SaveChanges();
                return user.Id;
            }

            return 0;
        }
        /// <summary>
        /// Loggar ut användaren och sätter timern till 0
        /// </summary>
        /// <param name="id"></param>
        public void Logout(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id && u.SessionTimer > DateTime.Now.AddMinutes(-15));

            if (user != null)
            {
                user.SessionTimer = DateTime.MinValue;
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Visar alla kategorier i databasen
        /// </summary>
        /// <returns></returns>
        public List<BookCategory> GetCategories()
        {
            return db.BookCategories.ToList();
        }
        /// <summary>
        /// Visar alla kategorier som innehåller keywordet
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<BookCategory> GetCategories(string keyword)
        {
            var search = db.BookCategories.Where(c => c.Name.Contains(keyword));
            return search.ToList();


        }
        /// <summary>
        /// Visar böckerna i en viss kategori, baserat på input
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Book> GetCategory(int id)
        {
            var cat = db.Books.Where(c => c.CategoryId == id);
            return cat.ToList();
        }
        /// <summary>
        /// Visar alla böcker som finns i lager, alla som har antal större än 0
        /// </summary>
        /// <returns></returns>
        public List<Book> GetAvailableBooks()
        {
            var available = db.Books.Where(c => c.Amount > 0);
            return available.ToList();
        }

        public List<Book> GetBook(int bookId)
        {
            //TODO: skapa metod som skriver ut informationen om en specifik bok, sök efter book ID
            //returnera informationen om boken. Fixa hur man skriver ut listan
            var book = db.Books.Where(b => b.Id == bookId);
            return book.ToList();
        }
        /// <summary>
        /// Visar alla böcker som har inputvärdet i titeln
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Book> GetBooks(string keyword)
        {
            var books = db.Books.Where(b => b.Title.Contains(keyword));

            return books.ToList();
        }
        /// <summary>
        /// Visar alla böcker som har en författare som matchar input
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Book> GetAuthors(string keyword)
        {
            var author = db.Books.Where(a => a.Author.Contains(keyword));
            return author.ToList();
        }

        public bool BuyBook(int userId, int bookId)
        {
            //TODO: metod som kollar om boken finns i lager, kollar att SessionTimer inte är mer än 15 min
            //kopierar bokdata till soldBooks, fyller på SoldBooks med datum och userid
            //returnera true om köpet är ok
            var user = db.Users.FirstOrDefault(u => u.Id == userId && u.SessionTimer > DateTime.Now.AddMinutes(-15));

            if (user != null)
            {
                using (var db = new MyDatabase())
                {
                    var org = db.Books.Find(bookId);
                    var copy = new SoldBook();

                    db.Entry(copy).CurrentValues.SetValues(org);
                    db.SoldBooks.Add(copy);
                    db.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public void Ping(int userId)
        {
            //TODO: kollar att användaren är online (kollar anslutningen)
            //returnera pong om användaren är online, annars string.empty
        }

        /// <summary>
        /// Registrera en ny användare
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="verifyPassword"></param>
        /// <returns></returns>
        public bool Register(string name, string password, string verifyPassword)
        {
            //fixa default IsActive == true
            var user = db.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            if (user != null)
            {
                return false;
            }
            if (password == verifyPassword)
            {
                db.Users.Add(new Models.User { Name = name, Password = password });
                db.SaveChanges();
            }
            return true;
        }
        /// <summary>
        /// Kollar om användaren är admin, lägger sedan till en bok i databasen.
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool AddBook(int adminId, string title, string author, int price, int amount)
        {
            var user = db.Users.FirstOrDefault(a => a.Id == adminId);

            if (UserHelper.IsUserAdmin(user) == true)
            {
                db.Books.Add(new Book { Title = title, Author = author, Price = price, Amount = amount });
                db.SaveChanges();

                return true;
            }
            return false;

        }

        public void SetAmount(int adminId, int bookId, int amount)
        {
            //TODO: metod som returnerar antal böcker i lager, på specific bok?
            //returnerar "sets amount of available books"
            var user = db.Users.FirstOrDefault(a => a.Id == adminId);
            if (UserHelper.IsUserAdmin(user) == true)
            { 
                var book = db.Books.Where(b => b.Id == bookId).ToList().ForEach(a => a.Amount = { amount});
                
                
            }
            
        }

        /// <summary>
        /// Kollar om admin och skriver sedan ut en lista på användarnas namn
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public List<User> ListUser(int adminId)
        {

            var user = db.Users.FirstOrDefault(a => a.Id == adminId);
            if (UserHelper.IsUserAdmin(user) == true)
            {
                return db.Users.ToList();
            }
            return null;
        }

        /// <summary>
        /// kollar om admin och returnerar en lista med användare som matchar input
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<User> FindUser(int adminId, string keyword)
        {
            //TODO: kolla om användaren är admin. skriv ut en lista med användare som matchar med keyword
            // returnerar en lista med användare som matchar keyword
            var user = db.Users.FirstOrDefault(a => a.Id == adminId);
            if (UserHelper.IsUserAdmin(user) == true)
            {
                var foundUser = db.Users.Where(u => u.Name.Contains(keyword));
                return foundUser.ToList();
            }
            return null;
        }

        public void UpdateBook(int adminId, int id, string title, string author, int price)
        {
            //TODO: kolla om användaren är admin. Updatera bokens uppgifter. returnera true om det går, false om det inte går
        }

        public void DeleteBook(int adminId, int bookId)
        {
            //TODO: kolla om användaren är admin. minska antal böcker. om det är noll, ta bort boken
            // returnera true om det går, false om det inte går
        }

        public void AddCategory (int adminId, string name)
        {

        }

        public void AddBookToCategory(int adminId, int bookId, int categoryId)
        {
            //TODO: kolla om användaren är admin. Lägg sedan till en kategori till boken
            //returnera true om det går, false om det inte går
        }

        public void UpdateCategory(int adminId, int categoryId, string name)
        {
            //TODO:kolla om användaren är admin. Uppdatera sedan namnet på kategorin
            //returnera true om det går, false om det inte går
        }

        public void DeleteCategory(int adminId, int categoryId)
        {
            //TODO: kolla om användaren är admin. Ta bort en kategri OM denna är tom, annars failar metoden.
            //returnera true om det går, false om det inte går
        }

        public void AddUser(int adminId, string name, string password)
        {
            //TODO: kolla om användaren är admin. Lägg sedan till en användare, fails om användaren finns eller om lösenord är tomt.
            //returnera true om det går, false om det inte går
        }
    }
}
