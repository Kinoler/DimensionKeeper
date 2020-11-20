using System.Collections.Generic;
using DimensionKeeper.DimensionExample;
using DimensionKeeper.DimensionService;
using DimensionKeeper.EyeDropperUI;
using DimensionKeeper.TagSerializers;
using DimensionKeeper.TagSerializers.Vanilla;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace DimensionKeeper
{
	public class DimensionKeeperMod : Mod
    {
        public const string EyeDropperTypeName = "EyeDropper";

        //TODO Change to false
        public bool EnableEyeDropper { get; set; } = true;

        public override void Load()
        {
            TagSerializer.AddSerializer(new ChestTagSerializer());
            TagSerializer.AddSerializer(new TileTagSerializer());
            TagSerializer.AddSerializer(new DimensionEntityTagSerializer());
            TagSerializer.AddSerializer(new DimensionsKeeperTagSerializer());
            TagSerializer.AddSerializer(new DimensionTagSerializer());
            TagSerializer.AddSerializer(new PointTagSerializer());
            TagSerializer.AddSerializer(new SingleEntryDimensionTagSerializer());
            TagSerializer.AddSerializer(new TileArrayTagSerializer());

            //TODO Move it to another project
            DimensionRegister.SetupDimensionTypesRegister<DimensionRegisterExample>();
        }

        public override void Unload()
        {
            SingleEntryFactory.Instance = null;
            DimensionRegister.Instance = null;
            EyeDropperUpdater.Instance = null;

            //TODO Move it to another project
            DimensionStorageExample.Dimensions = null;
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