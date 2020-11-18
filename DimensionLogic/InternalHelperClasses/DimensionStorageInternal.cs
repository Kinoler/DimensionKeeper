using System.Collections.Generic;

namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionStorageInternal
    {
        private readonly Dictionary<string, DimensionEntity> _cachedDimensions = 
            new Dictionary<string, DimensionEntity>();

        internal DimensionEntity GetDimension(string id)
        {
            if (AlwaysNewInternal)
            {
                return LoadInternal(id);
            }

            if (!_cachedDimensions.ContainsKey(id))
            {
                _cachedDimensions.Add(id, LoadInternal(id));
            }

            return _cachedDimensions[id];
        }

        internal abstract bool AlwaysNewInternal { get; }

        internal abstract DimensionEntity LoadInternal(string id);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}