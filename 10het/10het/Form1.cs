using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10het
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;

        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;

        Brain winnerBrain = null;
        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;

            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);

            /* gc.AddPlayer();
             gc.Start(true);
            */


            gc.GameOver += Gc_GameOver;

            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.Start();

        }
    }
}
