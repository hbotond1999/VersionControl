using System.Windows.Forms;
using WorldsHardestGame;

namespace _9.gyak
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;
        public Form1()
        {
            InitializeComponent();

            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);
            
        }
    }
}
