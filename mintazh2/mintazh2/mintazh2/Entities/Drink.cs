using mintazh2.Abstarctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mintazh2.Entities
{
    internal class Drink : Product
    {
        protected override void Display()
        {
            BackColor = Color.Blue;
        }
    }
}
