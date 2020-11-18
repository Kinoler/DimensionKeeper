using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TestMod.DimensionLogic;
using TestMod.UI;

namespace TestMod
{
	public class DimensionKeeperMod : Mod
    {
        public bool EnableEyeDropper { get; set; }

        public override void Load()
        {
            EnableEyeDropper = true;
        }

        public override void Unload()
        {
            DimensionKeeper.Instance = null;
            DimensionsRegister.Instance = null;
            EyeDropperUpdater.Instance = null;

            DimensionKeeperModWorld.DimensionsTag = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (!EnableEyeDropper)
                return;

            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex == -1) 
                return;

            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "DimensionKeeper: Mouse State Updater",
                delegate
                {
                    EyeDropperUpdater.Instance.UpdateMouseState();
                    return true;
                },
                InterfaceScaleType.UI)
            );

            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "DimensionKeeper: Paint Eye Dropper",
                delegate
                {
                    EyeDropperUpdater.Instance.DrawUpdateEyeDropper();
                    return true;
                },
                InterfaceScaleType.Game)
            );
        }
	}
}