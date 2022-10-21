using hatodikhet.Entities;
using hatodikhet.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace hatodikhet
{
    public partial class Form1 : Form

    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            RequestCurrencies();
            comboBox1.DataSource = Currencies;
            RefreshData();
           

        }

        private void RequestCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var MnbGetExResp = mnbService.GetCurrencies(request);
            var result = MnbGetExResp.GetCurrenciesResult;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement x in xml.DocumentElement.ChildNodes[0])
            {
                Currencies.Add(x.InnerText);

            }
        }

        private void RefreshData()
        {
            Rates.Clear();
            

            chartRateData.DataSource = Rates;
            dataGridView1.DataSource = Rates;
            Webszolg();


            Adatmegjelenites();
        }

        public void Webszolg()
        {
          
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (comboBox1.SelectedItem).ToString(),
                startDate = (dateTimePicker1.Value).ToString(),
                endDate = (dateTimePicker2.Value).ToString()
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            GetXML(result);
        }
       

        private void GetXML(string result)
        {

            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                
                var rate = new RateData();
                Rates.Add(rate);

                
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null) continue;

                rate.Currency = childElement.GetAttribute("request");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }

        }

        private void Adatmegjelenites()
        {
            

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
