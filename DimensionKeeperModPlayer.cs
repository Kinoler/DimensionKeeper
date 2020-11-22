using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.PacketHandlers;
using Terraria;
using Terraria.ID;
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
