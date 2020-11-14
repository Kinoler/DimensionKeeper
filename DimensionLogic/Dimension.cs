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

        public Tile[,] Tiles { get; set; } = new Tile[0, 0];
        public Chest[] Chests { get; set; } = new Chest[0];

        public int Width => Tiles?.GetLength(0) ?? 0;
        public int Height => Tiles?.GetLength(1) ?? 0;

        public void CopyFrom(Dimension dimension)
        {
            this.Location = dimension.Location;
            this.Tiles = dimension.Tiles;
            this.Chests = dimension.Chests;
        }
    }
}
