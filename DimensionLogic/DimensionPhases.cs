using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    public abstract class DimensionPhases
    {
        internal abstract void ExecuteLoadPhaseInternal(DimensionEntity dimension);
        internal abstract void ExecuteSynchronizePhaseInternal(DimensionEntity dimension);
        internal abstract void ExecuteClearPhaseInternal(DimensionEntity dimension);
    }

    /// <summary>
    /// The class that allow you to handle inject process.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="Dimension"/>.</typeparam>
    public class DimensionPhases<TDimension>: DimensionPhases where TDimension: Dimension
    {
        /// <summary>
        /// Allows you to handle the load process. Modify the terraria world according to <see cref="dimension"/>.
        /// </summary>
        /// <remarks>
        /// Calls whenever the <see cref="DimensionLoader.LoadDimension"/> is called. (For each registered phases)
        /// </remarks>
        /// <param name="dimension">The loading dimension</param>
        public virtual void ExecuteLoadPhase(DimensionEntity<TDimension> dimension)
        {
        }

        /// <summary>
        /// Takes something like a snapshot.
        /// Allows you to handle the synchronization process. Modify the <see cref="dimension"/> according to the terraria world.
        /// </summary>
        /// <remarks>
        /// Calls whenever the <see cref="DimensionLoader.LoadDimension"/> is called with the synchronizePrevious is true. (For each registered phases)
        /// </remarks>
        /// <param name="dimension">The dimension which should be modified</param>
        public virtual void ExecuteSynchronizePhase(DimensionEntity<TDimension> dimension)
        {
        }

        /// <summary>
        /// Allows you to handle the clear process.
        /// Clear the terraria world according to the <see cref="ExecuteLoadPhase"/> method changes.
        /// </summary>
        /// <remarks>
        /// Calls whenever the <see cref="DimensionLoader.LoadDimension"/> is called. (For each registered phases)
        /// </remarks>
        /// <param name="dimension">The synchronized dimension.</param>
        public virtual void ExecuteClearPhase(DimensionEntity<TDimension> dimension)
        {
        }

        internal override void ExecuteLoadPhaseInternal(DimensionEntity dimension)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(dimension);

            ExecuteLoadPhase(temp);
        }

        internal override void ExecuteSynchronizePhaseInternal(DimensionEntity dimension)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(dimension);

            ExecuteSynchronizePhase(temp);
        }

        internal override void ExecuteClearPhaseInternal(DimensionEntity dimension)
        {
            var temp = new DimensionEntity<TDimension>();
            temp.CopyFrom(dimension);

            ExecuteClearPhase(temp);
        }
    }
}