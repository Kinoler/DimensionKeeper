using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers
{
    public class TileArrayTagSerializer: TagSerializer<Tile[,], TagCompound>
    {
        public override TagCompound Serialize(Tile[,] tiles)
        {
            var tileList = new List<Tile>();
            var firstRankLength = tiles.GetLength(0);
            var secondRangLength = tiles.GetLength(1);

            for (var x = 0; x < firstRankLength; x++)
            for (var y = 0; y < secondRangLength; y++)
                tileList.Add(tiles[x, y]);

            return new TagCompound()
            {
                {"tileList",  tileList},
                {"firstRankLength",  firstRankLength},
            };
        }

        public override Tile[,] Deserialize(TagCompound tag)
        {
            var tileList = tag.GetList<Tile>("tileList");
            var firstRankLength = tag.GetInt("firstRankLength");

            var secondRangLength = tileList.Count / firstRankLength;

            var tiles = new Tile[firstRankLength, secondRangLength];
            for (var x = 0; x < firstRankLength; x++)
            for (var y = 0; y < secondRangLength; y++)
                tiles[x, y] = tileList[secondRangLength * x + y];

            return tiles;
        }
    }
}
