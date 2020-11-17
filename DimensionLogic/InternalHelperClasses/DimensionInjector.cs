namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionInjector
    {
        internal abstract void Load(DimensionEntity dimension);

        internal abstract void Synchronize(DimensionEntity dimension);

        internal abstract void Clear(DimensionEntity dimension);
    }
}