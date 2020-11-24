using System;
using System.Collections.Generic;
using DimensionKeeper.DimensionService.Configuration;
using DimensionKeeper.Interfaces;
using DimensionKeeper.Interfaces.Internal;

namespace DimensionKeeper.DimensionService
{
    /// <summary>
    /// The dimension types registrar.
    /// </summary>
    public class DimensionRegister
    {
        private static DimensionRegister _instance;

        /// <summary>
        /// The instance of class.
        /// </summary>
        public static DimensionRegister Instance
        {
            get => _instance ?? (_instance = new DimensionRegister());
            internal set => _instance = value;
        }

        internal Dictionary<string, IDimensionStorage> Stores { get; } =
            new Dictionary<string, IDimensionStorage>();

        internal Dictionary<string, IDimensionInjector> Injectors { get; } =
            new Dictionary<string, IDimensionInjector>();

        /// <summary>
        /// Registers the open generic type which register the dimension types.
        /// You can call the <see cref="Register{TDimensionInjector,TDimensionStorage,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <typeparam name="TDimensionRegister">The class which inherit from <see cref="IDimensionRegister"/>.</typeparam>
        public static void SetupDimensionTypesRegister<TDimensionRegister>()
            where TDimensionRegister : IDimensionRegister, new()
        {
            SetupDimensionTypesRegister(new TDimensionRegister());
        }

        /// <summary>
        /// Register the class which register the dimension types.
        /// You can call the <see cref="Register{TDimensionInjector,TDimensionStorage,TDimension}(string)"/> method directly through the Instance but it looks better in the IDimensionRegister.
        /// </summary>
        /// <param name="register">The instance of class which inherit from <see cref="IDimensionRegister"/>.</param>
        public static void SetupDimensionTypesRegister(IDimensionRegister register)
        {
            register.Register(Instance);
        }

        /// <summary>
        /// Register a new dimension type.
        /// </summary>
        /// <typeparam name="TDimensionInjector">The class of dimension injector.</typeparam>
        /// <typeparam name="TDimensionStorage">The class of dimension storage.</typeparam>
        /// <typeparam name="TDimension">The dimension class.</typeparam>
        /// <param name="type">The name of dimension type.</param>
        public void Register<TDimensionInjector, TDimensionStorage, TDimension>(string type)
            where TDimension: Dimension, new()
            where TDimensionStorage: DimensionStorage<TDimension>, new()
            where TDimensionInjector: DimensionInjector<TDimension>, new()
        {
            var storage = new TDimensionStorage();
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDimensionStorage, TDimension>(type, injector, storage);
        }

        /// <summary>
        /// Register a new dimension type.
        /// </summary>
        /// <typeparam name="TDimensionInjector">The class of dimension injector.</typeparam>
        /// <typeparam name="TDimensionStorage">The class of dimension storage.</typeparam>
        /// <typeparam name="TDimension">The dimension class.</typeparam>
        /// <param name="type">The name of dimension type.</param>
        /// <param name="storage">The instance of storage.</param>
        public void Register<TDimensionInjector, TDimensionStorage, TDimension>(string type, TDimensionStorage storage)
            where TDimension : Dimension, new()
            where TDimensionStorage : DimensionStorage<TDimension>
            where TDimensionInjector : DimensionInjector<TDimension>, new()
        {
            var injector = new TDimensionInjector();

            Register<TDimensionInjector, TDimensionStorage, TDimension>(type, injector, storage);
        }

        /// <summary>
        /// Register a new dimension type.
        /// </summary>
        /// <typeparam name="TDimensionInjector">The class of dimension injector.</typeparam>
        /// <typeparam name="TDimensionStorage">The class of dimension storage.</typeparam>
        /// <typeparam name="TDimension">The dimension class.</typeparam>
        /// <param name="type">The name of dimension type.</param>
        /// <param name="injector">The instance of injector.</param>
        public void Register<TDimensionInjector, TDimensionStorage, TDimension>(string type, TDimensionInjector injector)
            where TDimension : Dimension, new()
            where TDimensionStorage : DimensionStorage<TDimension>, new()
            where TDimensionInjector : DimensionInjector<TDimension>
        {
            var storage = new TDimensionStorage();

            Register<TDimensionInjector, TDimensionStorage, TDimension>(type, injector, storage);
        }

        /// <summary>
        /// Register a new dimension type.
        /// </summary>
        /// <typeparam name="TDimensionInjector">The class of dimension injector.</typeparam>
        /// <typeparam name="TDimensionStorage">The class of dimension storage.</typeparam>
        /// <typeparam name="TDimension">The dimension class.</typeparam>
        /// <param name="type">The name of dimension type.</param>
        /// <param name="injector">The instance of injector.</param>
        /// <param name="storage">The instance of storage.</param>
        public void Register<TDimensionInjector, TDimensionStorage, TDimension>(string type, TDimensionInjector injector, TDimensionStorage storage)
            where TDimension : Dimension, new()
            where TDimensionStorage : DimensionStorage<TDimension>
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

        internal IDimensionStorage GetStorage(string type)
        {
            return Stores[type];
        }

        internal IDimensionInjector GetInjector(string type)
        {
            return Injectors[type];
        }
    }
}
