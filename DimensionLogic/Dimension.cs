using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using TestMod.DimensionExample;

namespace TestMod.DimensionLogic
{
    public class Dimension
    {
        public Point Location { get; set; }

        public Tile[,] Tiles { get; set; }
        public Chest[] Chests { get; set; }


        public int Width => Tiles?.GetLength(0) ?? 0;
        public int Height => Tiles?.GetLength(1) ?? 0;

        public void CopyFrom(Dimension dimension)
        {
            this.Tiles = dimension.Tiles;
            this.Chests = dimension.Chests;
        }
    }
}
