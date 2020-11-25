using System;
using System.Xml;
using DimensionKeeper.DimensionService;
using DimensionKeeper.DimensionService.InternalClasses;
using DimensionKeeper.HelperImplementations.Storages;
using DimensionKeeper.Interfaces;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DimensionKeeper
{
    public class DimensionKeeperModWorld: ModWorld
    {
        private const string DimensionListTagName = "DimensionList";
        private const string SingleEntriesTagName = "SingleEntries";

        public override void Initialize()
        {
            SingleEntryFactory.Instance = null;
        }

        public override void Load(TagCompound tag)
        {
            SingleEntryFactory.Instance = tag.Get<SingleEntryFactory>(SingleEntriesTagName);
            LoadTagCompoundStorage(tag.GetCompound(DimensionListTagName));

            SingleEntryFactory.Instance.Load();

            foreach (var entry in SingleEntryFactory.Instance.SingleEntryDimensions)
            {
                var entity = entry.Value.CurrentEntity;
                entry.Value.CurrentEntity = null;

                entry.Value.LoadDimension(entity.Type, entity.Id);
            }
        }

        public override TagCompound Save()
        {
            foreach (var entry in SingleEntryFactory.Instance.SingleEntryDimensions)
            {
                var entity = entry.Value.CurrentEntity;
                entry.Value.ClearDimension();
                entry.Value.CurrentEntity = entity;
            }

            return new TagCompound
            {
                {DimensionListTagName, SaveTagCompoundStorage()},
                {SingleEntriesTagName, SingleEntryFactory.Instance},
            };
        }

        private TagCompound SaveTagCompoundStorage()
        {
            var dimensionsTag = new TagCompound();
            foreach (var storage in DimensionHelpers.RegisteredDimension.Stores)
            {
                dimensionsTag.Add(storage.Key, (storage.Value as ITagCompoundStorage)?.SavedDimensionsTag);
            }

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
