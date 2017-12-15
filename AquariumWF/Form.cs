using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.Interfaces;

namespace AquariumWF
{
    public sealed class Form : System.Windows.Forms.Form
    {
        private IAquarium _aquarium;
        public Form()
        {
            _aquarium = new Aquarium(new Size(900, 600));
            var bn = new BlueNeon(new PointF(50, 50), new SizeF(30, 15), _aquarium);
            var bn1 = new BlueNeon(new PointF(80, 50), new SizeF(30, 15), _aquarium);
            var bn2 = new BlueNeon(new PointF(110, 50), new SizeF(30, 15), _aquarium);
            var bn3 = new BlueNeon(new PointF(150, 50), new SizeF(30, 15), _aquarium);


            var pr = new Piranha(new PointF(250,200), new SizeF(70,30), _aquarium);
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
                _aquarium.GetFishes().ToList().ForEach(x=>x.Update());
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
