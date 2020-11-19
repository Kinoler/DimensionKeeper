using DimensionKeeper.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DimensionKeeper.Tiles
{
	public class ExampleChair : ModTile
	{
		public override void SetDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.addTile(Type);
		}

        public override bool NewRightClick(int x, int y)
        {
			if (EyeDropperUpdater.Instance.PaintTooltipUI.Visible)
            {
                EyeDropperUpdater.Instance.PaintTooltipUI.Hide();
            }
            else
            {
                EyeDropperUpdater.Instance.PaintTooltipUI.Show();
            }
            return false;
        }

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(i * 16, j * 16, 16, 32, ModContent.ItemType<global::DimensionKeeper.Items.ExampleChair>());
		}
	}
}