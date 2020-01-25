using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Guest
    {
        private List<Article> _articles;

        public string Name { get; private set; }

        public double Bill
        {
            get
            {
                double result = 0;
                foreach (Article article in _articles)
                {
                    result += article.Price;
                }
                return result;
            }
        }

        public Guest(string name)
        {
            Name = name;
            _articles = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _articles.Add(article);
        }
    }
}
