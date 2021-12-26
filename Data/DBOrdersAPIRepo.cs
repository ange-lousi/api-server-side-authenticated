using System;
using System.Collections.Generic;
using alou149.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace alou149.Data
{
    public class DBOrdersAPIRepo : IOrdersAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;
        public DBOrdersAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Orders AddOrder(Orders order)
        {
            EntityEntry<Orders> e = _dbContext.Order.Add(order);
            Orders c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            IEnumerable<Orders> oList = _dbContext.Order.ToList<Orders>();
            return oList;
        }

        public Orders GetOrderByOrderID(int id)
        {
            Orders a = _dbContext.Order.FirstOrDefault(o => o.ProductID == id);
            return a;
        }

        public Orders GetOrderByName(string n)
        {
            Orders name = _dbContext.Order.FirstOrDefault(e => e.UserName == n);
            return name;
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
