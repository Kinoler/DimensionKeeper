using DimensionKeeper.DimensionService.DefaultPhases;

namespace DimensionKeeper.DimensionService.DefaultInjectors
{
    public class DefaultInjector<TDimension> : DimensionInjector<TDimension> where TDimension: Dimension
    {
        public override void RegisterPhases()
        {
            AddPhase<TilePhase, Dimension>();
            AddPhase<TileObjectDataPhase, Dimension>();
            AddPhase<ChestPhase, Dimension>();

            AddPhase<TileFramePhase, Dimension>();
        }
    }
}