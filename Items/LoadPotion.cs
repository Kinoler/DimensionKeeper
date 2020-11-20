using DimensionKeeper.DimensionExample;
using DimensionKeeper.DimensionService;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DimensionKeeper.Items
{
    public class LoadPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives a light defense buff.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = ItemRarityID.Orange;
        }

        public override bool UseItem(Player player)
        {
            var entry = SingleEntryFactory.GetEntry("SomeEntry");
            entry.ClearDimension();
            DimensionStorageExample.NextDimension();
            return true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
