using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Order
    {
        public int Delay { get; private set; }
        public string Customer { get; private set; }
        public string MyOrderType { get; private set; }
        public string MyArticle { get; private set; }
        public double Price { get; set; }


        public Order(int delay, string customer, string orderType, string article)
        {
            Delay = delay;
            Customer = customer;
            MyOrderType = orderType;
            MyArticle = article;
        }
    }
}
