using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// The SingleEntryDimension's factory
    /// </summary>
    public sealed class SingleEntryFactory
    {
        #region Instance

        private static SingleEntryFactory _instance;

        internal static SingleEntryFactory Instance
        {
            get => _instance ?? (_instance = new SingleEntryFactory());
            set => _instance = value;
        }

        #endregion

        #region Static overloads

        /// <summary>
        /// Gets the entry. If the entry does not exists it will be created.
        /// </summary>
        /// <param name="entryName">The entry name.</param>
        /// <param name="locationToLoad">Allow you to specify tile location. You can also to set it later from an instance.</param>
        /// <returns>The entry</returns>
        public static SingleEntryDimension GetEntry(string entryName, Point locationToLoad = default)
        {
            return Instance.GetEntryInternal(entryName, locationToLoad);
        }

        /// <summary>
        /// Remove the entry by name.
        /// </summary>
        /// <param name="entryName">The entry name</param>
        public static void RemoveEntry(string entryName)
        {
            Instance.RemoveEntryInternal(entryName);
        }

        /// <summary>
        /// Creates a new entry. It is might useful for very specific cases.
        /// </summary>
        /// <param name="type">The type. Be sure that the type was registered.</param>
        /// <param name="size">The size.</param>
        /// <param name="location">The tile location.</param>
        /// <param name="entryName">The entry name.</param>
        /// <returns></returns>
        public static SingleEntryDimension CreateNewEntry(
            string type,
            Point size,
            Point location,
            string entryName)
        {
            return Instance.CreateNewEntryInternal(type, size, location, entryName);
        }

        #endregion

        private SingleEntryFactory() { }

        #region Internal implementation

        internal Dictionary<string, SingleEntryDimension> SingleEntryDimensions { get; set; } =
            new Dictionary<string, SingleEntryDimension>();

        internal void Load()
        {
            foreach (var entryDimension in SingleEntryDimensions)
            {
                try
                {
                    var entity = entryDimension.Value?.CurrentEntity;
                    if (entity == null)
                        continue;

                    entryDimension.Value.LoadDimensionNet(entity.Type, entity.Id, false);
                }
                catch (Exception e)
                {
                    DimensionKeeperMod.LogMessage($"{nameof(Load)} throw an error {e}");
                }
            }
        }

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

        #endregion
    }
}
