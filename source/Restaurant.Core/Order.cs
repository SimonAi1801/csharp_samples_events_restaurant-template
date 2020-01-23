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
        public string Name { get; private set; }
        public string MyOrderType { get; private set; }
        public string MyArticle { get; private set; }

        public Order(int delay, string name, string orderType, string article)
        {
            Delay = delay;
            Name = name;
            MyOrderType = orderType;
            MyArticle = article;
        }
    }
}
