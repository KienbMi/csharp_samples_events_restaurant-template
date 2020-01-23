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

        public string Name { get; set; }


        public Guest(string guestName)
        {

        }

        internal void AddArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
