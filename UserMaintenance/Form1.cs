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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {  
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
           label1.Text = Resource1.FullName;
            //label2.Text = Resource1.LastName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;
            button3.Text = Resource1.Delete;
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";


        }
       

        private void Button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
             FullName = textBox1.Text
               
            };

            users.Add(u);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Nevek (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                foreach (var u in users)
                {
                    sw.Write(u.ID);
                    sw.Write(";");
                    sw.Write(u.FullName);
                    sw.WriteLine();
                }
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string deleteID= (listBox1.SelectedValue).ToString();
            var s = from x in users
                    where (x.ID).ToString() == deleteID
                    select x;

            users.Remove(s.FirstOrDefault());
        }
    }
}
