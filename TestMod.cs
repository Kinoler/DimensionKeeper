using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.DimensionExample;
using TestMod.DimensionLogic;
using TestMod.Globals;

namespace TestMod
{
	public class TestMod : Mod
    {
        public override void AddRecipeGroups()
        {
            GetGlobalItem<AllUIUpdater>().Initialize();
        }

        public override void Load()
		{
            DimensionsRegister.SetupDimensionTypesRegister<DimensionRegisterExample>();
        }

        public override void Unload()
        {
            DimensionKeeper.Instance = null;
            DimensionsRegister.Instance = null;
            DataParserExample.Dimensions = null;
            TestWorldMod.DimensionsTag = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: All UI",
                    delegate
                    {
                        GetGlobalItem<AllUIUpdater>().DrawUpdateAll(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: Paint Tooltip",
                    delegate
                    {
                        GetGlobalItem<AllUIUpdater>().DrawUpdatePaintTools(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }
	}
}