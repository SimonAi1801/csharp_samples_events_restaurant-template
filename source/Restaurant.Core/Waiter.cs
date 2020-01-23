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
        private event EventHandler<Order> _logTask;
        private Dictionary<string, Article> _articles;
        private Queue<Order> _orders;
        private int _delay;
        private string _pathTask = MyFile.GetFullNameInApplicationTree("Tasks.csv");
        private string _pathArticle = MyFile.GetFullNameInApplicationTree("Articles.csv");
        private int minutesToBuild;
        private Order _currentOrder;
        private Article _currentArticle;

        public bool IsBusy
        {
            get
            {
                return _currentOrder != null;
            }
        }

        public Waiter(EventHandler<Order> onReadyTask)
        {
            _logTask += onReadyTask;
            _articles = new Dictionary<string, Article>();
            _orders = new Queue<Order>();
            string[] tasksLines = File.ReadAllLines(_pathTask);
            string[] articlesLines = File.ReadAllLines(_pathArticle);
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;

            InitArticles(articlesLines);
            InitTasks(tasksLines);
        }

        private void InitArticles(string[] articlesLines)
        {
            for (int i = 1; i < articlesLines.Length; i++)
            {
                string[] parts = articlesLines[i].Split(';');
                string articleName = parts[0];
                Article article = new Article(parts[0], Convert.ToDouble(parts[1]), Convert.ToInt32(parts[2]));
                _articles.Add(articleName, article);
            }
        }

        private void InitTasks(string[] tasksLines)
        {
            for (int i = 1; i < tasksLines.Length; i++)
            {
                string[] parts = tasksLines[i].Split(';');
                Order order = new Order(Convert.ToInt32(parts[0]), parts[1], parts[2], parts[3]);
                _orders.Enqueue(order);
            }
        }

        protected void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            if (!IsBusy)
            {
                _currentOrder = _orders.Dequeue();
                _delay = _currentOrder.Delay;
            }
            _delay--;
            if (_delay == 0)
            {
                //_logTask?.Invoke(this, _orders);
            }
        }
    }

}
