using Olympics.medálok;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace Olympics
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData(@"C:\Users\torok\Source\Repos\VersionControl\Olympics\Summer_olympic_Medals.csv)");
            Evvalaszto();
            Helyezes();

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

        private int Rangsor(OlympicResult eredmeny)
        //bemenete egy OlympicResult a kimenete pedig egy szám (az adott OlympicResult helyezése).
        {
            var jobb = 0;
            //Szűrd le a results listát úgy, hogy csak azok a sorok szerepeljenek benne, amelyekben egyezik az év a bemenettel, de az ország különbözik.
            var szures = from r in results
                                  where r.Year == eredmeny.Year && r.Country != eredmeny.Country
                                  select r;
            foreach (var r in szures)
            {
                //Az adott sor aranyainak száma nagyobb, mint a bemenet aranyainak száma.
                if (r.Medals[0] > eredmeny.Medals[0])
                   jobb++;
                //Az adott sor aranyainak száma ugyanannyi, mint a bemenet aranyainak száma, de az ezüstök száma nagyobb.
                else if (r.Medals[0] == eredmeny.Medals[0])
                     if (r.Medals[1] > eredmeny.Medals[1])
                           jobb++;
                    //Az adott sor aranyainak és ezüstjeinek száma rendre ugyanannyi, mint a bemenet aranyainak és ezüstjeinek száma, de a bronzok száma nagyobb.
                    else if (r.Medals[1] == eredmeny.Medals[1])
                        if (r.Medals[2] > eredmeny.Medals[2])
                                jobb++;

            }
            return jobb+1;
        }

        private void Helyezes()
        {
        //paraméter nélküli függvény, amiben menj végig a results sorain, és minden sor esetén töltsd fel a Position tulajdonság értékét az előbb létrehozott függvényt felhasználva.
            foreach (var r in results)
            {
                r.Position = Rangsor(r);
            }
        }

        private void CreateExcel()
        {
            //kimeneti állomány fejlécei rendre a következők: Helyezés, Ország, Arany, Ezüst, Bronz
            var headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
            //LINQ segítségével szűrd le a results tartalmát úgy, hogy csak a legördülő lista szerint kiválasztott éve értékei szerepeljenek benne, és az értékek a helyezés szerint legyenek sorba rendezve.
            for (int i = 0; i < headers.Length; i++)
                xlSheet.Cells[1, i + 1] = headers[i];

            var filteredResults = from r in results
                                  where r.Year == (int)comboBox1.SelectedItem
            orderby r.Position
                                  select r;
            //Menj végig a szűrt lista sorain, és értelemszerűen írd az értékeket az Excel soraiba.
            var counter = 2;
            foreach (var r in filteredResults)
            {
                xlSheet.Cells[counter, 1] = r.Position;
                xlSheet.Cells[counter, 2] = r.Country;
                for (int i = 0; i <= 2; i++)
                    xlSheet.Cells[counter, i + 3] = r.Medals[i];
                counter++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateExcel();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

    }
}
