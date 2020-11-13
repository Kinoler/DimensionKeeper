using Terraria;
using TestMod.DimensionExample;
using TestMod.Helpers;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    public static class DimensionLoader
    {
        internal static DimensionsRegister RegisteredDimension { get; } = new DimensionsRegister();

        private static DataParser CurrentParser { get; set; }
        private static DimensionInjector CurrentInjector { get; set; }
        private static Dimension CurrentDimension { get; set; }

        public static void RegisterDimensions<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            RegisterDimensions(new TDimensionRegister());
        }

        public static void RegisterDimensions(IDimensionRegister register)
        {
            register.Register(RegisteredDimension);
        }

        public static void Load(string name, bool savePrev = false)
        {
            if (savePrev)
                SaveCurrentDimension();

            ClearCurrentDimension();

            CurrentParser = RegisteredDimension.GetParser("Name");
            CurrentInjector = RegisteredDimension.GetInjector("Name");
            CurrentDimension = CurrentParser.GetDimension();

            LoadCurrentDimension();
        }

        private static void LoadCurrentDimension()
        {
            if (CurrentDimension == null)
                return;
            CurrentInjector.Load(CurrentDimension);
        }

        private static void SaveCurrentDimension()
        {
            if (CurrentDimension == null)
                return;
            CurrentInjector.Save(CurrentDimension);
        }

        private static void ClearCurrentDimension()
        {
            if (CurrentDimension == null)
                return;
            CurrentInjector.Clear(CurrentDimension);
        }
    }
}
