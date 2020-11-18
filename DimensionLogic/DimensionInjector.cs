using System;
using System.Collections.Generic;
using TestMod.DimensionLogic.InternalHelperClasses;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// Represents the phase container for the specific dimension.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="Dimension"/>.</typeparam>
    public abstract class DimensionInjector<TDimension>: DimensionInjectorInternal where TDimension : Dimension
    {
        public List<DimensionPhasesInternal> Phases { get; } = new List<DimensionPhasesInternal>();

        #region Add phase overloads

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        public void AddPhase<TPhase>(Func<TDimension, bool> condition = null)
            where TPhase : DimensionPhases<TDimension>, new()
        {
            AddPhase<TPhase, TDimension>(new TPhase(), condition);
        }

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        /// <typeparam name="TSpecifyDimension">The open generic type for the <see cref="TPhase"/> type.</typeparam>
        public void AddPhase<TPhase, TSpecifyDimension>(Func<TSpecifyDimension, bool> condition = null)
            where TSpecifyDimension : Dimension
            where TPhase : DimensionPhases<TSpecifyDimension>, new()
        {
            AddPhase<TPhase, TSpecifyDimension>(new TPhase(), condition);
        }

        /// <summary>
        /// Adds the a phase. Using an instance.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        /// <typeparam name="TSpecifyDimension">The open generic type for the <see cref="TPhase"/> type.</typeparam>
        /// <param name="instance">The instance of phase</param>
        /// <param name="condition">Allow you execute the phase by condition.</param>
        public void AddPhase<TPhase, TSpecifyDimension>(TPhase instance, Func<TSpecifyDimension, bool> condition = null)
            where TSpecifyDimension : Dimension
            where TPhase : DimensionPhases<TSpecifyDimension>
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            instance.Condition = condition;
            Phases.Add(instance);
        }

        #endregion

        /// <summary>
        /// Allow you to register custom phases using the <see cref="AddPhase{TPhase}"/> method.
        /// </summary>
        public virtual void RegisterPhases()
        {
        }

        internal void RegisterPhasesInternal()
        {
            RegisterPhases();
        }

        #region Phases execution

        internal override void Load(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteLoadPhaseInternal(dimension);
            }
        }

        internal override void Synchronize(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteSynchronizePhaseInternal(dimension);
            }
        }

        internal override void Clear(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteClearPhaseInternal(dimension);
            }
        }

        #endregion

    }
}
