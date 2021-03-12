using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopAssignment.Database;

namespace WebShopAssignment.Helpers
{
    public class Seeder
    {
        //Fixa metoder som lägger till exempeldatan i databasen
        public static void Seed()
        {
            using (var db = new MyDatabase())
            {
                if (db.BookCategories.Count() == 0)
                {
                    db.BookCategories.Add(new Models.BookCategory { Name = "Horror" });
                    db.BookCategories.Add(new Models.BookCategory { Name = "Humor" });
                    db.BookCategories.Add(new Models.BookCategory { Name = "Science Fiction" });
                    db.BookCategories.Add(new Models.BookCategory { Name = "Romance" });
                }
            }
        }
    }
}
