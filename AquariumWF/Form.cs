using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Drawing;
using AquariumLibrary.Drawing.Interfaces;
using AquariumLibrary.Factories;
using AquariumLibrary.Fishes;
using AquariumLibrary.GameClasses;
using AquariumLibrary.Interfaces;

namespace AquariumWF
{
    public sealed class Form : System.Windows.Forms.Form
    {
        public Form(Size size)
        {
            size -= new Size(50,150);
            Size = size;

            _buttonLocationX = size.Width - 2*_buttonDefaultSize.Width;

            _aquarium = new Aquarium(size);
            _drawer = new GameObjectDrawer();
            _fishFactory = new FishFactory(_aquarium);

            DoubleBuffered = true;
            SetImagesSettings();
            Init();
        }

        private void Init()
        {
            Draw();
            var render = new Timer { Interval = 1 };
            render.Tick += (sender, args) => { Invalidate(); };
            render.Start();
            var frames = new Timer { Interval = 15 };
            frames.Tick += (sender, args) =>
            {
                _aquarium.GetFishes().ToList().ForEach(x =>
                {
                    x.Update();
                    x.HandleCollisions();
                });
            };
            frames.Start();
            RegisterButton("Neon", ButtonPoint(1), _buttonDefaultSize,
                (s, e) => _aquarium.AddObject(_fishFactory.GetBlueNeon()));
            RegisterButton("Piranha", ButtonPoint(2), _buttonDefaultSize,
                (s, e) => _aquarium.AddObject(_fishFactory.GetPiranha()));
            RegisterButton("Swordsman", ButtonPoint(3), _buttonDefaultSize,
                (s, e) => _aquarium.AddObject(_fishFactory.GetSwordsMan()));
            RegisterButton("Catfish", ButtonPoint(4), _buttonDefaultSize,
                (s, e) => _aquarium.AddObject(_fishFactory.GetCatfish()));
        }

        private void SetImagesSettings()
        {
            BackgroundImage = Properties.Resources.BackgroundImage;
            _drawer.SetSettings(typeof(BlueNeon), FishState.None, Properties.Resources.NavalnyWalking, true);

            _drawer.SetSettings(typeof(Piranha), FishState.None, Properties.Resources.MentosAllStates, true);

            _drawer.SetSettings(typeof(Catfish), FishState.Walking, Properties.Resources.DimonNotSleep, true);
            _drawer.SetSettings(typeof(Catfish), FishState.Sleep, Properties.Resources.DimonSleep, false);

            _drawer.SetSettings(typeof(SwordsMan), FishState.None, Properties.Resources.PutinWalking, true);
            _drawer.SetSettings(typeof(SwordsMan), FishState.Attack, Properties.Resources.PutinAttack, false);
        }

        private void Draw()
        {
            Paint += (sender, args) =>
            {
                foreach (var fish in _aquarium.GetFishes())
                {
                    _drawer.Draw(args.Graphics, fish);
                }
            };
        }

        private void RegisterButton(string name, Point location, Size size, EventHandler clickHandler)
        {
            var button = new Button
            {
                Text = name,
                Location = location,
                Size = size
            };
            button.Click += clickHandler;
            Controls.Add(button);
        }

        private readonly IAquarium _aquarium;
        private readonly IDrawer _drawer;
        private readonly Size _buttonDefaultSize = new Size(70,35);
        private readonly int _buttonLocationX;
        private readonly FishFactory _fishFactory;
        private Point ButtonPoint(int count) => new Point(_buttonLocationX, _buttonDefaultSize.Height * count);
    }
}
