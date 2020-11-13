namespace TestMod.DimensionLogic
{
    public abstract class DataParser
    {
        private Dimension CachedDimension { get; set; }

        public virtual bool AlwaysNew => false;

        internal Dimension GetDimension()
        {
            if (CachedDimension == null || AlwaysNew)
            {
                CachedDimension = LoadInternal();
            }

            return CachedDimension;
        }

        internal abstract Dimension LoadInternal();
        internal abstract void Save(Dimension dimension);
    }

    public abstract class DataParser<TDimension>: DataParser where TDimension: Dimension
    {
        protected DataParser()
        {
        }

        internal override Dimension LoadInternal()
        {
            return Load();
        }

        internal override void Save(Dimension dimension)
        {
            Save((TDimension)dimension);
        }

        public abstract TDimension Load();

        public abstract void Save(TDimension dimension);
    }
}
