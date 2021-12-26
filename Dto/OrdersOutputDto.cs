using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alou149.Dto
{
    public class OrdersOutputDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        
        public int ProductID { get; set; }
        
        public int Quantity { get; set; }
    }
}
