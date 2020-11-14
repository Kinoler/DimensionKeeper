using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using TestMod.DimensionLogic;
using TestMod.Interfaces;

namespace TestMod.DimensionExample.Phases
{
    public class TilePhase : DimensionPhases<DimensionExample>
    {
        public override void ExecuteLoadPhase(DimensionExample dimension)
        {
            var locationPoint = dimension.Location;

            for (var x = 0; x < dimension.Width; x++)
            {
                for (var y = 0; y < dimension.Height; y++)
                {
                    var locationX = locationPoint.X + x;
                    var locationY = locationPoint.Y + y;
                    if (!WorldGen.InWorld(locationX, locationY))
                        continue;
                    var targetTile = Framing.GetTileSafely(locationX, locationY);
                    var dimensionTile = dimension.Tiles[x, y];
                    if (dimensionTile != null)
                        targetTile.CopyFrom(dimensionTile);
                    else
                        Main.tile[locationX, locationY] = null;
                }
            }

            var updateExtended = 3;
            for (var x = locationPoint.X - updateExtended; x < locationPoint.X + dimension.Width + updateExtended; x++)
            {
                for (var y = 0; y < locationPoint.Y + dimension.Height + updateExtended; y++)
                {
                    WorldGen.SquareTileFrame(x, y, false); // Need to do this after stamp so neighbors are correct.
                }
            }
        }

        public override void ExecuteSynchronizePhase(DimensionExample dimension)
        {
            var currentDimension = dimension;
            var loadCoordinate = dimension.Location;

            var newDimension = DataParserExample.CreateDimension(
                loadCoordinate.X,
                loadCoordinate.X + currentDimension.Width,
                loadCoordinate.Y,
                loadCoordinate.Y + currentDimension.Height
            );

            dimension.CopyFrom(newDimension);
        }

        public override void ExecuteClearPhase(DimensionExample dimension)
        {
            var emptyTiles = new Tile[dimension.Width, dimension.Height];
            for (var i = 0; i < emptyTiles.GetLength(0); i++)
            {
                for (var j = 0; j < emptyTiles.GetLength(1); j++)
                {
                    emptyTiles[i, j] = new Tile();
                }
            }

            var emptyDimension = new DimensionExample { Tiles = emptyTiles, Location = dimension.Location};
            ExecuteLoadPhase(emptyDimension);
        }
    }
}
