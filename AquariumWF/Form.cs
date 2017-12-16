using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
            _aquarium = new Aquarium(new Size(1200, 600));
            BlueNeon bn;
            PointF p = new PointF(0,0);
            for (var i = 0; i < 40; i++)
            {
                p = new PointF(Random1.rnd.Next(1,1200), Random1.rnd.Next(1,600));
                bn = new BlueNeon(p, new SizeF(20,10),_aquarium);
            }
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
                _aquarium.GetFishes().ToList().ForEach(x =>
                {
                    x.Update();
                    x.HandleCollisions();

                });
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
