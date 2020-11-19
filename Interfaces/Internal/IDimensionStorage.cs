using DimensionKeeper.DimensionService.InternalClasses;
using Microsoft.Xna.Framework;

namespace DimensionKeeper.Interfaces.Internal
{
    internal interface IDimensionStorage
    {
        DimensionEntityInternal CreateEmptyEntity(Point location, Point size);

        DimensionEntityInternal LoadInternal(string id);

        void SaveInternal(DimensionEntityInternal dimension);
    }
}