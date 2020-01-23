using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Guest
    {
        private List<Article> _orderList;
        private string _guestName;

        public string Name => _guestName;


        public Guest(string guestName)
        {
            _guestName = guestName;
            _orderList = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _orderList.Add(article);
        }

        public double GetBill()
        {
            double result = 0;

            foreach (Article article in _orderList)
            {
                result += article.Price;
            }
            return result;
        }
    }
}
