using System.Collections.Generic;
using System.IO;
using DimensionKeeper.DimensionService;
using DimensionKeeper.EyeDropperUI;
using DimensionKeeper.PacketHandlers;
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

        public static void LogMessage(string msg)
        {
            ModContent.GetInstance<DimensionKeeperMod>().Logger.InfoFormat(msg);
        }

        public bool EnableEyeDropper { get; set; } = false;

        public override void Load()
        {
            TagSerializer.AddSerializer(new ChestTagSerializer());
            TagSerializer.AddSerializer(new TileTagSerializer());
            TagSerializer.AddSerializer(new DimensionEntityTagSerializer());
            TagSerializer.AddSerializer(new SingleEntryFactoryTagSerializer());
            TagSerializer.AddSerializer(new DimensionTagSerializer());
            TagSerializer.AddSerializer(new PointTagSerializer());
            TagSerializer.AddSerializer(new SingleEntryDimensionTagSerializer());
            TagSerializer.AddSerializer(new TileArrayTagSerializer());
        }

        public override void Unload()
        {
            SingleEntryFactory.Instance = null;
            DimensionRegister.Instance = null;
            EyeDropperUpdater.Instance = null;

            PlayerJoinPacketHandler.Instance = null;
            SingleEntryPacketHandler.Instance = null;
        }

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
            var msgType = (ModMessageType)reader.ReadByte();

			switch (msgType)
			{
                case ModMessageType.SingleEntryDimensionOperation:
                    SingleEntryPacketHandler.Instance.HandlePacket(reader, whoAmI);
					break;
                case ModMessageType.OnPlayerJoin:
                    PlayerJoinPacketHandler.Instance.HandlePacket(reader, whoAmI);
                    break;
                default:
					Logger.WarnFormat("ExampleMod: Unknown Message type: {0}", msgType);
					break;
			}
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

    internal enum ModMessageType : byte
    {
        SingleEntryDimensionOperation,
        OnPlayerJoin,
    }

}