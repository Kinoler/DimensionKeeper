using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService
{
    public sealed class SingleEntryFactory
    {
        private static SingleEntryFactory _instance;

        internal static SingleEntryFactory Instance
        {
            get => _instance ?? (_instance = new SingleEntryFactory());
            set => _instance = value;
        }

        public static SingleEntryDimension GetEntry(string entryName, Point locationToLoad = default)
        {
            return Instance.GetEntryInternal(entryName, locationToLoad);
        }

        public static void RemoveEntry(string entryName)
        {
            Instance.RemoveEntryInternal(entryName);
        }

        public static SingleEntryDimension CreateNewEntry(
            string type,
            Point size,
            Point location,
            string entryName = null)
        {
            return Instance.CreateNewEntryInternal(type, size, location, entryName);
        }

        internal SingleEntryFactory()
        {
        }

        internal Dictionary<string, SingleEntryDimension> SingleEntryDimensions { get; set; } = 
            new Dictionary<string, SingleEntryDimension>();

        public SingleEntryDimension GetEntryInternal(string entryName, Point locationToLoad = default)
        {
            if (!SingleEntryDimensions.ContainsKey(entryName))
                SingleEntryDimensions.Add(entryName, new SingleEntryDimension());

            if (locationToLoad != default)
                SingleEntryDimensions[entryName].LocationToLoad = locationToLoad;

            return SingleEntryDimensions[entryName];
        }

        public void RemoveEntryInternal(string entryName)
        {
            if (SingleEntryDimensions.ContainsKey(entryName)) 
                SingleEntryDimensions.Remove(entryName);
        }

        public SingleEntryDimension CreateNewEntryInternal(
            string type,
            Point size,
            Point location,
            string entryName = null)
        {
            var singleEntryDimension = new SingleEntryDimension
            {
                CurrentEntity = DimensionRegister.Instance.GetStorage(type).CreateEmptyEntity(location, size),
                LocationToLoad = location
            };

            if (entryName != null)
                SingleEntryFactory.Instance.SingleEntryDimensions[entryName] = singleEntryDimension;
            return singleEntryDimension;
        }
    }
}
