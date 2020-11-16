using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic.InternalHelperClasses;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// The main class DimensionKeeper that allows you to manipulate dimensions.
    /// </summary>
    public static class DimensionLoader
    {
        internal static DimensionsRegister RegisteredDimension { get; private set; }

        private static DataParser CurrentParser { get; set; }
        private static DimensionInjector CurrentInjector { get; set; }

        private static DimensionEntity CurrentEntity { get; set; }

        private static bool DimensionLoaded => CurrentEntity != null;

        /// <summary>
        /// Register the open generic type which register the dimensions.
        /// </summary>
        /// <typeparam name="TDimensionRegister">The class which inherit from <see cref="IDimensionRegister"/>.</typeparam>
        public static void RegisterDimensions<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            RegisterDimensions(new TDimensionRegister());
        }

        /// <summary>
        /// Register the class which register the dimensions.
        /// </summary>
        /// <param name="register">The instance of class which inherit from <see cref="IDimensionRegister"/>.</param>
        public static void RegisterDimensions(IDimensionRegister register)
        {
            register.Register(RegisteredDimension);
        }

        /// <summary>
        /// Load (inject) registered dimension into the world.
        /// </summary>
        /// <param name="type">The type which dimension was registered.</param>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        /// <param name="locationToLoad">Specify the dimension loading tile. Points to the upper left corner.</param>
        public static void LoadDimension(string type, Point locationToLoad, bool synchronizePrevious = true)
        {
            if (synchronizePrevious)
                SynchronizeCurrentDimension();

            ClearCurrentDimension();

            CurrentParser = RegisteredDimension.GetParser(type);
            CurrentInjector = RegisteredDimension.GetInjector(type);

            CurrentEntity = CurrentParser.GetDimension(type);
            CurrentEntity.Location = new Point(locationToLoad.X, locationToLoad.Y - CurrentEntity.DimensionInternal.Height);

            LoadCurrentDimension();
        }

        private static void LoadCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Load(CurrentEntity);
        }

        private static void SynchronizeCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Synchronize(CurrentEntity);
            CurrentParser.SaveInternal(CurrentEntity);
        }

        private static void ClearCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Clear(CurrentEntity);
        }

        internal static TagCompound Save()
        {
            var tag = new TagCompound
            {
                {"TypeName", CurrentEntity?.TypeName}, 
                {"Id", CurrentEntity?.Id}, 
                {"Location", CurrentEntity?.Location}
            };

            return tag;
        }

        internal static void Load(TagCompound tag)
        {
            var name = tag.Get<string>("TypeName");
            if (string.IsNullOrEmpty(name))
                return;

            CurrentEntity = RegisteredDimension.GetParser(name).GetDimension(name);
            CurrentEntity.Location = tag.Get<Point>("Location");
            CurrentEntity.Id = tag.Get<string>("Id");
        }

        internal static void Initialize()
        {
            RegisteredDimension = new DimensionsRegister();
        }

        internal static void Unload()
        {
            RegisteredDimension = null;
            Clear();
        }

        internal static void Clear()
        {
            CurrentParser = null;
            CurrentInjector = null;
            CurrentEntity = null;
        }
    }
}
