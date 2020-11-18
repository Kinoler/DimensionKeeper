using System.Collections.Generic;

namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionStorageInternal
    {
        internal DimensionEntity GetDimension(string id)
        {
            return LoadInternal(id);
        }

        internal abstract DimensionEntity LoadInternal(string id);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}