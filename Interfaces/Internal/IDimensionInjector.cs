using DimensionKeeper.DimensionService.InternalClasses;

namespace DimensionKeeper.Interfaces.Internal
{
    internal interface IDimensionInjector
    {
        void Load(DimensionEntityInternal dimension);

        void Synchronize(DimensionEntityInternal dimension);

        void Clear(DimensionEntityInternal dimension);
    }
}