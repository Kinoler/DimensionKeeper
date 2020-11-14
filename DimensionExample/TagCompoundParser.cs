using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;
using TestMod.DimensionLogic;

namespace TestMod.DimensionExample
{
    public class TagCompoundParser<TDimension>: DataParser<TDimension> where TDimension : Dimension
    {
        public override TDimension Initialize()
        {
            throw new NotImplementedException();
        }

        public override TDimension Load(TagCompound tag)
        {
            throw new NotImplementedException();
        }

        public override void Save(TDimension dimension, TagCompound tag)
        {
            throw new NotImplementedException();
        }
    }
}
