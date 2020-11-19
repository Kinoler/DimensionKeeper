using Terraria.ModLoader.IO;

namespace DimensionKeeper.Interfaces
{
    internal interface ITagCompound
    {
        TagCompound Save();

        void Load(TagCompound tag);
    }
}
