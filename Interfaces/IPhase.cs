using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMod.Interfaces
{
    public abstract class Phase<TData>
    {
        public abstract void Execute(TData data);
    }
}
