using DimensionKeeper.DimensionService;

namespace DimensionKeeper.Interfaces.Internal
{
    internal interface IDimensionInjector
    {
        void Load(DimensionEntity dimension);

        void Synchronize(DimensionEntity dimension);

        void Clear(DimensionEntity dimension);
    }
}