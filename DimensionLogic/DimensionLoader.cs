using System.Collections.Generic;
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

        internal static bool ValidateDimension(DimensionEntity entity)
        {
            return entity?.DimensionInternal != null && 
                   entity.Size != Point.Zero && 
                   entity.Location != Point.Zero;
        }

        internal static void LoadDimension(DimensionEntity entity)
        {
            if (!ValidateDimension(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Load(entity);
        }

        internal static void SynchronizeDimension(DimensionEntity entity, bool needSave = true)
        {
            if (!ValidateDimension(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Synchronize(entity);

            if (needSave)
                RegisteredDimension.GetParser(entity.Type).SaveInternal(entity);
        }

        internal static void ClearDimension(DimensionEntity entity)
        {
            if (!ValidateDimension(entity))
                return;
            RegisteredDimension.GetInjector(entity.Type).Clear(entity);
        }
    }
}
