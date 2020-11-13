using System.Collections.Generic;

namespace TestMod.DimensionLogic
{
    public class DimensionsRegister
    {
        private Dictionary<string, (DataParser Parser, DimensionInjector Injector)> DimensionScripts { get; } = 
            new Dictionary<string, (DataParser Parser, DimensionInjector Injector)>();

        public void Register<TDataParser, TDimensionInjector, TDimension>(string name)
            where TDimension: Dimension
            where TDataParser: DataParser<TDimension>, new()
            where TDimensionInjector: DimensionInjector<TDimension>, new()
        {
            var parser = new TDataParser();
            Register<TDataParser, TDimensionInjector, TDimension>(name, parser);
        }

        public void Register<TDataParser, TDimensionInjector, TDimension>(string name, TDataParser parser)
            where TDimension : Dimension
            where TDataParser : DataParser<TDimension>, new()
            where TDimensionInjector : DimensionInjector<TDimension>, new()
        {
            var injector = new TDimensionInjector();
            injector.RegisterPhasesInternal();

            DimensionScripts.Add(name, (parser, injector));
        }

        internal void Clear()
        {
            DimensionScripts.Clear();
        }

        internal DataParser GetParser(string name)
        {
            return DimensionScripts[name].Parser;
        }

        internal DimensionInjector GetInjector(string name)
        {
            return DimensionScripts[name].Injector;
        }
    }
}
