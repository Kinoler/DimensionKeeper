using TestMod.DimensionService;
using TestMod.DimensionService.DefaultInjectors;
using TestMod.Interfaces;

namespace TestMod.DimensionExample
{
    public class DimensionRegisterExample : IDimensionRegister
    {
        public const string ExampleName = "Name";

        public void Register(DimensionRegister register)
        {
            register.Register<DefaultInjector<Dimension>, DimensionStorageExample, Dimension>(ExampleName);
            register.Register<DefaultInjector<Dimension>, DimensionStorageExample, Dimension>(DimensionKeeperMod.EyeDropperTypeName);
        }
    }
}
