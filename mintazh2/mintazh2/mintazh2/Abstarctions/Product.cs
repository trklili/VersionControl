using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace mintazh2.Abstarctions
{
    public abstract class Product: Button
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set 
            {
                _title = value;
                Text = Title;
            }
        }

        private string _calories;
        public string Calories
        {
            get { return _calories; }
            set 
            { 
                _calories = value;
                Display();
            }
        }

        //visszatérés nélküli absztrakt fgv
        protected abstract void Display();

        //konstruktor
        public Product()
        {
            Width = 150;
            Height = 50;
        }
    }
}
