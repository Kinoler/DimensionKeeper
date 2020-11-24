using System;
using DimensionKeeper.DimensionService.InternalClasses;
using DimensionKeeper.PacketHandlers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// The entry which allow handle single dimension in the world.
    /// </summary>
    public class SingleEntryDimension
    {
        /// <summary>
        /// The name of this entry.
        /// </summary>
        public string EntryName { get; internal set; }

        /// <summary>
        /// Contains data of current loaded dimension.
        /// </summary>
        public DimensionEntity CurrentEntity { get; internal set; }

        /// <summary>
        /// Specify the dimension loading tile. Points to the down left corner.
        /// </summary>
        public Point LocationToLoad { get; set; }

        /// <summary>
        /// Same with <see cref="LoadDimension"/>.
        /// But also works with the client/server. 
        /// </summary>
        public void LoadDimensionNet(string type, string id = default, bool synchronizePrevious = true)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (Main.netMode != NetmodeID.SinglePlayer)
                SingleEntryPacketHandler.Instance.SendLoadDimension(-1, -1, EntryName, LocationToLoad.X, LocationToLoad.Y, type, id ?? type, synchronizePrevious);
            
            LoadDimension(type, id, synchronizePrevious);
        }

        /// <summary>
        /// Same with <see cref="SynchronizeDimension"/>.
        /// But also works with the client/server. 
        /// </summary>
        public void SynchronizeDimensionNet()
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
                SingleEntryPacketHandler.Instance.SendSynchronizeDimension(-1, -1, EntryName);

            SynchronizeDimension();
        }

        /// <summary>
        /// Same with <see cref="ClearDimension"/>.
        /// But also works with the client/server. 
        /// </summary>
        public void ClearDimensionNet(bool synchronizePrevious = true)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
                SingleEntryPacketHandler.Instance.SendClearDimension(-1, -1, EntryName, synchronizePrevious);

            ClearDimension(synchronizePrevious);
        }

        /// <summary>
        /// Load (inject) dimension into the world. Set the <see cref="LocationToLoad"/> to specify loading position.
        /// </summary>
        /// <param name="type">The type of the dimension.</param>
        /// <param name="id">The identifier for the dimension. By default will be equals to the type.</param>
        /// <param name="synchronizePrevious">Should be the previous dimension synchronized with changing in the world.</param>
        public void LoadDimension(string type, string id = default, bool synchronizePrevious = true)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            ClearDimension(synchronizePrevious);

            CurrentEntity = DimensionRegister.Instance.GetStorage(type).LoadInternal(id);
            CurrentEntity.Location = new Point(LocationToLoad.X, LocationToLoad.Y - CurrentEntity.Height);

            DimensionHelpers.LoadDimension(CurrentEntity);
        }

        /// <summary>
        /// Synchronizes current loaded dimension.
        /// Directly call is might be useful for very specific cases, usually <see cref = "LoadDimension" /> is sufficient.
        /// </summary>
        public void SynchronizeDimension()
        {
            DimensionHelpers.SynchronizeDimension(CurrentEntity);
        }

        /// <summary>
        /// Clears current loaded dimension.
        /// </summary>
        /// <param name="synchronizePrevious">Should be the previous dimension synchronized with changing in the world.</param>
        public void ClearDimension(bool synchronizePrevious = true)
        {
            if (synchronizePrevious)
                SynchronizeDimension();

            DimensionHelpers.ClearDimension(CurrentEntity);

            CurrentEntity = null;
        }
    }
}