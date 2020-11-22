using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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
            string entryName)
        {
            return Instance.CreateNewEntryInternal(type, size, location, entryName);
        }

        private SingleEntryFactory()
        {
        }

        internal Dictionary<string, SingleEntryDimension> SingleEntryDimensions { get; set; } = 
            new Dictionary<string, SingleEntryDimension>();

        private SingleEntryDimension GetEntryInternal(string entryName, Point locationToLoad = default)
        {
            if (entryName == null)
                throw new ArgumentNullException(nameof(entryName));

            if (!SingleEntryDimensions.ContainsKey(entryName))
                SingleEntryDimensions.Add(entryName, new SingleEntryDimension());

            var entry = SingleEntryDimensions[entryName];
            if (locationToLoad != default)
                entry.LocationToLoad = locationToLoad;
            entry.EntryName = entryName;

            return entry;
        }

        private void RemoveEntryInternal(string entryName)
        {
            if (entryName == null)
                throw new ArgumentNullException(nameof(entryName));

            if (SingleEntryDimensions.ContainsKey(entryName)) 
                SingleEntryDimensions.Remove(entryName);
        }

        private SingleEntryDimension CreateNewEntryInternal(
            string type,
            Point size,
            Point location,
            string entryName)
        {
            if (entryName == null)
                throw new ArgumentNullException(nameof(entryName));

            var singleEntryDimension = new SingleEntryDimension
            {
                CurrentEntity = DimensionRegister.Instance.GetStorage(type).CreateEmptyEntity(new Point(location.X, location.Y - size.Y), size),
                LocationToLoad = location
            };

            SingleEntryFactory.Instance.SingleEntryDimensions[entryName] = singleEntryDimension;
            return singleEntryDimension;
        }
    }
}
