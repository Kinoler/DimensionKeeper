﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TestMod.DimensionLogic.InternalHelperClasses;
using TestMod.Interfaces;

namespace TestMod.DimensionLogic
{
    public class DimensionsRegister
    {
        private static DimensionsRegister _instance;

        public static DimensionsRegister Instance
        {
            get => _instance ?? (_instance = new DimensionsRegister());
            internal set => _instance = value;
        }

        /// <summary>
        /// Registers the open generic type which register the dimensions.
        /// You can call the <see cref="Register{TDimensionInjector,TDataParser,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <typeparam name="TDimensionRegister">The class which inherit from <see cref="IDimensionRegister"/>.</typeparam>
        public static void SetupDimensionTypesRegister<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            SetupDimensionTypesRegister(new TDimensionRegister());
        }

        /// <summary>
        /// Register the class which register the dimensions.
        /// You can call the <see cref="Register{TDimensionInjector,TDataParser,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <param name="register">The instance of class which inherit from <see cref="IDimensionRegister"/>.</param>
        public static void SetupDimensionTypesRegister(IDimensionRegister register)
        {
            register.Register(Instance);
        }

        private Dictionary<string, DataParser> Parsers { get; } =
            new Dictionary<string, DataParser>();

        private Dictionary<string, DimensionInjector> Injectors { get; } =
            new Dictionary<string, DimensionInjector>();

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type)
            where TDimension: Dimension, new()
            where TDataParser: DataParser<TDimension>, new()
            where TDimensionInjector: DimensionInjector<TDimension>, new()
        {
            var parser = new TDataParser();
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDataParser parser)
            where TDimension : Dimension, new()
            where TDataParser : DataParser<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>, new()
        {
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDimensionInjector injector)
            where TDimension : Dimension, new()
            where TDataParser : DataParser<TDimension>, new()
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            var parser = new TDataParser();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, parser);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDimensionInjector injector, TDataParser parser)
            where TDimension : Dimension, new()
            where TDataParser : DataParser<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            if (injector == null)
                throw new ArgumentNullException(nameof(injector));
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            parser.Type = type;
            injector.RegisterPhasesInternal();

            Parsers.Add(type, parser);
            Injectors.Add(type, injector);
        }

        internal IEnumerable<string> GetNames()
        {
            return Parsers.Keys;
        }

        internal DataParser GetParser(string name)
        {
            return Parsers[name];
        }

        internal DimensionInjector GetInjector(string name)
        {
            return Injectors[name];
        }
    }
}
