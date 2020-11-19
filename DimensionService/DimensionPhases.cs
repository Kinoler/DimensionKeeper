﻿using System;
using TestMod.DimensionService.InternalHelperClasses;

namespace TestMod.DimensionService
{
    /// <summary>
    /// The class that allow you to handle inject process.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="Dimension"/>.</typeparam>
    public class DimensionPhases<TDimension>: DimensionPhasesInternal where TDimension: Dimension
    {
        internal Func<TDimension, bool> Condition { get; set; }

        //TODO remarks
        /// <summary>
        /// Allows you to handle the load process. Modify the terraria world according to <see cref="entity"/>.
        /// </summary>
        /// <remarks>
        /// Calls whenever the  is called. (For each registered phases)
        /// </remarks>
        /// <param name="entity">The loading dimension</param>
        public virtual void ExecuteLoadPhase(DimensionEntity<TDimension> entity)
        {
        }

        //TODO remarks
        /// <summary>
        /// Takes something like a snapshot.
        /// Allows you to handle the synchronization process. Modify the <see cref="entity"/> according to the terraria world.
        /// </summary>
        /// <remarks>
        /// Calls whenever the  is called with the synchronizePrevious is true. (For each registered phases)
        /// </remarks>
        /// <param name="entity">The dimension which should be modified</param>
        public virtual void ExecuteSynchronizePhase(DimensionEntity<TDimension> entity)
        {
        }

        //TODO remarks
        /// <summary>
        /// Allows you to handle the clear process.
        /// Clear the terraria world according to the <see cref="ExecuteLoadPhase"/> method changes.
        /// </summary>
        /// <remarks>
        /// Calls whenever the  is called. (For each registered phases)
        /// </remarks>
        /// <param name="entity">The synchronized dimension.</param>
        public virtual void ExecuteClearPhase(DimensionEntity<TDimension> entity)
        {
        }

        internal override void ExecuteLoadPhaseInternal(DimensionEntity entity)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(entity);

            if (Condition?.Invoke(temp.Dimension) ?? true)
                ExecuteLoadPhase(temp);
        }

        internal override void ExecuteSynchronizePhaseInternal(DimensionEntity entity)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(entity);

            if (Condition?.Invoke(temp.Dimension) ?? true)
                ExecuteSynchronizePhase(temp);
        }

        internal override void ExecuteClearPhaseInternal(DimensionEntity entity)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(entity);

            if (Condition?.Invoke(temp.Dimension) ?? true)
                ExecuteClearPhase(temp);
        }
    }
}