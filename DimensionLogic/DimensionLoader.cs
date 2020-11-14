using Terraria;
using Terraria.ModLoader.IO;
using TestMod.DimensionExample;
using TestMod.Helpers;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// The main class DimensionKeeper that allows you to manipulate dimensions.
    /// </summary>
    public static class DimensionLoader
    {
        internal static TagCompound DimensionsTag { get; set; }
        internal static DimensionsRegister RegisteredDimension { get; private set; }

        private static DataParser CurrentParser { get; set; }
        private static DimensionInjector CurrentInjector { get; set; }
        private static Dimension CurrentDimension { get; set; }

        private static bool DimensionLoaded => CurrentDimension != null;

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
        /// <param name="name">The name which dimension is associated.</param>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        public static void LoadDimension(string name, bool synchronizePrevious = false)
        {
            if (synchronizePrevious)
                SynchronizeCurrentDimension();

            ClearCurrentDimension();

            CurrentParser = RegisteredDimension.GetParser("Name");
            CurrentInjector = RegisteredDimension.GetInjector("Name");

            CurrentDimension = CurrentParser.GetDimension(name);

            LoadCurrentDimension();
        }

        private static void LoadCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Load(CurrentDimension);
        }

        private static void SynchronizeCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Synchronize(CurrentDimension);
            CurrentParser.SaveInternal(CurrentDimension);
        }

        private static void ClearCurrentDimension()
        {
            if (!DimensionLoaded)
                return;
            CurrentInjector.Clear(CurrentDimension);
        }

        internal static void Initialize()
        {
            RegisteredDimension = new DimensionsRegister();
        }

        internal static void Unload()
        {
            RegisteredDimension = null;
            DimensionsTag = null;
            CurrentParser = null;
            CurrentInjector = null;
            CurrentDimension = null;
        }
    }
}
