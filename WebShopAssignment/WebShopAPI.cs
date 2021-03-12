using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebShopAssignment.Database;
using WebShopAssignment.Models;

namespace WebShopAssignment
{
    public class WebShopAPI
    {
        private static MyDatabase db = new MyDatabase();
        public int Login (string userName, string password)
        {       // skriv om till nya linq-syntaxen
                var user = db.Users.FirstOrDefault(u => u.Name == userName && u.Password == password && u.IsActive);

                if (user != null)
                {
                    user.LastLogin = DateTime.Now;
                    user.SessionTimer = DateTime.Now;
                    db.Users.Update(user);
                    db.SaveChanges();
                    return user.Id;
                }          
            //TODO: skapa en metod som kollar om användaren finns i databasen
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
            //TODO: skapa en metod som kollar om användaren varit inaktiv i 15 mi8n
        }

        public List<BookCategory> GetCategories()
        {
            //TODO: skapa en metod som visar alla bokkategorier
            //return db.BookCategories;
        }
        /*
        public static List<BookCategory> GetCategories(string keyword)
        {
            
            //TODO: skapa en metod som visar alla kategorier som matchar med keyword/sökord
            return 
        }
        */
        public static void GetAvailableBooks()
        {
            //TODO: skapa en metod som visar böcker med fler än 0 i lager
        }

        public static void GetBook()
        {
            //TODO: skapa metod som
        }
    }
}
