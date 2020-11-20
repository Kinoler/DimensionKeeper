using System;
using DimensionKeeper.DimensionExample;
using DimensionKeeper.DimensionService.InternalClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService
{
    public class SingleEntryDimension
    {
        /// <summary>
        /// Contains data of current loaded dimension.
        /// </summary>
        public DimensionEntity CurrentEntity { get; internal set; }

        /// <summary>
        /// Specify the dimension loading tile. Points to the down left corner.
        /// </summary>
        public Point LocationToLoad { get; set; }

        /// <summary>
        /// Load (inject) dimension into the world. Set the <see cref="LocationToLoad"/> to specify loading position.
        /// </summary>
        /// <param name="type">The type which dimension was registered.</param>
        /// <param name="id">The identifier for the dimension. By default equals to the type.</param>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
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
        /// Synchronizes the <see cref="CurrentEntity"/> dimension. Can be useful for very specific cases, usually <see cref = "LoadDimension" /> is sufficient.
        /// </summary>
        public void SynchronizeDimension()
        {
            DimensionHelpers.SynchronizeDimension(CurrentEntity);
        }

        /// <summary>
        /// Clear current loaded dimension.
        /// </summary>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        public void ClearDimension(bool synchronizePrevious = true)
        {
            if (synchronizePrevious)
                DimensionHelpers.SynchronizeDimension(CurrentEntity);

            DimensionHelpers.ClearDimension(CurrentEntity);

            CurrentEntity = null;
        }
    }
}