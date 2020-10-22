using _7.gyak.Entities;
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

namespace _7.gyak
{
    public partial class Form1 : Form
    {
        List<Person> People = new List<Person>();
        List<BirthProbability> Születés = new List<BirthProbability>();
        List<DeathProbability> Halál = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
            People = GetPeople(@"C:\Temp\nép.csv");
            Születés = GetSzületés(@"C:\Temp\születés.csv");
            Halál = GetHalál(@"C:\Temp\halál.csv");

            dataGridView1.DataSource = Halál;

        }

        private List<DeathProbability> GetHalál(string v)
        {
            List<DeathProbability> halál = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(v, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    halál.Add(new DeathProbability()
                    {
                        Kor = int.Parse(line[1]),
                        P = double.Parse(line[2]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0])
                    });
                }
            }

            return halál;
        }

        private List<BirthProbability> GetSzületés(string v)
        {
            List<BirthProbability> születés = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(v, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    születés.Add(new BirthProbability()
                    {
                        Kor = int.Parse(line[0]),
                        P = double.Parse(line[2]),
                        GyermekekSzáma = int.Parse(line[1])
                    });

                }
            }


            return születés;
        }

        public List<Person> GetPeople(string csvpath)
        {
            List<Person> people = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    people.Add(new Person() {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NumberOfChildren = int.Parse(line[2])
                    });

                }
            }
            return people;
        }
       

        
    }
}
