using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;
using static Terraria.ModLoader.ModContent;

namespace TestMod.Tiles
{
    public class EmptyTile : ModTile
	{
        private static int X { get; set; }
        private static int Y { get; set; }

		public override void SetDefaults()
		{
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
        }

        public override bool NewRightClick(int x, int y)
        {
            DataParserExample.SetLoadCoordinate((X > 0 ? X : x) + 10, Y > 0 ? Y : y);
            DimensionLoader.LoadDimension("");
            return false;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            X = i;
            Y = j;
            return;
        }

        public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Main.clock = true;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, ItemType<Items.ExampleClock>());
		}
	}
}
