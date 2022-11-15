using labdagyar.Abstractions;
using labdagyar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labdagyar
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private Toy _nextToy;

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        

      

        private void createTimer_Tick_1(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);

        }

        private void conveyorTimer_Tick_1(object sender, EventArgs e)
        {
             var maxPosition = 0;
                        foreach (var toy in _toys)
                        {
                            toy.MoveToy();
                            if (toy.Left > maxPosition)
                                maxPosition = toy.Left;
                        }

                        if (maxPosition > 1000)
                        {
                            var oldestBall = _toys[0];
                            mainPanel.Controls.Remove(oldestBall);
                            _toys.Remove(oldestBall);
                        }
        }

        private void car_btn_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void ball_btn_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = color_btn.BackColor
            };
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 10;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void color_btn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }

        private void prsnt_btn_Click(object sender, EventArgs e)
        {

            Factory = new PresentFactory
            {
                BoxColor = box_color.BackColor,
                RibbonColor = ribbin_color.BackColor
            };

        }

        private void box_color_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }

        private void ribbin_color_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
                return;
            button.BackColor = colorPicker.Color;
        }
    }
}
