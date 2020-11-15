using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMod.DimensionLogic;
using TestMod.Interfaces;

namespace TestMod.DimensionExample
{
    public class DimensionRegisterExample : IDimensionRegister
    {
        public const string ExampleName = "Name";

        public void Register(DimensionsRegister register)
        {
            register.Register<DimensionInjectorExample, DataParserExample, DimensionExample>(ExampleName);
        }
    }
}
