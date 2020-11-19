using TestMod.DimensionLogic;
using TestMod.DimensionLogic.DefaultInjectors;
using TestMod.Interfaces;

namespace TestMod.DimensionExample
{
    public class DimensionRegisterExample : IDimensionRegister
    {
        public const string ExampleName = "Name";

        public void Register(DimensionsRegister register)
        {
            register.Register<DefaultInjector<Dimension>, DimensionStorageExample, Dimension>(ExampleName);
            register.Register<DefaultInjector<Dimension>, DimensionStorageExample, Dimension>(DimensionKeeperMod.EyeDropperTypeName);
        }
    }
}
