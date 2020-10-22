using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7.gyak.Entities
{
    public class Person
    {
        public int BirthYear { get; set; }
       
        public Gender Gender { get; set; }
        public int NumberOfChildren { get; set; }
        public bool Isalive { get; set; }

    }
}
