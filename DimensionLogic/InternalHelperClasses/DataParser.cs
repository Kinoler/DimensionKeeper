namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DataParser
    {
        private DimensionEntity _cachedDimension;

        internal DimensionEntity GetDimension(string name)
        {
            if (AlwaysNewInternal || _cachedDimension == null)
            {
                _cachedDimension = LoadInternal(name);
            }

            return _cachedDimension;
        }

        internal abstract bool AlwaysNewInternal { get; }

        internal abstract DimensionEntity LoadInternal(string name);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}