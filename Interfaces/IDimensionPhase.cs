using DimensionKeeper.DimensionService.InternalClasses;

namespace DimensionKeeper.Interfaces
{
    public interface IDimensionPhase
    {
        void ExecuteLoadPhaseInternal(DimensionEntityInternal dimension);

        void ExecuteSynchronizePhaseInternal(DimensionEntityInternal dimension);

        void ExecuteClearPhaseInternal(DimensionEntityInternal dimension);
    }
}