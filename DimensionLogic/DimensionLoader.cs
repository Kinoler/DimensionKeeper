using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic.InternalHelperClasses;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// The main class DimensionKeeper that allows you to manipulate dimensions.
    /// </summary>
    public static class DimensionLoader
    {
        internal static DimensionsRegister RegisteredDimension => DimensionsRegister.Instance;

        private static DimensionEntity CurrentEntity { get; set; }

        /// <summary>
        /// Register the open generic type which register the dimensions.
        /// </summary>
        /// <typeparam name="TDimensionRegister">The class which inherit from <see cref="IDimensionRegister"/>.</typeparam>
        public static void RegisterDimensions<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            RegisterDimensions(new TDimensionRegister());
        }

        /// <summary>
        /// Register the class which register the dimensions.
        /// </summary>
        /// <param name="register">The instance of class which inherit from <see cref="IDimensionRegister"/>.</param>
        public static void RegisterDimensions(IDimensionRegister register)
        {
            register.Register(RegisteredDimension);
        }

        /// <summary>
        /// Load (inject) registered dimension into the world.
        /// </summary>
        /// <param name="type">The type which dimension was registered.</param>
        /// <param name="synchronizePrevious">Should the previous dimension be synchronized with changing in the world.</param>
        /// <param name="locationToLoad">Specify the dimension loading tile. Points to the upper left corner.</param>
        public static void LoadDimension(string type, Point locationToLoad, string id = null, bool synchronizePrevious = true)
        {
            if (synchronizePrevious)
                SynchronizeCurrentDimension(CurrentEntity);

            ClearCurrentDimension(CurrentEntity);

            CurrentEntity = RegisteredDimension.GetParser(type).GetDimension(id);
            if (CurrentEntity.DimensionInternal == null)
            {
                Clear();
                return;
            }

            CurrentEntity.Location = new Point(locationToLoad.X, locationToLoad.Y - CurrentEntity.Height);

            LoadCurrentDimension(CurrentEntity);
        }

        private static bool DimensionLoaded(DimensionEntity entity)
        {
            return entity?.DimensionInternal != null;
        }

        private static void LoadCurrentDimension(DimensionEntity entity)
        {
            if (!DimensionLoaded(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Load(entity);
        }

        private static void SynchronizeCurrentDimension(DimensionEntity entity, bool needSave = true)
        {
            if (!DimensionLoaded(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Synchronize(entity);

            if (needSave)
                RegisteredDimension.GetParser(entity.Type).SaveInternal(entity);
        }

        private static void ClearCurrentDimension(DimensionEntity entity)
        {
            if (!DimensionLoaded(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Clear(entity);
        }

        internal static TagCompound Save()
        {
            var tag = new TagCompound
            {
                {"TypeName", CurrentEntity?.Type}, 
                {"Id", CurrentEntity?.Id}, 
                {"Location", CurrentEntity?.Location.ToVector2()},
                {"Size", CurrentEntity != null ?
                    new Point(CurrentEntity.Width, CurrentEntity.Height).ToVector2() : 
                    (object) null
                }
            };

            return tag;
        }

        internal static void Load(TagCompound tag)
        {
            var type = tag.Get<string>("Type");
            if (string.IsNullOrEmpty(type))
                return;

            var id = tag.Get<string>("Id");
            CurrentEntity = RegisteredDimension.GetParser(type).GetDimension(id);

            CurrentEntity.Location = tag.Get<Vector2>("Location").ToPoint();
            CurrentEntity.Id = tag.Get<string>("Id");
            CurrentEntity.Size = tag.Get<Vector2>("Size").ToPoint();

            SynchronizeCurrentDimension(CurrentEntity);
        }

        internal static void Unload()
        {
            Clear();
        }

        internal static void Clear()
        {
            CurrentEntity = null;
        }
    }
}
