using Terraria.ID;
using Terraria.ModLoader;

namespace DimensionKeeper.Items
{
	public class ExampleChair : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This is a modded chair.");
		}

        public override void SetDefaults() {
			item.width = 12;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 150;
			item.createTile = ModContent.TileType<Tiles.ExampleChair>();
		}

        public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}