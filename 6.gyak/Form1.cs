using _6.gyak.Entities;
using _6.gyak.MnbServiceReference;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _6.gyak
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();


        BindingList<RateData> rates = new BindingList<RateData>();

        
        public Form1()
        {
            InitializeComponent();
           
            RefreshData();
            Vondaldiagramm();
           


        }

        private void RefreshData()
        {   
            
            rates.Clear();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (comboBox1.SelectedItem).ToString(),
                startDate = (dateTimePicker1.Value).ToString(),
                endDate = (dateTimePicker2.Value).ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));


                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");


                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
            dataGridView1.DataSource = rates;
        }
        private void Vondaldiagramm()
        {
            chartRateData.DataSource = rates;
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

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
          
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
            
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
           
        }




    }
}
