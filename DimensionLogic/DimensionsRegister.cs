using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace TestMod.DimensionLogic
{
    public class DimensionsRegister
    {
        private Dictionary<string, (DataParser Parser, DimensionInjector Injector)> DimensionScripts { get; } = 
            new Dictionary<string, (DataParser Parser, DimensionInjector Injector)>();


        public void Register<TDimensionInjector, TDataParser, TDimension>(string name)
            where TDimension: Dimension
            where TDataParser: DataParser<TDimension>, new()
            where TDimensionInjector: DimensionInjector<TDimension>, new()
        {
            var parser = new TDataParser();
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDataParser, TDimension>(name, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string name, TDataParser parser)
            where TDimension : Dimension
            where TDataParser : DataParser<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>, new()
        {
            var injector = new TDimensionInjector();
            Register<TDimensionInjector, TDataParser, TDimension>(name, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string name, TDimensionInjector injector)
            where TDimension : Dimension
            where TDataParser : DataParser<TDimension>, new()
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            var parser = new TDataParser();
            Register<TDimensionInjector, TDataParser, TDimension>(name, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string name, TDimensionInjector injector, TDataParser parser)
            where TDimension : Dimension
            where TDataParser : DataParser<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            if (injector == null)
                throw new ArgumentNullException(nameof(injector));
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            injector.RegisterPhasesInternal();
            DimensionScripts.Add(name, (parser, injector));
        }

        internal IEnumerable<string> GetNames()
        {
            return DimensionScripts.Keys;
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
