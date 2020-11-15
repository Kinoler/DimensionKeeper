using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;
using TestMod.DimensionLogic;
using TestMod.Interfaces;

namespace TestMod.DimensionExample.Phases
{
    public class TilePhase : DimensionPhases<DimensionExample>
    {
        public override void ExecuteLoadPhase(DimensionExample dimension)
        {
            var locationPoint = dimension.LocationToLoad;

            for (var y = 0; y < dimension.Height; y++)
            {
                for (var x = 0; x < dimension.Width; x++)
                {
                    var worldX = locationPoint.X + x;
                    var worldY = locationPoint.Y + y;

                    if (!WorldGen.InWorld(worldX, worldY))
                        continue;
                    var targetTile = Framing.GetTileSafely(worldX, worldY);
                    targetTile.ClearTile();

                    var dimensionTile = dimension.Tiles[x, y];
                    var dimensionTileData = TileObjectData.GetTileData(dimensionTile);
                    if (dimensionTileData == null) 
                        targetTile.CopyFrom(dimensionTile);
                }
            }
            var checkedPoints = new List<Point>();
            for (var y = 0; y < dimension.Height; y++)
            {
                for (var x = 0; x < dimension.Width; x++)
                {
                    if (checkedPoints.Contains(new Point(x, y)))
                        continue;

                    var worldX = locationPoint.X + x;
                    var worldY = locationPoint.Y + y;

                    if (!WorldGen.InWorld(worldX, worldY))
                        continue;
                    var targetTile = Framing.GetTileSafely(worldX, worldY);

                    var dimensionTile = dimension.Tiles[x, y];
                    var dimensionTileData = TileObjectData.GetTileData(dimensionTile);
                    if (dimensionTileData != null)
                    {
                        var style = -1;
                        var alternate = -1;
                        TileObjectData.GetTileInfo(dimensionTile, ref style, ref alternate);

                        if (TileObject.CanPlace(worldX + dimensionTileData.Origin.X, worldY + dimensionTileData.Origin.Y, dimensionTile.type, style, alternate,
                            out var tileObject))
                        {
                            TileObject.Place(tileObject);

                            for (var j = 0; j < dimensionTileData.Height; j++)
                            {
                                for (var i = 0; i < dimensionTileData.Width; i++)
                                {
                                    checkedPoints.Add(new Point(x + i, y + j));
                                }
                            }
                        }
                    }
                }
            }

            /*
            var updateExtended = 3;
            for (var x = locationPoint.X - updateExtended; x < locationPoint.X + dimension.Width + updateExtended; x++)
            {
                for (var y = 0; y < locationPoint.Y + dimension.Height + updateExtended; y++)
                {
                    WorldGen.SquareTileFrame(x, y); // Need to do this after stamp so neighbors are correct.
                }
            }
            /*
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
            */
        }

        public override void ExecuteSynchronizePhase(DimensionExample dimension)
        { 
            var currentDimension = dimension;
            var loadCoordinate = currentDimension.LocationToLoad;

            var minX = loadCoordinate.X;
            var maxX = loadCoordinate.X + currentDimension.Width;  
            var minY = loadCoordinate.Y;
            var maxY = loadCoordinate.Y + currentDimension.Height;

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

            dimension.Tiles = stampTiles;
        }

        public override void ExecuteClearPhase(DimensionExample dimension)
        {
            var loadCoordinate = dimension.LocationToLoad;

            for (var y = 0; y < dimension.Height; y++)
            {
                for (var x = 0; x < dimension.Width; x++)
                {
                    var worldX = loadCoordinate.X + x;
                    var worldY = loadCoordinate.Y + y;

                    if (!WorldGen.InWorld(worldX, worldY))
                        continue;

                    var targetTile = Framing.GetTileSafely(worldX, worldY);
                    targetTile.ClearEverything();
                }
            }
        }
    }
}
