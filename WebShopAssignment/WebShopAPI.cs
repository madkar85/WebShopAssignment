using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShopAssignment.Database;
using WebShopAssignment.Models;

namespace WebShopAssignment
{
    public class WebShopAPI
    {
        private static MyDatabase db = new MyDatabase();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login (string userName, string password)
        {       
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

        public void Logout (int id)
        {
            //skriv om till nya linq-syntaxen
            var user = db.Users.FirstOrDefault(u => u.Id == id && u.SessionTimer > DateTime.Now.AddMinutes(-15));

            if (user != null)
            {
                user.SessionTimer = DateTime.MinValue;
                db.Users.Update(user);
                db.SaveChanges();               
            }
            //TODO: skapa en metod som kollar om användaren varit inaktiv i 15 min
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
        public List<Book> GetCategory (int id)
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

        public static void GetBook(int bookId)
        {
            //TODO: skapa metod som skriver ut informationen om en specifik bok, sök efter book ID
            //returnera informationen om boken
        }
        /// <summary>
        /// Visar alla böcker som har inputvärdet i titeln
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Book> GetBooks(string keyword)
        {
            var books = db.Books.Where(b => b.Title == keyword);
            return books.ToList();
        }
        /// <summary>
        /// Visar alla böcker som har en författare som matchar input
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Book> GetAuthors(string keyword)
        {
            var author = db.Books.Where(a => a.Author == keyword);
            return author.ToList();
        }

        public bool BuyBook(int userId, int bookId)
        {
            //TODO: metod som kollar om boken finns i lager, kollar att SessionTimer inte är mer än 15 min
            //kopierar bokdata till soldBooks, fyller på SoldBooks med datum och userid
            //returnera true om köpet är ok
            var user = db.Users.FirstOrDefault(u => u.Id == userId && u.SessionTimer > DateTime.Now.AddMinutes(-15));

            if(user != null)
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

        public void Ping (int userId)
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
        public bool Register (string name, string password, string verifyPassword)
        {
            //fixa default IsActive == true
            var user = db.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
            if (user!= null)
            {
                return false;
            }
            if(password == verifyPassword)
            {
                db.Users.Add( new Models.User { Name = name, Password = password});
                db.SaveChanges();
            }
            return true;
        }

        public bool AddBook(int adminId, int id, string title, string author, int price, int amount)
        {
            //TODO: metod som kollar om användaren är admin, sen lägger till antal böcker(antingen plussar på befintligt antal eller lägger antal)
            // returnera true om det gick, false annars
            var admin = db.Users.FirstOrDefault(a => a.IsAdmin == true);
            if (admin==null)
            {
                return false;
            }
            else
            {

            }
            return true;
        }

        public void SetAmount(int adminId, int bookId)
        {
            //TODO: metod som returnerar antal böcker i lager, på specific bok?
            //returnerar "sets amount of available books"
        }

        /*
        public List<User> ListUser(int adminId)
        {
            //TODO: kolla om användaren är admin. returnera sedan alla användare. endast namn
            //returnerar en lista med användare
            var users = db.Users.Where(u => u.Name);
            return 
        }*/

        /*
        public List<User> FindUser(int adminId, string keyword)
        {
            //TODO: kolla om användaren är admin. skriv ut en lista med användare som matchar med keyword
            // returnerar en lista med användare som matchar keyword
        }*/

        public void UpdateBook(int adminId, int id, string title, string author, int price)
        {
            //TODO: kolla om användaren är admin. Updatera bokens uppgifter. returnera true om det går, false om det inte går
        }

        public void DeleteBook(int adminId, int bookId)
        {
            //TODO: kolla om användaren är admin. minska antal böcker. om det är noll, ta bort boken
            // returnera true om det går, false om det inte går
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
