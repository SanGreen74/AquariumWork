using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Drawing;
using AquariumLibrary.Drawing.Interfaces;
using AquariumLibrary.Fishes;
using AquariumLibrary.GameClasses;
using AquariumLibrary.Interfaces;

namespace AquariumWF
{
    public sealed class Form : System.Windows.Forms.Form
    {
        private readonly IAquarium _aquarium;
        private readonly IDrawer _drawer;
        private readonly string _pathToFishes = Application.StartupPath + @"\Resources\Fishes";
        private readonly string _pathToBackground = Application.StartupPath + @"\Resources\Background";

        public Form(Size size)
        {
            size -= new Size(50,100);
            Size = size;
            _aquarium = new Aquarium(size);
            _drawer = new Drawer();
            DoubleBuffered = true;
            Init();
            for (var i = 0; i < 30; i++)
            {
                var bn = new BlueNeon(
                    new PointF((float) Randomizer.rnd.Next(0, 600), (float) Randomizer.rnd.Next(0, 600)),
                    new SizeF(65, 65), _aquarium);
            }

            var pr2 = new Piranha(new PointF(Randomizer.rnd.Next(0, 600), Randomizer.rnd.Next(0, 600)), new SizeF(75, 75), _aquarium);
            var pr3 = new Piranha(new PointF(Randomizer.rnd.Next(0, 600), Randomizer.rnd.Next(0, 600)), new SizeF(75, 75), _aquarium);
            var pr = new Piranha(new PointF(Randomizer.rnd.Next(0, 600), Randomizer.rnd.Next(0, 600)), new SizeF(75, 75), _aquarium);
        }

        private void Init()
        {
            SetImagesSettings();
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

        private void SetImagesSettings()
        {
            BackgroundImage = Properties.Resources.BackgroundImage;
            _drawer.SetSettings(typeof(BlueNeon), FishState.None, Properties.Resources.NavalnyAllStates, true);
            _drawer.SetSettings(typeof(Piranha), FishState.None, Properties.Resources.MentosAllStates, true);
        }

        private void Draw()
        {
            Paint += (sender, args) =>
            {
                foreach (var fish in _aquarium.GetFishes())
                {
                    _drawer.Draw(args.Graphics, fish);
                    //args.Graphics.FillRectangle(Brushes.BlueViolet, fish.Rectangle);
                }
            };
        }
    }
}
