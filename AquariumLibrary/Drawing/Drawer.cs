using System;
using System.Collections.Generic;
using System.Drawing;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Drawing.Interfaces;

namespace AquariumLibrary.Drawing
{
    public class Drawer : IDrawer
    {
        public void Draw(Graphics graph, AGameObject gameObject)
        {
            if (gameObject is AFish)
                Draw(graph, (AFish)gameObject);
        }

        public void SetSettings(Type typeOfGameObject, FishState state, Image image, bool onceImage)
        {
            if (onceImage)
            {
                SetSettings(typeOfGameObject, image);
            }
            else
            {
                ObjectToImage[typeOfGameObject][state] = image;
            }
        }

        private void SetSettings(Type typeOfGameObject, Image image)
        {
            foreach (FishState value in Enum.GetValues(typeof(FishState)))
            {
                if (!ObjectToImage.ContainsKey(typeOfGameObject))
                    ObjectToImage.Add(typeOfGameObject, new Dictionary<FishState, Image>());
                if (!ObjectToImage[typeOfGameObject].ContainsKey(value))
                    ObjectToImage[typeOfGameObject].Add(value, image);
                else
                    ObjectToImage[typeOfGameObject][value] = image;
            }
        }

        private void Draw(Graphics graph, AFish fish)
        {
            var image = ObjectToImage[fish.GetType()][fish.FishState];
            graph.DrawImage(image, fish.Rectangle);
        }

        public Dictionary<Type, Dictionary<FishState, Image>> ObjectToImage 
            = new Dictionary<Type, Dictionary<FishState, Image>>();
    }
}
