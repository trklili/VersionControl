using mintazh2.Abstarctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mintazh2.Entities
{
    internal class Food : Product
    {
        public Food()
        {
            Click += Food_Click;
        }

        private void Food_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("{0}\n{1}", Title, Description));
        }

        public string Description { get; set; }

        protected override void Display()
        {
            if (Calories < 750)
            {
                BackColor = Color.LightGreen;
            }
            else if (Calories < 1000)
            {
                BackColor = Color.LightYellow;
            }
            else
            {
                BackColor = Color.Salmon;
            }
        }
    }
}
