using DimensionKeeper.DimensionService;

namespace DimensionKeeper.Interfaces
{
    public interface IDimensionPhase
    {
        void ExecuteLoadPhaseInternal(DimensionEntity dimension);

        void ExecuteSynchronizePhaseInternal(DimensionEntity dimension);

        void ExecuteClearPhaseInternal(DimensionEntity dimension);
    }
}