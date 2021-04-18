using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dio_Bank_MVC.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Dio_Bank_MVCUser class
    public class Dio_Bank_MVCUser : IdentityUser
    {
        public static explicit operator Dio_Bank_MVCUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}
