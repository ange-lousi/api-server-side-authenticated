using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alou149.Models;

namespace alou149.Data
{
    public interface IProductAPIRepo
    {
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetProductByName(string n);
        void SaveChanges();

    }
}
