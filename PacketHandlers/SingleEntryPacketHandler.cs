using System.IO;
using DimensionKeeper.DimensionService;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DimensionKeeper.PacketHandlers
{
    internal class SingleEntryPacketHandler: PacketHandler
    {
        public const byte LoadDimension = 1;
        public const byte SynchronizeDimension = 2;
        public const byte ClearDimension = 3;

        private static SingleEntryPacketHandler _instance;

        internal static SingleEntryPacketHandler Instance
        {
            get => _instance ?? (_instance = new SingleEntryPacketHandler((byte)ModMessageType.SingleEntryDimensionOperation));
            set => _instance = value;
        }

        private SingleEntryPacketHandler(byte handlerType) : base(handlerType)
        {
        }

        public override void HandlePacket(BinaryReader reader, int fromWho)
        {
            switch (reader.ReadByte())
            {
                case (LoadDimension):
                    OnLoadDimension(reader, fromWho);
                    break;
                case (SynchronizeDimension):
                    OnSynchronizeDimension(reader, fromWho);
                    break;
                case (ClearDimension):
                    OnClearDimension(reader, fromWho);
                    break;
            }
        }

        public void SendLoadDimension(int toWho, int fromWho, string entryName, int locationToLoadX, 
            int locationToLoadY, string type, string id, bool synchronizePrevious)
        {
            var packet = GetPacket(LoadDimension, fromWho);
            packet.Write(entryName);
            packet.Write(locationToLoadX);
            packet.Write(locationToLoadY);
            packet.Write(type);
            packet.Write(id);
            packet.Write(synchronizePrevious);

            packet.Send(toWho, fromWho);
        }

        public void SendSynchronizeDimension(int toWho, int fromWho, string entryName)
        {
            var packet = GetPacket(SynchronizeDimension, fromWho);
            packet.Write(entryName);

            packet.Send(toWho, fromWho);
        }

        public void SendClearDimension(int toWho, int fromWho, string entryName, bool synchronizePrevious)
        {
            var packet = GetPacket(ClearDimension, fromWho);
            packet.Write(entryName);
            packet.Write(synchronizePrevious);

            packet.Send(toWho, fromWho);
        }

        private void OnLoadDimension(BinaryReader reader, int fromWho)
        {
            var entryName = reader.ReadString();
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            var type = reader.ReadString();
            var id = reader.ReadString();
            var synchronizePrevious = reader.ReadBoolean();

            if (Main.netMode == NetmodeID.Server)
                SendLoadDimension(-1, fromWho, entryName, x, y, type, id, synchronizePrevious);

            SingleEntryFactory.GetEntry(entryName, new Point(x, y))
                .LoadDimension(type, id, synchronizePrevious);
        }

        private void OnSynchronizeDimension(BinaryReader reader, int fromWho)
        {
            var entryName = reader.ReadString();

            if (Main.netMode == NetmodeID.Server)
                SendSynchronizeDimension(-1, fromWho, entryName);

            SingleEntryFactory.GetEntry(entryName)
                .SynchronizeDimension();
        }

        private void OnClearDimension(BinaryReader reader, int fromWho)
        {
            var entryName = reader.ReadString();
            var synchronizePrevious = reader.ReadBoolean();

            if (Main.netMode == NetmodeID.Server)
                SendClearDimension(-1, fromWho, entryName, synchronizePrevious);

            SingleEntryFactory.GetEntry(entryName)
                .ClearDimension(synchronizePrevious);
        }
    }
}
