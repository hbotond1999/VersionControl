using _6.gyak.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6.gyak
{
    public partial class Form1 : Form
    {
        MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
        GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody()
        {
            currencyNames = "Valuta",
            startDate = "2020-01-01",
            endDate= "2020-06-30"
        };
        
        public Form1()
        {
            InitializeComponent();
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
        }
    }
}
