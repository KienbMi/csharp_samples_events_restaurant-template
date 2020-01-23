using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Article
    {
        private string _name;
        private double _price;
        private int _timeToBuild;


        public string Name => _name;
        public double Price => _price;
        public int TimeToBuild => _timeToBuild;

        public Article(string name, double price, int timeToBuild)
        {
            _name = name;
            _price = price;
            _timeToBuild = timeToBuild;
        }
    }
}
