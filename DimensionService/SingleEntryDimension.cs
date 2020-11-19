using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic.InternalHelperClasses;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
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

            CurrentEntity = DimensionsRegister.Instance.GetStorage(type).LoadInternal(id ?? type);
            CurrentEntity.Location = new Point(LocationToLoad.X, LocationToLoad.Y - CurrentEntity.Height);

            DimensionLoader.LoadDimension(CurrentEntity);
        }

        /// <summary>
        /// Clear current loaded dimension.
        /// </summary>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        public void ClearDimension(bool synchronizePrevious = true)
        {
            if (synchronizePrevious)
                DimensionLoader.SynchronizeDimension(CurrentEntity);

            DimensionLoader.ClearDimension(CurrentEntity);
        }

        internal TagCompound Save()
        {
            if (!DimensionLoader.ValidateDimension(CurrentEntity))
                return null;

            return new TagCompound
            {
                {nameof(CurrentEntity.Type), CurrentEntity.Type},
                {nameof(CurrentEntity.Id), CurrentEntity.Id},
                {nameof(CurrentEntity.Location), CurrentEntity.Location.ToVector2()},
                {nameof(CurrentEntity.Size), CurrentEntity.Size.ToVector2()},
                {nameof(LocationToLoad), LocationToLoad.ToVector2()}
            };
        }

        internal void Load(TagCompound tag)
        {
            var type = tag.Get<string>(nameof(CurrentEntity.Type));
            var id = tag.Get<string>(nameof(CurrentEntity.Id));
            var location = tag.Get<Vector2>(nameof(CurrentEntity.Location)).ToPoint();
            var size = tag.Get<Vector2>(nameof(CurrentEntity.Size)).ToPoint();

            CurrentEntity = DimensionsRegister.Instance.GetStorage(type).LoadInternal(id);
            CurrentEntity.Location = location;
            CurrentEntity.Size = size;

            //DimensionLoader.SynchronizeDimension(CurrentEntity);
        }
    }
}
