using DimensionKeeper.DimensionService;
using DimensionKeeper.HelperImplementations.Phases;

namespace DimensionKeeper.HelperImplementations.Injectors
{
    public class StandardInjector<TDimension> : DimensionInjector<TDimension> 
        where TDimension: Dimension
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