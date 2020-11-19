using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace DimensionKeeper.DimensionService.DefaultStorages
{
    public class ResourceManagerStorage<TDimension>: DimensionStorage<TDimension> where TDimension : Dimension, new()
    {
        public virtual string ResourceFolder => "Resources";

        public override TDimension Load()
        {
            return null;
        }

        public override void Save(TDimension dimension)
        {
            
        }
    }
}
