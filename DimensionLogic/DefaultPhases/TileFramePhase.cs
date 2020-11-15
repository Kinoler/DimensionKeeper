using Terraria;

namespace TestMod.DimensionLogic.DefaultPhases
{
    public class TileFramePhase: DimensionPhases<Dimension>
    {
        public override void ExecuteLoadPhase(Dimension dimension)
        {
            var locationToLoad = dimension.LocationToLoad;
            var updateExtended = 3;

            for (var x = locationToLoad.X - updateExtended; x < locationToLoad.X + dimension.Width + updateExtended; x++)
            {
                for (var y = 0; y < locationToLoad.Y + dimension.Height + updateExtended; y++)
                {
                    WorldGen.TileFrame(x, y);
                }
            }
        }
    }
}
