using System.Runtime.Serialization;
using Terraria;

namespace DimensionKeeper.DimensionService
{
    public class Dimension
    {
        public Tile[,] Tiles { get; set; } = new Tile[0, 0];
        public Chest[] Chests { get; set; } = new Chest[0];

        public int Width => Tiles?.GetLength(0) ?? 0;
        public int Height => Tiles?.GetLength(1) ?? 0;

        public void CopyFrom(Dimension dimension)
        {
            this.Tiles = dimension.Tiles;
            this.Chests = dimension.Chests;
        }
    }
}
