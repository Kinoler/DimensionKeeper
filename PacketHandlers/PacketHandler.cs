using System.IO;
using Terraria.ModLoader;

namespace DimensionKeeper.PacketHandlers
{
    internal abstract class PacketHandler
    {
        internal byte HandlerType { get; set; }

        public abstract void HandlePacket(BinaryReader reader, int fromWho);

        protected PacketHandler(byte handlerType)
        {
            HandlerType = handlerType;
        }

        protected ModPacket GetPacket(byte packetType, int fromWho)
        {
            var p = ModContent.GetInstance<DimensionKeeperMod>().GetPacket();
            p.Write(HandlerType);
            p.Write(packetType);

            return p;
        }
    }
}
