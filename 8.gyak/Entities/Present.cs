using _8.gyak.Abstarctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8.gyak.Entities
{
    class Present : Toy
    {
        public SolidBrush ribbon1 { get; private set; }
        public SolidBrush box1 { get; private set; }

        public Present(Color ribbon, Color box)
        {
            ribbon1 = new SolidBrush(ribbon);
            box1 = new SolidBrush(box);
        }

        protected override void DrawImage(Graphics g)
        {
           g.FillRectangle(box1, 0, 0, 50, 50 );
            g.FillRectangle(ribbon1, 20, 0, 10, 50);
            g.FillRectangle(ribbon1, 0, 20, 50, 10);
        }
    }
}
