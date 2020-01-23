using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Task
    {
        private OrderType _orderType;
        private Guest _guest;
        private DateTime _taskTime;
        private Article _article;

        public Task(OrderType orderType, Guest guest, DateTime taskTime, Article article = null)
        {
            _orderType = orderType;
            _guest = guest;
            _taskTime = taskTime;
            _article = article;
        }

        public OrderType TaskType => _orderType;
        public Guest Guest => _guest;

        public Article Article => _article;

    }
}
