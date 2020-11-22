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
            tile.frameX = tag.Get<short>(nameof(tile.frameX));
            tile.frameY = tag.Get<short>(nameof(tile.frameY));
            tile.type = tag.Get<ushort>(nameof(tile.type));
            tile.wall = tag.Get<ushort>(nameof(tile.wall));
            tile.sTileHeader = tag.Get<ushort>(nameof(tile.sTileHeader));
            tile.bTileHeader = tag.Get<byte>(nameof(tile.bTileHeader));
            tile.bTileHeader2 = tag.Get<byte>(nameof(tile.bTileHeader2));
            tile.bTileHeader3 = tag.Get<byte>(nameof(tile.bTileHeader3));
            tile.liquid = tag.Get<byte>(nameof(tile.liquid));

            return tile;
        }
    }
}
