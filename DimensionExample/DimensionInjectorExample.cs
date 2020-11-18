﻿using TestMod.DimensionLogic;
using TestMod.DimensionLogic.DefaultPhases;

namespace TestMod.DimensionExample
{
    public class DimensionInjectorExample : DimensionInjector<DimensionExample>
    {
        public override void RegisterPhases()
        {
            AddPhase<TilePhase, Dimension>();
            AddPhase<TileObjectDataPhase, Dimension>();
            AddPhase<ChestPhase, Dimension>();

            AddPhase<TileFramePhase, Dimension>();
        }
    }
}
