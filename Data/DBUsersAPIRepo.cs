using System;
using System.Collections.Generic;
using alou149.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace alou149.Data
{
    public class DBUsersAPIRepo : IUsersAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBUsersAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users AddUser(Users user)
        {
            EntityEntry<Users> e = _dbContext.User.Add(user);
            Users c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            IEnumerable<Users> u = _dbContext.User.ToList<Users>();
            return u;
        }

        public Users GetUserByName(string n)
        {
            Users products = _dbContext.User.FirstOrDefault(e => e.UserName == n);
            return products;
        }

        public bool ValidLogin(string userName, string password)
        {
            Users c = _dbContext.User.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            if (c == null)
                return false;
            else
                return true;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
