namespace DimensionKeeper.DimensionService.InternalHelperClasses
{
    internal interface IDimensionInjector
    {
        void Load(DimensionEntity dimension);

        void Synchronize(DimensionEntity dimension);

        void Clear(DimensionEntity dimension);
    }
}