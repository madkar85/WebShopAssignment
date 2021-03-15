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

        public List<BookCategory> GetCategories()
        {
            
            return db.BookCategories.ToList();
        }

        public List<BookCategory> GetCategories(string keyword)
        {

            var search = db.BookCategories.Where(c => c.Name.Contains(keyword));
            return search.ToList();
            
            
        }

        public List<Book> GetCategory (int id)
        {
            //TODO: metod som skriver ut böckerna i en viss kategori
            return 
        }
        
        public List<Book> GetAvailableBooks()
        {
            //TODO: skapa en metod som visar böcker med fler än 0 i lager
            var available = db.Books.Where(c => c.Amount > 0);
            return available.ToList();

        }

        public static void GetBook(int bookId)
        {
            //TODO: skapa metod som skriver ut informationen om en specifik bok, sök efter book ID
        }

        public List<Book> GetBooks(string keyword)
        {
            var books = db.Books.Where(b => b.Title == keyword);
            return books.ToList();
        }

        public List<Book> GetAuthors(string keyword)
        {
            var author = db.Books.Where(a => a.Author == keyword);
            return author.ToList();
        }

        public void BuyBook(int userId, int bookId)
        {
            //TODO: metod som kollar om boken finns i lager, kollar att SessionTimer inte ör mer än 15 min
            //kopierar bokdata till soldBooks, fyller på SoldBooks med datum och userid

        }

        public void Ping (int userId)
        {
            //TODO: kollar att användaren är online (kollar anslutningen)
        }

        public void Register (string name, string password, string verifyPassword)
        {
            //TODO: skapar en kund. returnerar true om användaren kan skapas, returnerar false om redan finns eller password är fel
        }

        public bool AddBook(int adminId, int id, string title, string author, int price, int amount)
        {
            //TODO: metod som kollar om användaren är admin, sen lägger till antal böcker(antingen plussar på befintligt antal eller lägger antal)
            return true;
        }

        public void SetAmount(int adminId, int bookId)
        {
            //TODO: metod som returnerar antal böcker i lager, på specific bok?
        }

        
        public List<User> ListUser(int adminId)
        {
            //TODO: kolla om användaren är admin. returnera sedan alla användare. endast namn
            return
        }

        public List<User> FindUser(int adminId, string keyword)
        {
            //TODO: kolla om användaren är admin. skriv ut en lista med användare som matchar med keyword
        }

        public void UpdateBook(int adminId, int id, string title, string author, int price)
        {
            //TODO: kolla om användaren är admin. Updatera bokens uppgifter. true om det går, false om det inte går
        }

        public void DeleteBook(int adminId, int bookId)
        {
            //TODO: kolla om användaren är admin. minska antal böcker. om det är noll, ta bort boken
        }

        public void AddBookToCategory(int adminId, int bookId, int categoryId)
        {
            //TODO: kolla om användaren är admin. Lägg sedan till en kategori till boken
        }

        public void UpdateCategory(int adminId, int categoryId, string name)
        {
            //TODO:kolla om användaren är admin. Uppdatera sedan namnet på kategorin
        }

        public void DeleteCategory(int adminId, int categoryId)
        {
            //TODO: kolla om användaren är admin. Ta bort en kategri OM denna är tom, annars failar metoden.
        }

        public void AddUser(int adminId, string name, string password)
        {
            //TODO: kolla om användaren är admin. Lägg sedan till en användare, fails om användaren finns eller om lösenord är tomt.
        }
    }
}
