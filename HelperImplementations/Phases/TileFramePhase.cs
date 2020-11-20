using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.Configuration;
using Microsoft.Xna.Framework;
using Terraria;

namespace DimensionKeeper.HelperImplementations.Phases
{
    public class TileFramePhase: DimensionPhase<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var updateExtended = 3;

            for (var x = locationToLoad.X - updateExtended; x < locationToLoad.X + entity.Width + updateExtended; x++)
            {
                for (var y = 0; y < locationToLoad.Y + entity.Height + updateExtended; y++)
                {
                    WorldGen.TileFrame(x, y);
                }
            }
        }

        public override void ExecuteClearPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var offsetX = -1;
            var offsetY = -1;

            for (var x = locationToLoad.X + offsetX; x < locationToLoad.X + entity.Width - offsetX; x++)
            {
                for (var y = locationToLoad.Y + offsetY; y < locationToLoad.Y + entity.Height - offsetY; y++)
                {
                    WorldGen.TileFrame(x, y);
                }
            }
        }
    }
}
