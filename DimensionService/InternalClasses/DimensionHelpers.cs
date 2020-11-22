using System;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.DimensionService.InternalClasses
{
    internal class DimensionHelpers
    {
        internal static DimensionRegister RegisteredDimension => DimensionRegister.Instance;

        internal static bool ValidateDimension(DimensionEntity entity)
        {
            return !(entity == null ||
                   entity.Size == Point.Zero ||
                   entity.Location == Point.Zero);
        }

        internal static void LoadDimension(DimensionEntity entity)
        {
            if (!ValidateDimension(entity))
                return;

            try
            {
                RegisteredDimension.GetInjector(entity.Type).Load(entity);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        internal static void SynchronizeDimension(DimensionEntity entity, bool needSave = true)
        {
            if (!ValidateDimension(entity))
                return;

            try
            {
                RegisteredDimension.GetInjector(entity.Type).Synchronize(entity);

                if (needSave)
                    RegisteredDimension.GetStorage(entity.Type).SaveInternal(entity);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        internal static void ClearDimension(DimensionEntity entity)
        {
            if (!ValidateDimension(entity))
                return;

            try
            {
                RegisteredDimension.GetInjector(entity.Type).Clear(entity);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
