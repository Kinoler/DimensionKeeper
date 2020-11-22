using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.Configuration;
using Terraria;

namespace DimensionKeeper.HelperImplementations.Phases
{
    public class TileFramePhase: DimensionPhase<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var updateExtended = 3;
            var locationToLoad = entity.Location;

            WorldGen.RangeFrame(
                locationToLoad.X - updateExtended,
                locationToLoad.Y - updateExtended,
                locationToLoad.X + entity.Width + updateExtended,
                locationToLoad.Y + entity.Height + updateExtended);

        }

        public override void ExecuteClearPhase(DimensionEntity<Dimension> entity)
        {
            var updateExtended = 1;
            var locationToLoad = entity.Location;

            WorldGen.RangeFrame(
                locationToLoad.X - updateExtended,
                locationToLoad.Y - updateExtended,
                locationToLoad.X + entity.Width + updateExtended,
                locationToLoad.Y + entity.Height + updateExtended);
        }
    }
}
