using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mono.CompilerServices.SymbolWriter;
using TestMod.DimensionLogic.InternalHelperClasses;

namespace TestMod.DimensionLogic
{
    /// <summary>
    /// Represents the dimension which will be or already loaded into the world.
    /// </summary>
    public class DimensionEntity<TDimension>: DimensionEntity where TDimension: Dimension
    {
        public TDimension Dimension { get; internal set; }

        public void CopyFrom(DimensionEntity otherEntity)
        {
            DimensionInternal = otherEntity.DimensionInternal;
            Location = otherEntity.Location;
            Type = otherEntity.Type;
            Id = otherEntity.Id;
            Size = otherEntity.Size;
        }

        internal override Dimension DimensionInternal
        {
            get => Dimension;
            set => Dimension = (TDimension)value;
        }

    }
}
