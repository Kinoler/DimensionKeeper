using Microsoft.Xna.Framework;
using Terraria;
using TestMod.Helpers;

namespace TestMod.DimensionLogic.DefaultPhases
{
    public class TileFramePhase: DimensionPhases<Dimension>
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

            foreach (var point in entity.RectangularPoints(new Point(-1, -1)))
            {
                    WorldGen.TileFrame(point.X, point.Y);
            }
        }
    }
}
