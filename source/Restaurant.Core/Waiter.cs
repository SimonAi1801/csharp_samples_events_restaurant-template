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
        public event EventHandler<string> _taskReady;
        private Dictionary<string, Article> _articles;
        private Dictionary<string, Guest> _guestList;
        private List<Task> _tasks;

        public Waiter()
        {
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            _articles = new Dictionary<string, Article>();
            _guestList = new Dictionary<string, Guest>();
            _tasks = new List<Task>();

            InitArticles();
            CreatTasks();
        }

        private void InitArticles()
        {
            string articlePath = MyFile.GetFullNameInApplicationTree("Articles.csv");
            string[] articleLines = File.ReadAllLines(articlePath, UTF8Encoding.Default);

            for (int i = 1; i < articleLines.Length; i++)
            {
                string[] parts = articleLines[i].Split(';');
                string articleName = parts[0];
                Article article = new Article(parts[0], Convert.ToDouble(parts[1]), Convert.ToInt32(parts[2]));
                _articles.Add(articleName, article);
            }
        }

        private void CreatTasks()
        {
            string taskPath = MyFile.GetFullNameInApplicationTree("Tasks.csv");
            string[] taskLines = File.ReadAllLines(taskPath, UTF8Encoding.Default);
            OrderType orderType;
            Article article;

            for (int i = 1; i < taskLines.Length; i++)
            {
                string[] parts = taskLines[i].Split(';');
                string guestName = parts[1];
                Guest guest = new Guest(guestName);

                if (parts != null && Enum.TryParse(parts[2], out orderType))
                {
                    if (!_guestList.ContainsKey(guestName))
                    {
                        _guestList.Add(guestName, guest);
                    }

                    DateTime taskTime = FastClock.Instance.Time.AddMinutes(Convert.ToInt32(parts[0]));
                    Task task = new Task(taskTime, parts[1], orderType, parts[3]);
                    _tasks.Add(task);

                    if (orderType == OrderType.Order && _articles.TryGetValue(parts[3], out article))
                    {
                        taskTime = taskTime.AddMinutes(article.TimeToBuild);
                        Task taskReady = new Task(taskTime, task.Customer, OrderType.Ready, task.MyArticle);
                        _tasks.Add(taskReady);
                    }
                }
            }
            _tasks.Sort();
        }

        protected virtual void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            string text = String.Empty;
            Guest guest;
            Article article;

            while (_tasks.Count > 0 && _tasks[0].Delay.Equals(FastClock.Instance.Time))
            {
                if (_guestList.TryGetValue(_tasks[0].Customer, out guest))
                {
                    if (_tasks[0].MyOrderType == OrderType.Order)
                    {
                        text = $"{_tasks[0].MyArticle} für {_tasks[0].Customer} ist bestellt!";
                    }

                    else if (_tasks[0].MyOrderType == OrderType.Ready
                                && _articles.TryGetValue(_tasks[0].MyArticle, out article))
                    {
                        text = $"{_tasks[0].MyArticle} für {_tasks[0].Customer} wird serviert!";
                        guest.AddArticle(article);
                    }

                    else if (_tasks[0].MyOrderType == OrderType.ToPay)
                    {
                        text = $"{_tasks[0].Customer} bezahlt {guest.Bill:F2} EUR!";
                    }

                    OnTaskFinish(text);
                    _tasks.RemoveAt(0);
                }
            }
        }
        protected virtual void OnTaskFinish(string text)
        {
            _taskReady?.Invoke(this, text);
        }
    }

}
