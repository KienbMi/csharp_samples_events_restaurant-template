using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Restaurant.Core
{
    public class Waiter
    {

        private List<Task> _taskList;
        private Dictionary<string, Article> _articleList;
        private Dictionary<string, Guest> _guestList;
        private event EventHandler<string> TaskDone;


        public Waiter(EventHandler<string> OnTaskReady)
        {
            _articleList = new Dictionary<string, Article>();
            _taskList = new List<Task>();
            _guestList = new Dictionary<string, Guest>();
            ReadArticleFromFile();
            ReadTasksFromFile();
            FastClock.Instance.OneMinuteIsOver += OnOneMinuteIsOver;
            TaskDone += OnTaskReady;
        }


        public void ReadArticleFromFile()
        {
            string fileName = "Articles.csv";
            string fullFileName = Utils.MyFile.GetFullNameInApplicationTree(fileName);

            if (File.Exists(fullFileName) == false)
            {
                throw new InvalidOperationException($"File {fileName} does not exist");
            }

            string[] lines = File.ReadAllLines(fullFileName, Encoding.UTF8);

            if (lines == null)
            {
                throw new InvalidOperationException($"File {fileName} is empty");
            }

            bool ignoreFirstLine = true;

            foreach (string line in lines)
            {

                if (ignoreFirstLine == false)
                {
                    //Article; Price; TimeToBuild
                    //Cola; 2,2; 2
                    string[] data = line.Split(';');

                    string articleName;
                    double price;
                    int timeToBuild;

                    if (data != null &&
                        data.Length == 3 &&
                        double.TryParse(data[1], out price) &&
                        int.TryParse(data[2], out timeToBuild))
                    {
                        articleName = data[0];

                        if (_articleList.ContainsKey(articleName) == false)
                        {
                            Article article = new Article(articleName,
                                price,
                                timeToBuild);

                            _articleList.Add(articleName, article);
                        }
                    }
                }
                ignoreFirstLine = false;
            }
        }



        public void ReadTasksFromFile()
        {
            string fileName = "Tasks.csv";
            string fullFileName = Utils.MyFile.GetFullNameInApplicationTree(fileName);

            if (File.Exists(fullFileName) == false)
            {
                throw new InvalidOperationException($"File {fileName} does not exist");
            }

            string[] lines = File.ReadAllLines(fullFileName, Encoding.UTF8);

            if (lines == null)
            {
                throw new InvalidOperationException($"File {fileName} is empty");
            }

            bool ignoreFirstLine = true;

            foreach (string line in lines)
            {

                if (ignoreFirstLine == false)
                {
                    //Delay; Name; OrderType; Article
                    //5; Franz; Order; Bier
                    string[] data = line.Split(';');

                    int delay;
                    string guestName;
                    OrderType orderType;
                    string articleName;

                    if (data != null &&
                        data.Length == 4 &&
                        int.TryParse(data[0], out delay) &&
                        Enum.TryParse(data[2], out orderType))
                    {
                        guestName = data[1];
                        articleName = data[3];

                        Guest guest;
                        if (_guestList.TryGetValue(guestName, out guest) == false)
                        {
                            guest = new Guest(guestName);
                            _guestList.Add(guestName, guest);
                        }

                        if (orderType == OrderType.Order)
                        {
                            Article article;

                            if (_articleList.TryGetValue(articleName, out article))
                            {
                                DateTime taskTime = FastClock.Instance.Time.AddMinutes(delay);
                                guest.AddArticle(article);
                                Task taskOrder = new Task(orderType, guest, taskTime, article);
                                _taskList.Add(taskOrder);

                                taskTime = taskTime.AddMinutes(article.TimeToBuild);
                                Task taskReady = new Task(OrderType.Ready, guest, taskTime, article);
                                _taskList.Add(taskReady);
                            }

                        }
                        else if (orderType == OrderType.ToPay)
                        {
                            DateTime taskTime = FastClock.Instance.Time.AddMinutes(delay);
                            Task task = new Task(orderType, guest, taskTime);
                            _taskList.Add(task);
                        }
                    }
                }
                ignoreFirstLine = false;
            }
            _taskList.Sort();
        }


        private void OnOneMinuteIsOver(object source, DateTime time)
        {
            while (_taskList.Count > 0 && 
                    _taskList[0].TaskTime == FastClock.Instance.Time)
            {
                Task task = _taskList[0];
                _taskList.RemoveAt(0);

                string text = "Ausgabe";

                if (task.TaskType == OrderType.Order)
                {
                    
                    text = $"{task.Article.Name} für {task.Guest.Name} ist bestellt";
                }
                else if (task.TaskType == OrderType.Ready)
                {
                    text = $"{task.Article.Name} für {task.Guest.Name} wird serviert";
                }
                else if (task.TaskType == OrderType.ToPay)
                {
                    text = $"{task.Guest.Name} bezahlt {task.Guest.GetBill() :f2}";
                }

                OnTaskDone(text);
            }
        }

        private void OnTaskDone(string text)
        {
            TaskDone?.Invoke(this, text);
        }
    }
}
