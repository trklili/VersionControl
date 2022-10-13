using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace negyedikhet
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateTable();
            CreateExcel();
        } 
        
        private void LoadData()
        {
            Flats = context.Flats.ToList();
            // A List<Flat> Flats = context.Flats.ToList() sor lemásolja a context.Flats táblát egy Flats nevű Flat típusú elemekből álló listába a memóriába. A listán végrehajtott LINQ műveletek már lokálisan kerülnek kiszámolásra a szerver terhelése nélkül. (LINQ to SQL vs. LINQ to objets)
        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // T
                // ábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }


        }
      
    

        public void CreateTable()
        {
            string[] headers = new string[] 
            {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"
            };

            //for ciklus segítségével írd ki a tömb elemeit a munkalap első sorába. 
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];
            //Egy foreach ciklussal menj végig a Flats lista sorain, és tölts fel a tömböt a megfelelő adatokkal.
            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                if (f.Elevator == true)
                {
                    values[counter, 4] = "Van";
                }
                else values[counter, 4] = "Nincs";

                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = $"=(H{counter + 2} /G{counter + 2}) * 1000000";
                   //Az utolsó (9.) oszlopba egy Excel képlet kerüljön string formában. Az Excel képletek szövegesek és mindig "=" jellel kezdődnek. A pontos referenciák meghatározásához használd a korábban létrehozott GetCell függvényt.
                
                counter++;


            }


            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
           

           
            //Törzs

            int lastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range tableRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, headers.Length));
            tableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            //1.oszlop
            Excel.Range firstcolumnRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID - 1, 1));
            firstcolumnRange.Interior.Color = Color.LightYellow;
            firstcolumnRange.Font.Bold = true;

            //utolso oszlop
            int lastColID = xlSheet.UsedRange.Columns.Count;

            Excel.Range lastcolumnRange = xlSheet.get_Range(GetCell(2, lastColID), GetCell(lastRowID - 1, lastColID));
            lastcolumnRange.Interior.Color = Color.LightGreen;
            lastcolumnRange.NumberFormat = "0.00";

            //header
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

       
       

 


    }
}
