using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace _4.gyak
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();





        }
        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }
        private void CreateExcel()
        {
            try
            {
                
                xlApp = new Excel.Application();

                
                xlWB = xlApp.Workbooks.Add(Missing.Value);

               
                xlSheet = xlWB.ActiveSheet;

                
                CreateTable(); 

                
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
        private void CreateTable()
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
             "Négyzetméter ár (Ft/m2)"};

            for (int i = 1; i < headers.Length+1; i++)
            {
                xlSheet.Cells[1, i] = headers[i-1];
            }
            

            object[,] values = new object[Flats.Count, headers.Length];
            int counter = 0;
            int a = 2;
            foreach (Flat f in Flats)
            {
                
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                values[counter, 4] = f.Elevator;
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "="+GetCell(a,7)+"*"+GetCell(a, 8);
                counter++;
                a++;

            }

            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            int lastRowID = xlSheet.UsedRange.Rows.Count;

            Excel.Range táblarange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, headers.Length));
            táblarange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range elsőoszlop= xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            elsőoszlop.Font.Bold = true;
            elsőoszlop.Interior.Color = Color.LightYellow;
            Excel.Range utolsóoszlop = xlSheet.get_Range(GetCell(2, 9), GetCell(lastRowID, 9));
            utolsóoszlop.Interior.Color = Color.LightGreen;
            
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
