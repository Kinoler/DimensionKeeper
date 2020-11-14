using System.Collections.Generic;

namespace TestMod.DimensionLogic
{
    public abstract class DimensionInjector
    {
        internal abstract void Load(Dimension dimension);
        internal abstract void Synchronize(Dimension dimension);
        internal abstract void Clear(Dimension dimension);
    }

    public abstract class DimensionInjector<TDimension>: DimensionInjector where TDimension : Dimension
    {
        public List<DimensionPhases<TDimension>> Phases { get; } = new List<DimensionPhases<TDimension>>();

        public void AddPhase<TPhase>(TPhase instance = default)
            where TPhase : DimensionPhases<TDimension>, new()
        {
            Phases.Add(instance ?? new TPhase());
        }

        public virtual void RegisterPhases()
        {
        }

        internal void RegisterPhasesInternal()
        {
            RegisterPhases();
        }

        internal override void Load(Dimension dimension)
        {
            var dimensionGen = (TDimension)dimension;
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteLoadPhase(dimensionGen);
            }
        }

        internal override void Synchronize(Dimension dimension)
        {
            var dimensionGen = (TDimension)dimension;
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteSynchronizePhase(dimensionGen);
            }
        }

        internal override void Clear(Dimension dimension)
        {
            var dimensionGen = (TDimension)dimension;
            for (var i = 0; i < Phases.Count; i++)
            {
                Phases[i].ExecuteClearPhase(dimensionGen);
            }
        }
    }
}
