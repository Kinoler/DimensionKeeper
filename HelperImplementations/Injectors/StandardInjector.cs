﻿using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.HelperImplementations.Phases;
using DimensionKeeper.Interfaces;

namespace DimensionKeeper.HelperImplementations.Injectors
{
    /// <summary>
    /// The class which register the useful phases.
    /// </summary>
    /// <typeparam name="TDimension"></typeparam>
    public class StandardInjector<TDimension> : DimensionInjector<TDimension> 
        where TDimension: Dimension
    {
        public override void RegisterPhases()
        {
            AddPhase<TilePhase, Dimension>();
            AddPhase<ChestPhase, Dimension>();
            AddPhase<NPCPhase, Dimension>();

            AddPhase<TileFramePhase, Dimension>();
        }
    }
}