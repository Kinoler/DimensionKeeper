using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DimensionKeeper.DimensionService.InternalClasses;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.TagSerializers.Vanilla
{
    public class TileTagSerializer: TagSerializer<Tile, TagCompound>
    {
        public override TagCompound Serialize(Tile tile)
        {
            return new TagCompound()
            {
                {nameof(tile.frameX), tile.frameX != 0 ? (short?)tile.frameX : null},
                {nameof(tile.frameY), tile.frameY != 0 ? (short?)tile.frameY : null},
                {nameof(tile.type), tile.type != 0 ? (ushort?)tile.type : null},
                {nameof(tile.wall), tile.wall != 0 ? (ushort?)tile.wall : null},
                {nameof(tile.sTileHeader), tile.sTileHeader != 0 ? (ushort?)tile.sTileHeader : null},
                {nameof(tile.bTileHeader), tile.bTileHeader != 0 ? (byte?)tile.bTileHeader : null},
                {nameof(tile.bTileHeader2), tile.bTileHeader2 != 0 ? (byte?)tile.bTileHeader2 : null},
                {nameof(tile.bTileHeader3), tile.bTileHeader3 != 0 ? (byte?)tile.bTileHeader3 : null},
                {nameof(tile.liquid), tile.liquid != 0 ? (byte?)tile.liquid : null},
            };
        }

        public override Tile Deserialize(TagCompound tag)
        {
            var tile = new Tile();
            tile.frameX = tag.Get<short?>(nameof(tile.frameX)) ?? 0;
            tile.frameY = tag.Get<short?>(nameof(tile.frameY)) ?? 0;
            tile.type = tag.Get<ushort?>(nameof(tile.type)) ?? 0;
            tile.wall = tag.Get<ushort?>(nameof(tile.wall)) ?? 0;
            tile.sTileHeader = tag.Get<ushort?>(nameof(tile.sTileHeader)) ?? 0;
            tile.bTileHeader = tag.Get<byte?>(nameof(tile.bTileHeader)) ?? 0;
            tile.bTileHeader2 = tag.Get<byte?>(nameof(tile.bTileHeader2)) ?? 0;
            tile.bTileHeader3 = tag.Get<byte?>(nameof(tile.bTileHeader3)) ?? 0;
            tile.liquid = tag.Get<byte?>(nameof(tile.liquid)) ?? 0;

            return tile;
        }
    }
}
