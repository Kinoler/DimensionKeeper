﻿namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionStorageInternal
    {
        internal abstract DimensionEntity LoadInternal(string id);

        internal abstract void SaveInternal(DimensionEntity dimension);
    }
}