using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Article
    {
        public string MyArticle { get; private set; }
        public double Price { get; private set; }
        public int TimeToBuild { get; private set; }

        public Article(string myArticle, double price, int timeToBuild)
        {
            MyArticle = myArticle;
            Price = price;
            TimeToBuild = timeToBuild;
        }
    }
}
