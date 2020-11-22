using System.IO;
using DimensionKeeper.DimensionService;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DimensionKeeper.PacketHandlers
{
    internal class PlayerJoinPacketHandler: PacketHandler
    {
        public const byte SyncStoragesAndEntries = 1;

        private static PlayerJoinPacketHandler _instance;

        internal static PlayerJoinPacketHandler Instance
        {
            get => _instance ?? (_instance = new PlayerJoinPacketHandler((byte)ModMessageType.OnPlayerJoin));
            set => _instance = value;
        }

        private PlayerJoinPacketHandler(byte handlerType) : base(handlerType)
        {
        }

		public override void HandlePacket(BinaryReader reader, int fromWho)
        {
            switch (reader.ReadByte())
            {
                case (SyncStoragesAndEntries):
                    OnSyncStoragesAndEntries(reader, fromWho);
                    break;
            }
        }

        public void SendSyncStoragesAndEntries()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                var packet = GetPacket(SyncStoragesAndEntries, Main.myPlayer);
                packet.Send();
            }
        }

        private void OnSyncStoragesAndEntries(BinaryReader reader, int fromWho)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                var packet = GetPacket(SyncStoragesAndEntries, Main.myPlayer);
                foreach (var storage in DimensionRegister.Instance.Stores.Values)
                    storage.SendInternal(packet);

                packet.Write(SingleEntryFactory.Instance.SingleEntryDimensions.Values.Count);
                foreach (var entry in SingleEntryFactory.Instance.SingleEntryDimensions.Values)
                {
                    packet.Write(entry.EntryName);
                    packet.Write(entry.LocationToLoad.X);
                    packet.Write(entry.LocationToLoad.Y);
                    packet.Write(entry.CurrentEntity.Width);
                    packet.Write(entry.CurrentEntity.Height);
                    packet.Write(entry.CurrentEntity.Type);
                    packet.Write(entry.CurrentEntity.Id);
                }

                packet.Send(fromWho);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                foreach (var storage in DimensionRegister.Instance.Stores.Values)
                    storage.ReceiveInternal(reader);

                var count = reader.ReadInt32();
                for (var i = 0; i < count; i++)
                {
                    var entryName = reader.ReadString();
                    var locationX = reader.ReadInt32();
                    var locationY = reader.ReadInt32();
                    var width = reader.ReadInt32();
                    var height = reader.ReadInt32();
                    var type = reader.ReadString();
                    var id = reader.ReadString();

                    var entry = SingleEntryFactory.CreateNewEntry(
                        type,
                        new Point(width, height),
                        new Point(locationX, locationY),
                        entryName);

                    entry.CurrentEntity.Id = id;
                }
            }
        }
    }
}
