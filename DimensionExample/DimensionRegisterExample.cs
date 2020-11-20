using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.HelperImplementations.Injectors;
using DimensionKeeper.HelperImplementations.Storages;
using DimensionKeeper.Interfaces;

namespace DimensionKeeper.DimensionExample
{
    public class DimensionRegisterExample : IDimensionRegister
    {
        public const string ExampleName = "Name";

        public void Register(DimensionRegister register)
        {
            register.Register<StandardInjector<Dimension>, TagCompoundFromFileStorage<Dimension>, Dimension>(ExampleName);
            register.Register<StandardInjector<Dimension>, ResourceManagerStorage<Dimension>, Dimension>(DimensionKeeperMod.EyeDropperTypeName);
        }
    }
}
