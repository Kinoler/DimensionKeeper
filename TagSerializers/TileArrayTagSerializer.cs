using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            for (var i = 0; i < firstRankLength; i++)
            for (var j = 0; j < secondRangLength; j++)
                tileList.Add(tiles[i, j]);

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
            for (var i = 0; i < firstRankLength; i++)
            for (var j = 0; j < secondRangLength; j++)
                tiles[i, j] = tileList[firstRankLength * i + j];

            return tiles;
        }
    }
}
