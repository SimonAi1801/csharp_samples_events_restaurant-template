using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using System.IO;

namespace Restaurant.Core
{
    public class Waiter
    {
        public event EventHandler<Order> _logTask;
        private List<Article> _articles;
        private List<Order> _orders;
        private int _delay;
        private string _pathTask = MyFile.GetFullNameInApplicationTree("Tasks.csv");
        private string _pathArticle = MyFile.GetFullNameInApplicationTree("Articles.csv");
        private int minutesToBuild;

        public Waiter(EventHandler<Order> onReadyTask)
        {
            _logTask += onReadyTask;
            _articles = new List<Article>();
            _orders = new List<Order>();
            string[] tasksLines = File.ReadAllLines(_pathTask);
            string[] articlesLines = File.ReadAllLines(_pathArticle);
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;

            InitArticles(articlesLines);
            InitTasks(tasksLines);
        }

        private void InitArticles(string[] articlesLines)
        {
            for (int i = articlesLines.Length - 1; i >= 1; i--)
            {
                string[] parts = articlesLines[i].Split(';');
                Article article = new Article(parts[0], Convert.ToDouble(parts[1]), Convert.ToInt32(parts[2]));
                _articles.Add(article);
            }
        }

        private void InitTasks(string[] tasksLines)
        {
            for (int i = tasksLines.Length - 1; i >= 1; i--)
            {
                string[] parts = tasksLines[i].Split(';');
                Order order = new Order(Convert.ToInt32(parts[0]), parts[1], parts[2], parts[3]);
                _orders.Add(order);
            }
        }

        protected void Instance_OneMinuteIsOver(object sender, DateTime e)
        {

            if (true)
            {
                _logTask?.Invoke(this, _orders[0]);
            }
        }
    }

}
