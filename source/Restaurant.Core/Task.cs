using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Task : IComparable<Task>
    {
        public DateTime Delay { get; private set; }
        public string Customer { get; private set; }
        public OrderType MyOrderType { get; private set; }
        public string MyArticle { get; private set; }

        public Task(DateTime delay, string customer, OrderType orderType, string article)
        {
            Delay = delay;
            Customer = customer;
            MyOrderType = orderType;
            MyArticle = article;
        }

        public int CompareTo(Task other)
        {
            return Delay.CompareTo(other.Delay);
        }
    }
}
