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

        public Task(OrderType orderType, Guest guest, DateTime taskTime)
        {
            _orderType = orderType;
            _guest = guest;
            _taskTime = taskTime;
        }

        public OrderType TaskType => _orderType;

    }
}
