using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.BaseClasses;
using AquariumLibrary.Fishes;
using AquariumLibrary.GameClasses;
using AquariumLibrary.Interfaces;

namespace AquariumLibrary.Factories
{
    public class FishFactory
    {
        private readonly IAquarium _aquarium;
        private PointF _randomPoint => new PointF(Randomizer.Next(0, _aquarium.Size.Width), Randomizer.Next(0, _aquarium.Size.Height));
        public FishFactory(IAquarium aquarium)
        {
            _aquarium = aquarium;
        }
        public BlueNeon GetBlueNeon()
        {
            return new BlueNeon(_randomPoint, Settings.BlueNeon.Size, _aquarium);
        }

        public Piranha GetPiranha()
        {
            return new Piranha(_randomPoint, Settings.Piranha.Size, _aquarium);
        }

        public Catfish GetCatfish()
        {
            var spawnPoint = new PointF(Randomizer.Next(0, _aquarium.Size.Width), 
                Randomizer.Next(_aquarium.Size.Height/2, _aquarium.Size.Height));
            return new Catfish(spawnPoint, Settings.Catfish.Size, _aquarium);
        }

        public SwordsMan GetSwordsMan()
        {
            return new SwordsMan(_randomPoint, Settings.SwordsMan.Size, _aquarium);
        }
    }
}
