using _6.gyak.Entities;
using _6.gyak.MnbServiceReference;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace _6.gyak
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
        GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
        {
            currencyNames = "EUR",
            startDate = "2020-01-01",
            endDate = "2020-06-30"
        };

        BindingList<RateData> rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();
            XML();

            dataGridView1.DataSource = rates;
           



        }
        private void XML()
        {
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
        }


    }
}
