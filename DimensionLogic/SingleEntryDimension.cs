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
    public class SingleEntryDimension: ITagCompound
    {
        private DimensionsRegister RegisteredDimension => DimensionsRegister.Instance;

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
        /// <param name="id">The identifier for the dimension.</param>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        public void LoadDimension(string type, string id = default, bool synchronizePrevious = true)
        {
            id = id ?? type;

            if (synchronizePrevious)
                DimensionLoader.SynchronizeDimension(CurrentEntity);

            DimensionLoader.ClearDimension(CurrentEntity);

            CurrentEntity = RegisteredDimension.GetParser(type).GetDimension(id);
            CurrentEntity.Location = new Point(LocationToLoad.X, LocationToLoad.Y - CurrentEntity.Height);

            DimensionLoader.LoadDimension(CurrentEntity);
        }

        TagCompound ITagCompound.Save()
        {
            if (!DimensionLoader.ValidateDimension(CurrentEntity))
                return null;

            var tag = new TagCompound
            {
                {"Type", CurrentEntity?.Type},
                {"Id", CurrentEntity?.Id},
                {"Location", CurrentEntity?.Location.ToVector2()},
                {"Size", CurrentEntity != null ?
                    new Point(CurrentEntity.Width, CurrentEntity.Height).ToVector2() :
                    (object) null
                }
            };

            return tag;
        }

        void ITagCompound.Load(TagCompound tag)
        {
            var type = tag.Get<string>("Type");
            if (string.IsNullOrEmpty(type))
                return;

            var id = tag.Get<string>("Id");
            CurrentEntity = RegisteredDimension.GetParser(type).GetDimension(id);

            CurrentEntity.Location = tag.Get<Vector2>("Location").ToPoint();
            CurrentEntity.Size = tag.Get<Vector2>("Size").ToPoint();

            DimensionLoader.SynchronizeDimension(CurrentEntity);
        }
    }
}
