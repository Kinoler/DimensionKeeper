using DimensionKeeper.DimensionService.InternalClasses;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// The main class DimensionKeeper that allows you to manipulate dimensions.
    /// </summary>
    public static class DimensionLoader
    {
        internal static DimensionRegister RegisteredDimension => DimensionRegister.Instance;

        internal static bool ValidateDimension(DimensionEntityInternal entity)
        {
            return !(entity == null ||
                   entity.Size == Point.Zero ||
                   entity.Location == Point.Zero);
        }

        internal static void LoadDimension(DimensionEntityInternal entity)
        {
            if (!ValidateDimension(entity))
                return;

            RegisteredDimension.GetInjector(entity.Type).Load(entity);
        }

        internal static void SynchronizeDimension(DimensionEntityInternal entity, bool needSave = true)
        {
            if (!ValidateDimension(entity))
                return;

            RegisteredDimension.GetInjector(entity.Type).Synchronize(entity);

            if (needSave)
                RegisteredDimension.GetStorage(entity.Type).SaveInternal(entity);
        }

        internal static void ClearDimension(DimensionEntityInternal entity)
        {
            if (!ValidateDimension(entity))
                return;

            RegisteredDimension.GetInjector(entity.Type).Clear(entity);
        }

        internal static void CreateDimension(DimensionEntityInternal entity)
        {
            if (!ValidateDimension(entity))
                return;

            RegisteredDimension.GetInjector(entity.Type).Clear(entity);
        }
    }
}
