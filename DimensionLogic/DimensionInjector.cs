﻿using System;
using System.Collections.Generic;

namespace TestMod.DimensionLogic
{
    public abstract class DimensionInjector
    {
        internal abstract void Load(Dimension dimension);
        internal abstract void Synchronize(Dimension dimension);
        internal abstract void Clear(Dimension dimension);
    }

    /// <summary>
    /// The class used to register <see cref="DimensionPhases{TDimension}"/> phases for specific <see cref="TDimension"/>.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="Dimension"/>.</typeparam>
    public abstract class DimensionInjector<TDimension>: DimensionInjector where TDimension : Dimension
    {
        public List<DimensionPhases> Phases { get; } = new List<DimensionPhases>();

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        public void AddPhase<TPhase>()
            where TPhase : DimensionPhases<TDimension>, new()
        {
            AddPhase<TPhase, TDimension>(new TPhase());
        }

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        /// <typeparam name="TSpecifyDimension">The open generic type for the <see cref="TPhase"/> type.</typeparam>
        public void AddPhase<TPhase, TSpecifyDimension>()
            where TSpecifyDimension : Dimension
            where TPhase : DimensionPhases<TSpecifyDimension>, new()
        {
            AddPhase<TPhase, TSpecifyDimension>(new TPhase());
        }

        /// <summary>
        /// Adds the a phase. Using an instance.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        /// <typeparam name="TSpecifyDimension">The open generic type for the <see cref="TPhase"/> type.</typeparam>
        /// <param name="instance">The instance of phase</param>
        public void AddPhase<TPhase, TSpecifyDimension>(TPhase instance)
            where TSpecifyDimension : Dimension
            where TPhase : DimensionPhases<TSpecifyDimension>
        {
            if (instance == null) 
                throw new ArgumentNullException(nameof(instance));

            Phases.Add(instance);
        }

        /// <summary>
        /// Allow you to register custom phases using the <see cref="AddPhase{TPhase}()"/> method.
        /// </summary>
        public virtual void OnPhasesRegister()
        {
        }

        internal void RegisterPhasesInternal()
        {
            OnPhasesRegister();
        }

        internal override void Load(Dimension dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteLoadPhaseInternal(dimension);
            }
        }

        internal override void Synchronize(Dimension dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteSynchronizePhaseInternal(dimension);
            }
        }

        internal override void Clear(Dimension dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteClearPhaseInternal(dimension);
            }
        }
    }
}
