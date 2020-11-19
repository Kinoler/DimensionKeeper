﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace DimensionKeeper.DimensionService.DefaultPhases
{
    public class TileObjectDataPhase: DimensionPhases<Dimension>
    {
        public override void ExecuteLoadPhase(DimensionEntity<Dimension> entity)
        {
            var locationToLoad = entity.Location;
            var dimension = entity.Dimension;

            var checkedPoints = new List<Point>();
            for (var y = 0; y < entity.Height; y++)
            {
                for (var x = 0; x < entity.Width; x++)
                {
                    if (checkedPoints.Contains(new Point(x, y)))
                        continue;

                    var worldX = locationToLoad.X + x;
                    var worldY = locationToLoad.Y + y;

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
        }
    }
}
