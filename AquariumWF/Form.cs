using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Interfaces;

namespace AquariumWF
{
    public class Form : System.Windows.Forms.Form
    {
        private IAquarium _aquarium;
        public Form()
        {
            _aquarium = new Aquarium(new Size(500, 500));
            Size = new Size(1000, 1000);
            DoubleBuffered = true;
            Init();
        }

        private void Init()
        {
            Draw();
            var render = new Timer() { Interval = 1 };
            render.Tick += (sender, args) => { Invalidate(); };
            render.Start();
            var frames = new Timer() { Interval = 15 };
            frames.Tick += (sender, args) =>
            {
                foreach (var fish in _aquarium.GetFishes())
                    fish.Move();
            };
            frames.Start();
        }

        private void Draw()
        {
            Paint += (sender, args) =>
            {
                foreach (var fish in _aquarium.GetFishes())
                {
                    args.Graphics.FillRectangle(Brushes.BlueViolet, fish.Rectangle);
                }
            };
        }
    }
}
