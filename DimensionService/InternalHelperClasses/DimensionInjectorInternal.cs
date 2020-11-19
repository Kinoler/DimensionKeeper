namespace DimensionKeeper.DimensionService.InternalHelperClasses
{
    public abstract class DimensionInjectorInternal
    {
        internal abstract void Load(DimensionEntity dimension);

        internal abstract void Synchronize(DimensionEntity dimension);

        internal abstract void Clear(DimensionEntity dimension);
    }
}