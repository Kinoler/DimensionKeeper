using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.DefaultInjectors;
using DimensionKeeper.Interfaces;

namespace DimensionKeeper.DimensionExample
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
