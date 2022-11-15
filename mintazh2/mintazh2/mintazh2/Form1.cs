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
            AutoScroll=true;
            InitializeComponent();
            GetProducts();
            Productlist();
            
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

        //Készíts visszatérési érték nélküli függvényt, amely a mellékelt Menu.xml-ből tölti be a termékeket az előző lépésben létrehozott függvény segítségével.
        private void GetProducts()
        {
            //Hívd meg a függvényben a 2.lépésben létrehozott betöltő metódust „Menu.xml” paraméterrel
            var xml = new XmlDocument();
            xml.LoadXml(GetXml("Menu.xml"));

            /* Egy megfelelő ciklus segítségével haladj végig az XML bejegyzésein.
            a. A ciklusban mentsd ki egy-egy változóba a „name”, a „description”, a „calories” és a „type” értékeket. Ehhez használhatod az XmlElement osztály SelectSingleNode függvényét, vagy belső ciklus segítségével vizsgáld meg a gyermekelemeket és vedd a megfelelő nevűek értékét.
            b. A „type” függvényében („food” vagy „drink” lehet) hozd létre a megfelelő osztálypéldányt és töltsd fel a lehetséges értékeit.
            c. Add a példányt a _products listához*/
            foreach (XmlElement element in xml.DocumentElement)
            {
                var name = element.SelectSingleNode("name").InnerText;
                var calories = element.SelectSingleNode("calories").InnerText;
                var description = element.SelectSingleNode("description").InnerText;
                var type = element.SelectSingleNode("type").InnerText;

                if (type == "food")
                {
                    var f = new Food()
                    {
                        Title = name,
                        Calories = int.Parse(calories),
                        Description = description
                    };
                    _products.Add(f);
                }
                else
                {
                    var d = new Drink()
                    {
                        Title = name,
                        Calories = int.Parse(calories)
                    };
                    _products.Add(d);
                }
            }
        }

        private void Productlist()
        {
            var topPosition = 0;
            var sortedProducts = from p in _products
                                 orderby p.Title
                                 select p;
            foreach (var item in sortedProducts)
            {
                item.Left = 0;
                item.Top = topPosition;
                Controls.Add(item);
                topPosition += item.Height;
            }
        }

    }
}
