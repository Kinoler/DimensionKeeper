using TestMod.DimensionLogic;
using TestMod.DimensionLogic.DefaultPhases;

namespace TestMod.DimensionExample
{
    public class DimensionInjectorExample : DimensionInjector<DimensionExample>
    {
        public override void OnPhasesRegister()
        {
            AddPhase<TilePhase>();
            //AddPhase<TileObjectDataPhase>();
            AddPhase<ChestPhase>();
        }
    }
}
