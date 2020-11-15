using TestMod.DimensionExample.Phases;
using TestMod.DimensionLogic;

namespace TestMod.DimensionExample
{
    public class DimensionInjectorExample : DimensionInjector<DimensionExample>
    {
        public override void OnPhasesRegister()
        {
            AddPhase<TilePhase>();
            AddPhase<ChestPhase>();
        }
    }
}
