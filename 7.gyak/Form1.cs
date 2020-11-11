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
        Random rng = new Random(1234);
        List<int> férfi = new List<int>();
        List<int> nő = new List<int>();

        public Form1()
        {
            InitializeComponent();
            People = GetPeople(textBox1.Text);
            Születés = GetSzületés(@"C:\Temp\születés.csv");
            Halál = GetHalál(@"C:\Temp\halál.csv");

            

            

           

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
                        NumberOfChildren = int.Parse(line[2]),
                        Isalive=bool.Parse(line[3])
                        
                    });

                }
            }
            return people;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Simulation();
        }

        private void Simulation()
        {
            richTextBox1.Clear();
            for (int év = 2005; év <= numericUpDown1.Value; év++)
            {
                for (int i = 0; i <= People.Count(); i++)
                {
                    Person person = new Person();
                    if (!person.Isalive) return;

                    var age = év - person.BirthYear;

                    double pDeath = (from x in Halál
                                     where x.Gender == person.Gender && x.Kor == age
                                     select x.P).FirstOrDefault();

                    if (rng.NextDouble() <= pDeath)
                        person.Isalive = false;

                    if (person.Isalive == true && person.Gender == Gender.Female)
                    {
                        double pszül = (from x in Születés
                                        where x.Kor == age
                                        select x.P).FirstOrDefault();

                        if (rng.NextDouble() <= pszül)
                        {
                            Person újszülött = new Person();
                            újszülött.BirthYear = év;
                            újszülött.NumberOfChildren = 0;
                            újszülött.Gender = (Gender)(rng.Next(1, 3));
                            People.Add(újszülött);
                        }
                    }
                }
                var Males = (from x in People
                             where x.Gender == Gender.Male && x.Isalive
                             select x).Count();
                var Females = (from x in People where x.Gender == Gender.Female && x.Isalive select x).Count();

               
                nő.Add(Females);
                férfi.Add(Males);

                
            }
            DisplayResults();

        }

        private void DisplayResults()
        {

            
            for (int i = 2005; i <= numericUpDown1.Value; i++)
            {
                
                richTextBox1.Text ="Év:"+i+"\n"+"\t"+"Nő:"+nő[i-2005]+"\n"+"\t"+"Férfi:"+férfi[i-2005];
              

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
           
        }
    }
}
