using DimensionKeeper.Interfaces;
using Terraria;

namespace DimensionKeeper.DimensionService.Configuration
{
    public class Dimension: IDimension
    {
        public Tile[,] Tiles { get; set; } = new Tile[0, 0];
        public Chest[] Chests { get; set; } = new Chest[0];

        public NPC[] NPCs { get; set; } = new NPC[0];
        public int[] NPCIndexes { get; set; } = new int[0];

        public int Width => Tiles?.GetLength(0) ?? 0;
        public int Height => Tiles?.GetLength(1) ?? 0;

        public void CopyFrom(Dimension dimension)
        {
            this.Tiles = dimension.Tiles;
            this.Chests = dimension.Chests;
            this.NPCs = dimension.NPCs;
            this.NPCIndexes = dimension.NPCIndexes;
        }
    }
}
