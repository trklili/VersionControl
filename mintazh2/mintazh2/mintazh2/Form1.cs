using mintazh2.Abstarctions;
using mintazh2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace mintazh2
{
    public partial class Form1 : Form
    {
       List<Product> _products = new List<Product>();
        public Form1()
        {
            InitializeComponent();
            
        }

        //Készíts egy karakterláncot visszaadó, egy bemeneti karakterlánc paraméterrel bíró függvényt, amely a kapott paraméternek megfelelő nevű XML állományt olvassa be teljes egészében és adja ezt vissza.
        private string GetXml(string fileName)
        {
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                var xml = sr.ReadToEnd(); // A ReadToEnd-et el lehet mondani nekik, lehet, hogy nem tudják
                return xml;
            }
        }

        private void GetProducts()
        {
            var xml = new XmlDocument();
            xml.LoadXml(GetXml("Menu.xml"));

            foreach (XmlElement element in xml.DocumentElement)
            {
                var name = element.SelectSingleNode("name").InnerText;
                var calories = element.SelectSingleNode("calories").InnerText;
                var description = element.SelectSingleNode("description").InnerText;
                var type = element.SelectSingleNode("type").InnerText;

                if (type == "food")
                {
                    var p = new Food()
                    {
                        Title = name,
                        Calories = int.Parse(calories),
                        Description = description
                    };
                    _products.Add(p);
                }
                else
                {
                    var p = new Drink()
                    {
                        Title = name,
                        Calories = int.Parse(calories)
                    };
                    _products.Add(p);
                }
            }
        }

    }
}
