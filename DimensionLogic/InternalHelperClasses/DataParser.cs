namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DataParser
    {
        private DimensionEntity _cachedDimension;

        internal DimensionEntity GetDimension(string id)
        {
            if (AlwaysNewInternal || _cachedDimension == null)
            {
                _cachedDimension = LoadInternal(id);
            }

            return _cachedDimension;
        }

        internal abstract bool AlwaysNewInternal { get; }

        internal abstract DimensionEntity LoadInternal(string id);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}