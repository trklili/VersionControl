using Olympics.medálok;
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

namespace Olympics
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        public Form1()
        {
            InitializeComponent();
            LoadData("Summer_olypics_Medals.csv");
        }

        private void LoadData(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                sr.ReadLine();
                // fejléc beolvas
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');

                    var eredmeny = new OlympicResult()
                    {
                        Year = int.Parse(sor[0]),
                        Country = sor[3],
                        Medals = new int[]
                        {
                            int.Parse(sor[5]),
                            int.Parse(sor[6]),
                            int.Parse(sor[7]),
                        }
                    };
                    results.Add(eredmeny);

                }
            }
        }

        private void Evvalaszto()
        {
            var years = (from r in results
                         orderby r.Year
                         select r.Year).Distinct();
            comboBox1.DataSource = years.ToList();
        }


    }
}
