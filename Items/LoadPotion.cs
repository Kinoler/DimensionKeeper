using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TestMod.DimensionExample;

namespace TestMod.Items
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
