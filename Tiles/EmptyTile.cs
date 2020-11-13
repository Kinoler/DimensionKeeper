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
            DimensionLoader.Load("", true);
            return false;
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            X = i;
            Y = j;
            return;
            var tile = Main.tile[i, j];
            var point = new Point(i, j);
            var width = 10;
            var height = 10;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (WorldGen.InWorld(x + point.X, y + point.Y))
                    {
                        var target = Framing.GetTileSafely(x + point.X, y + point.Y);
                        target.CopyFrom(tile);
                    }
                }
            }

            // TODO: Experiment with WorldGen.stopDrops = true;
            // TODO: Button to ignore TileFrame?
            for (var x = point.X; x < point.X + width; x++)
            {
                for (var y = 0; y < point.Y + height; y++)
                {
                    WorldGen.SquareTileFrame(x, y, false); // Need to do this after stamp so neighbors are correct.
                    if (Main.netMode == 1 && Framing.GetTileSafely(x, y).liquid > 0)
                    {
                        NetMessage.sendWater(x, y); // Does it matter that this is before sendtilesquare?
                    }
                }
            }
            if (Main.netMode == 1)
            {
                NetMessage.SendTileSquare(-1, point.X + width / 2, point.Y + height / 2, Math.Max(width, height));
            }
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
