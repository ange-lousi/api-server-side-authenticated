using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alou149.Models;


namespace alou149.Data
{
    public interface IOrdersAPIRepo
    {
        public IEnumerable<Orders> GetAllOrders();
        Orders GetOrderByName(string n);
        public bool ValidLogin(string userName, string password);
        public Orders GetOrderByOrderID(int e);
        Orders AddOrder(Orders order);
    }
}
