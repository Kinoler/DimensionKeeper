namespace DimensionKeeper.DimensionService.InternalHelperClasses
{
    public abstract class DimensionPhasesInternal
    {
        internal abstract void ExecuteLoadPhaseInternal(DimensionEntity dimension);

        internal abstract void ExecuteSynchronizePhaseInternal(DimensionEntity dimension);

        internal abstract void ExecuteClearPhaseInternal(DimensionEntity dimension);
    }
}