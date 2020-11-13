using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    public class DimensionPhases<TDimension> where TDimension: Dimension
    {
        public virtual void ExecuteLoadPhase(TDimension dimension)
        {
        }

        public virtual void ExecuteSavePhase(TDimension dimension)
        {
        }

        public virtual void ExecuteClearPhase(TDimension dimension)
        {
        }
    }
}
