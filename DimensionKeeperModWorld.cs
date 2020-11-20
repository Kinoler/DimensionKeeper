using System.Collections.Generic;
using System.Linq;
using DimensionKeeper.DimensionExample;
using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.InternalClasses;
using DimensionKeeper.HelperImplementations.Storages;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DimensionKeeper
{
    public class DimensionKeeperModWorld: ModWorld
    {
        private const string DimensionListTagName = "DimensionList";
        private const string SingleEntriesTagName = "SingleEntries";

        //TODO Move it to another project
        public override void Initialize()
        {
            DimensionStorageExample.Initialize();
        }

        public override void Load(TagCompound tag)
        {
            SingleEntryFactory.Instance = tag.Get<SingleEntryFactory>(SingleEntriesTagName);
            LoadTagCompoundStorage(tag.GetCompound(DimensionListTagName));
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {SingleEntriesTagName, SingleEntryFactory.Instance},
                {DimensionListTagName, SaveTagCompoundStorage()}
            };
        }

        private TagCompound SaveTagCompoundStorage()
        {
            var dimensionsTag = new TagCompound();
            foreach (var storage in DimensionHelpers.RegisteredDimension.Stores)
                dimensionsTag.Add(storage.Key, (storage.Value as ITagCompoundStorage)?.SavedDimensionsTag);

            return dimensionsTag;
        }

        private void LoadTagCompoundStorage(TagCompound tag)
        {
            foreach (var storage in DimensionHelpers.RegisteredDimension.Stores)
            {
                if (storage.Value is ITagCompoundStorage tagCompoundParser)
                {
                    tagCompoundParser.SavedDimensionsTag = tag.GetCompound(storage.Key);
                }
            }
        }
    }
}
