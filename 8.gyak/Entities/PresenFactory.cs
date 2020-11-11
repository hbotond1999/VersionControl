using _8.gyak.Abstarctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8.gyak.Entities
{
    public class PresenFactory : IToyFactory
    {
        public Color ribbon1 { get;  set; }
        public Color box1 { get;  set; }
        public Toy CreateNew()
        {
            return new Present(ribbon1, box1);
        }
    }
}
