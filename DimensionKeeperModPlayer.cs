using DimensionKeeper.PacketHandlers;
using Terraria;
using Terraria.ModLoader;

namespace DimensionKeeper
{
    public class DimensionKeeperModPlayer: ModPlayer
    {
        public override void OnEnterWorld(Player otherPlayer)
        {
            PlayerJoinPacketHandler.Instance.SendSyncStoragesAndEntries();
        }
    }
}
