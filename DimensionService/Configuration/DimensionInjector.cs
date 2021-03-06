﻿using System;
using System.Collections.Generic;
using DimensionKeeper.Interfaces;
using DimensionKeeper.Interfaces.Internal;

namespace DimensionKeeper.DimensionService.Configuration
{
    /// <summary>
    /// Represents the phase container for the specific dimension.
    /// </summary>
    /// <typeparam name="TDimension">The specific <see cref="IDimension"/>.</typeparam>
    public abstract class DimensionInjector<TDimension>: IDimensionInjector 
        where TDimension : IDimension
    {
        /// <summary>
        /// Current registered phases. Be careful it does not reload after <see cref="RegisterPhases"/>.
        /// </summary>
        public List<IDimensionPhase> Phases { get; } = new List<IDimensionPhase>();

        #region Add phase overloads

        /// <summary>
        /// Adds the a phase. Creates a new instance for <see cref="TPhase"/>.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        public void AddPhase<TPhase>(Func<TDimension, bool> condition = null)
            where TPhase : DimensionPhase<TDimension>, new()
        {
            AddPhase<TPhase, TDimension>(new TPhase(), condition);
        }

        /// <summary>
        /// Adds the a phase. Create a new instance for <see cref="TPhase"/>. Allow you to specify phase dimension.
        /// </summary>
        /// <typeparam name="TPhase">The phase.</typeparam>
        /// <typeparam name="TSpecifyDimension">The open generic type for the <see cref="TPhase"/> type.</typeparam>
        public void AddPhase<TPhase, TSpecifyDimension>(Func<TSpecifyDimension, bool> condition = null)
            where TSpecifyDimension : IDimension
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
            where TSpecifyDimension : IDimension
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
            try
            {
                RegisterPhases();
            }
            catch (Exception e)
            {
                DimensionKeeperMod.LogMessage($"{nameof(RegisterPhasesInternal)} throw an error {e}");
            }
        }

        #region Phases execution

        void IDimensionInjector.Load(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                try
                {
                    Phases[i].ExecuteLoadPhaseInternal(dimension);
                }
                catch (Exception e)
                {
                    DimensionKeeperMod.LogMessage($"{nameof(IDimensionInjector.Load)} with {dimension} throw an error {e}");
                }
            }
        }

        void IDimensionInjector.Synchronize(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                try
                {
                    Phases[i].ExecuteSynchronizePhaseInternal(dimension);
                }
                catch (Exception e)
                {
                    DimensionKeeperMod.LogMessage($"{nameof(IDimensionInjector.Synchronize)} with {dimension} throw an error {e}");
                }
            }
        }

        void IDimensionInjector.Clear(DimensionEntity dimension)
        {
            for (var i = 0; i < Phases.Count; i++)
            {
                try
                {
                    Phases[i].ExecuteClearPhaseInternal(dimension);
                }
                catch (Exception e)
                {
                    DimensionKeeperMod.LogMessage($"{nameof(IDimensionInjector.Clear)} with {dimension} throw an error {e}");
                }
            }
        }

        #endregion
    }
}
