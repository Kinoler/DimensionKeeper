using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TestMod.DimensionLogic;
using TestMod.Helpers;

namespace TestMod.DimensionExample
{
    public class DataParserExample : DataParser<DimensionExample>
    {
        internal static Point LoadCoordinate { get; set; }
        internal static List<DimensionExample> Dimensions { get; } = new List<DimensionExample>();
        internal static CycledCounter Counter { get; } = new CycledCounter();
        internal static int CurrentLoadedDimension { get; set; } = -1;

        public override bool AlwaysNew => true;

        public static void SetLoadCoordinate(int x, int y)
        {
            LoadCoordinate = new Point(x, y);
        }

        public static void AddDimension(DimensionExample dimension)
        {
            Dimensions.Add(dimension);
            Counter.AddNew();
        }

        public static int NextDimension()
        {
            var current = Counter.Next();
            Main.NewText($"Current dimension {current + 1}/{Counter.Max}", Color.Azure, false);
            return current;
        }

        public override DimensionExample Load()
        {
            if (Counter.Max == 0)
                return null;

            CurrentLoadedDimension = Counter.Current;
            var dimension = Dimensions[Counter.Current];
            dimension.Location = new Point(LoadCoordinate.X, LoadCoordinate.Y - dimension.Height);
            return dimension;
        }

        public override void Save(DimensionExample dimension)
        {
            if (Counter.Max == 0)
                return;

            if (CurrentLoadedDimension == -1)
                return;

            var currentDimension = dimension;

            var newDimension = CreateDimension(
                LoadCoordinate.X,
                LoadCoordinate.X + currentDimension.Width,
                LoadCoordinate.Y - currentDimension.Height,
                LoadCoordinate.Y
            );

            UpdateDimension(CurrentLoadedDimension, newDimension);
        }

        public static void UpdateDimension(int num, DimensionExample dimension)
        {
            Dimensions[num] = dimension;
        }

        public static DimensionExample CreateDimension(int minX, int maxX, int minY, int maxY)
        {
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
                        if (Main.tile[x, y].type == TileID.Count)
                        {
                        }

                        if (Main.tile[x, y].active())
                        {
                            WorldGen.TileFrame(x, y, true, false);
                        }

                        if (Main.tile[x, y].wall > 0)
                        {
                            Framing.WallFrame(x, y, true);
                        }

                        var target = Framing.GetTileSafely(x, y);
                        stampTiles[x - minX, y - minY].CopyFrom(target);
                        if (Main.tile[x, y].type == TileID.Count)
                            stampTiles[x - minX, y - minY].ClearTile();
                        if (Main.tileContainer[Main.tile[x, y].type])
                            stampTiles[x - minX, y - minY].ClearTile();
                    }
                }
            }

            return new DimensionExample(){Tiles = stampTiles};
        }
    }
}
