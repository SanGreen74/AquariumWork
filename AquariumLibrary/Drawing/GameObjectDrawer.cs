using System;
using System.Collections.Generic;
using System.Drawing;
using AquariumLibrary.AbstractClasses;
using AquariumLibrary.Drawing.Interfaces;

namespace AquariumLibrary.Drawing
{
    public class GameObjectDrawer : IDrawer
    {
        public void Draw(Graphics graph, AGameObject gameObject)
        {
            if (gameObject is AFish)
                Draw(graph, (AFish)gameObject);
        }

        public void SetSettings(Type typeOfGameObject, Enum state, Image image)
        {
            SetSettings(typeOfGameObject, state, image, false);;
        }

        public void SetSettings(Type typeOfGameObject, Enum state, Image image, bool onceImage)
        {
            if (onceImage)
                AddConformity(typeOfGameObject, image);
            else
                Add(typeOfGameObject, image, (FishState)state);
        }

        private void AddConformity(Type typeOfGameObject, Image image)
        {
            foreach (FishState value in Enum.GetValues(typeof(FishState)))
                Add(typeOfGameObject, image, value);
        }

        private void Add(Type typeOfGameObject, Image image, FishState value)
        {
            if (!ObjectToImage.ContainsKey(typeOfGameObject))
                ObjectToImage.Add(typeOfGameObject, new Dictionary<FishState, Image>());
            if (!ObjectToImage[typeOfGameObject].ContainsKey(value))
                ObjectToImage[typeOfGameObject].Add(value, image);
            else
                ObjectToImage[typeOfGameObject][value] = image;
        }

        private void Draw(Graphics graph, AFish fish)
        {
            var image = ObjectToImage[fish.GetType()][fish.State];
            graph.DrawImage(image, fish.Rectangle);
        }

        public Dictionary<Type, Dictionary<FishState, Image>> ObjectToImage 
            = new Dictionary<Type, Dictionary<FishState, Image>>();
    }
}
