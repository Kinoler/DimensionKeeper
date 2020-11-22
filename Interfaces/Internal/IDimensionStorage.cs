using System.IO;
using DimensionKeeper.DimensionService;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.Interfaces.Internal
{
    internal interface IDimensionStorage
    {
        DimensionEntity CreateEmptyEntity(Point location, Point size);

        DimensionEntity LoadInternal(string id);

        void SaveInternal(DimensionEntity dimension);

        void SendInternal(BinaryWriter writer);

        void ReceiveInternal(BinaryReader reader);
    }
}