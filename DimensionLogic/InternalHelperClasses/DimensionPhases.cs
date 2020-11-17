namespace TestMod.DimensionLogic.InternalHelperClasses
{
    public abstract class DimensionPhases
    {
        internal abstract void ExecuteLoadPhaseInternal(DimensionEntity dimension);

        internal abstract void ExecuteSynchronizePhaseInternal(DimensionEntity dimension);

        internal abstract void ExecuteClearPhaseInternal(DimensionEntity dimension);
    }
}