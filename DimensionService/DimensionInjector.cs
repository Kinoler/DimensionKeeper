using System;
using System.Collections.Generic;
using DimensionKeeper.DimensionService.InternalClasses;
using DimensionKeeper.Interfaces;
using DimensionKeeper.Interfaces.Internal;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// Represents the phase container for the specific dimension.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="Dimension"/>.</typeparam>
    public abstract class DimensionInjector<TDimension>: IDimensionInjector where TDimension : Dimension
    {
        public List<IDimensionPhase> Phases { get; } = new List<IDimensionPhase>();

        #region Add phase overloads

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        public void AddPhase<TPhase>(Func<TDimension, bool> condition = null)
            where TPhase : DimensionPhase<TDimension>, new()
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
            where TPhase : DimensionPhase<TSpecifyDimension>, new()
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
            where TPhase : DimensionPhase<TSpecifyDimension>
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

        void IDimensionInjector.Load(DimensionEntityInternal dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteLoadPhaseInternal(dimension);
            }
        }

        void IDimensionInjector.Synchronize(DimensionEntityInternal dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteSynchronizePhaseInternal(dimension);
            }
        }

        void IDimensionInjector.Clear(DimensionEntityInternal dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteClearPhaseInternal(dimension);
            }
        }

        #endregion
    }
}
