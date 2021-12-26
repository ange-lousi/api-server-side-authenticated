using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alou149.Models;

namespace alou149.Data
{
    public interface IUsersAPIRepo
    {
        public IEnumerable<Users> GetAllUsers();
        Users GetUserByName(string n);
        public bool ValidLogin(string userName, string password);
        
        Users AddUser(Users user);
        
    }
}
