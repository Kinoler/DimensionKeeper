using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using TestMod.DimensionLogic.InternalHelperClasses;

namespace TestMod.Helpers
{
    public static class ModUtils
    {
        internal static Vector2 Offset(this Vector2 position, float x, float y)
		{
			position.X += x;
			position.Y += y;
			return position;
		}

        internal static Point Offset(this Point position, Point point)
		{
            return position.Offset(point.X, point.Y);
		}

        internal static Point Offset(this Point position, int x, int y)
		{
			position.X += x;
			position.Y += y;
			return position;
		}

        public static IEnumerable<Point> RectangularPoints(this DimensionEntity entity, Point offset = default)
        {
            var locationToLoad = entity.Location;
            var offsetX = offset.X;
            var offsetY = offset.Y;

            for (var x = locationToLoad.X + offsetX; x < locationToLoad.X + entity.Width - offsetX; x++)
            {
                yield return new Point(x, locationToLoad.Y + offsetY);
            }

            for (var y = locationToLoad.Y + offsetY; y < locationToLoad.Y + entity.Height - offsetY; y++)
            {
                yield return new Point(locationToLoad.X + entity.Width - offsetX, y);
            }

            for (var y = locationToLoad.Y + offsetY; y < locationToLoad.Y + entity.Height - offsetY; y++)
            {
                yield return new Point(locationToLoad.X + offsetX, y);
            }

            for (var x = locationToLoad.X + offsetX; x < locationToLoad.X + entity.Width - offsetX; x++)
            {
                yield return new Point(x, locationToLoad.Y + entity.Height - offsetY);
            }
        }
    }
}
