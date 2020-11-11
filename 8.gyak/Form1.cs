using _8.gyak.Abstarctions;
using _8.gyak.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _8.gyak
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private Toy _nextToy;
        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set
            {
                _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
            createTimer.Start();
            conveyorTimer.Start();
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {   
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _toys)
            {
                ball.MoveToy();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory()
            {
                BallColor = button3.BackColor
            };
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Factory = new PresenFactory
            {
                box1 = button5.BackColor,
                ribbon1 = button6.BackColor
                

            };
        }
    }
}
