﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace TestMod.DimensionLogic.DefaultPhases
{
    public class TilePhase : DimensionPhases<Dimension>
    {
        public override void ExecuteLoadPhase(Dimension dimension)
        {
            var locationToLoad = dimension.LocationToLoad;

            for (var y = 0; y < dimension.Height; y++)
            {
                for (var x = 0; x < dimension.Width; x++)
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

        public override void ExecuteSynchronizePhase(Dimension dimension)
        { 
            var currentDimension = dimension;
            var locationToLoad = currentDimension.LocationToLoad;

            var minX = locationToLoad.X;
            var maxX = locationToLoad.X + currentDimension.Width;  
            var minY = locationToLoad.Y;
            var maxY = locationToLoad.Y + currentDimension.Height;

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

        public override void ExecuteClearPhase(Dimension dimension)
        {
            var locationToLoad = dimension.LocationToLoad;

            for (var y = 0; y < dimension.Height; y++)
            {
                for (var x = 0; x < dimension.Width; x++)
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