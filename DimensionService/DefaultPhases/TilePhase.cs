using Terraria;
using Terraria.ObjectData;

namespace DimensionKeeper.DimensionService.DefaultPhases
{
    public class TilePhase : DimensionPhases<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var dimension = entity.Dimension;

            for (var y = 0; y < entity.Height; y++)
            {
                for (var x = 0; x < entity.Width; x++)
                {
                    var worldX = locationToLoad.X + x;
                    var worldY = locationToLoad.Y + y;

                    if (!WorldGen.InWorld(worldX, worldY))
                        continue;
                    var targetTile = Framing.GetTileSafely(worldX, worldY);
                    targetTile.ClearEverything();

                    var dimensionTile = dimension.Tiles[x, y];
                    var dimensionTileData = TileObjectData.GetTileData(dimensionTile);
                    if (dimensionTileData == null) 
                        targetTile.CopyFrom(dimensionTile);
                }
            }
        }

        public override void ExecuteSynchronizePhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;

            var minX = locationToLoad.X;
            var maxX = locationToLoad.X + entity.Width;  
            var minY = locationToLoad.Y;
            var maxY = locationToLoad.Y + entity.Height;

            var stampTiles = new Tile[maxX - minX, maxY - minY];

            for (var i = 0; i < maxX - minX; i++)
            {
                for (var j = 0; j < maxY - minY; j++)
                {
                    stampTiles[i, j] = new Tile();
                }
            }

            for (var x = minX; x < maxX; x++)
            {
                for (var y = minY; y < maxY; y++)
                {
                    if (WorldGen.InWorld(x, y))
                    {
                        if (Main.tile[x, y].active()) 
                            WorldGen.TileFrame(x, y);

                        if (Main.tile[x, y].wall > 0) 
                            Framing.WallFrame(x, y);

                        var target = Framing.GetTileSafely(x, y);
                        stampTiles[x - minX, y - minY].CopyFrom(target);
                    }
                }
            }

            entity.Dimension.Tiles = stampTiles;
        }

        public override void ExecuteClearPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;

            for (var y = 0; y < entity.Height; y++)
            {
                for (var x = 0; x < entity.Width; x++)
                {
                    var worldX = locationToLoad.X + x;
                    var worldY = locationToLoad.Y + y;

                    if (!WorldGen.InWorld(worldX, worldY))
                        continue;

                    var targetTile = Framing.GetTileSafely(worldX, worldY);
                    targetTile.ClearEverything();
                }
            }
        }
    }
}
