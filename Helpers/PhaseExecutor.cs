using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMod.DimensionLogic;
using TestMod.Interfaces;

namespace TestMod.Helpers
{
    public class PhaseExecutor<TData> where TData: class
    {
        protected internal List<Type> PhaseTypes { get; } = new List<Type>();

        public void AddPhase<TPhase>() 
            where TPhase: Phase<TData>, new()
        {
            PhaseTypes.Add(typeof(TPhase));
        }

        internal void Execute(TData data)
        {
            foreach (var phaseType in PhaseTypes)
            {
                var phase = (Phase<TData>)Activator.CreateInstance(phaseType);
                phase.Execute(data);
            }
        }
    }
}
