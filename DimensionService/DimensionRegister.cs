using System;
using System.Collections.Generic;
using DimensionKeeper.Interfaces;
using DimensionKeeper.Interfaces.Internal;

namespace DimensionKeeper.DimensionService
{
    public class DimensionRegister
    {
        private static DimensionRegister _instance;

        public static DimensionRegister Instance
        {
            get => _instance ?? (_instance = new DimensionRegister());
            internal set => _instance = value;
        }

        private Dictionary<string, IDimensionStorage> Stores { get; } =
            new Dictionary<string, IDimensionStorage>();

        private Dictionary<string, IDimensionInjector> Injectors { get; } =
            new Dictionary<string, IDimensionInjector>();

        /// <summary>
        /// Registers the open generic type which register the dimension types.
        /// You can call the <see cref="Register{TDimensionInjector,TDataParser,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <typeparam name="TDimensionRegister">The class which inherit from <see cref="IDimensionRegister"/>.</typeparam>
        public static void SetupDimensionTypesRegister<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            SetupDimensionTypesRegister(new TDimensionRegister());
        }

        /// <summary>
        /// Register the class which register the dimension types.
        /// You can call the <see cref="Register{TDimensionInjector,TDataParser,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <param name="register">The instance of class which inherit from <see cref="IDimensionRegister"/>.</param>
        public static void SetupDimensionTypesRegister(IDimensionRegister register)
        {
            register.Register(Instance);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type)
            where TDimension: Dimension, new()
            where TDataParser: DimensionStorage<TDimension>, new()
            where TDimensionInjector: DimensionInjector<TDimension>, new()
        {
            var storage = new TDataParser();
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, storage);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDataParser storage)
            where TDimension : Dimension, new()
            where TDataParser : DimensionStorage<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>, new()
        {
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, storage);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDimensionInjector injector)
            where TDimension : Dimension, new()
            where TDataParser : DimensionStorage<TDimension>, new()
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            var storage = new TDataParser();

            Register<TDimensionInjector, TDataParser, TDimension>(type, injector, storage);
        }

        public void Register<TDimensionInjector, TDataParser, TDimension>(string type, TDimensionInjector injector, TDataParser storage)
            where TDimension : Dimension, new()
            where TDataParser : DimensionStorage<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            if (injector == null)
                throw new ArgumentNullException(nameof(injector));
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            storage.Type = type;
            injector.RegisterPhasesInternal();

            Stores.Add(type, storage);
            Injectors.Add(type, injector);
        }

        internal IEnumerable<string> GetTypes()
        {
            return Stores.Keys;
        }

        internal IDimensionStorage GetStorage(string name)
        {
            return Stores[name];
        }

        internal IDimensionInjector GetInjector(string name)
        {
            return Injectors[name];
        }
    }
}
