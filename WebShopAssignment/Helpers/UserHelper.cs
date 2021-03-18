using System;
using System.Collections.Generic;
using System.Text;
using WebShopAssignment.Models;

namespace WebShopAssignment.Helpers
{
    public class UserHelper
    {
        public static bool IsUserAdmin(User user)
        {
            //TODO: fixa en metod som kollar om en användare är admin.
            if (user == null || !user.IsAdmin)
            {
                return false;
            }
            return true;

        }


    }
}
