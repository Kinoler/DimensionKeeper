using System.Collections.Generic;

namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionStorageInternal
    {
        protected internal readonly Dictionary<string, DimensionEntity> CachedDimensions = 
            new Dictionary<string, DimensionEntity>();

        internal DimensionEntity GetDimension(string id)
        {
            if (AlwaysNewInternal)
                return LoadInternal(id);

            if (!CachedDimensions.ContainsKey(id)) 
                CachedDimensions.Add(id, LoadInternal(id));

            return CachedDimensions[id];
        }

        internal abstract bool AlwaysNewInternal { get; }

        internal abstract DimensionEntity LoadInternal(string id);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}