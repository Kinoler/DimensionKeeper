using Microsoft.Xna.Framework;
using Terraria;

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

        internal static Chest Offset(this Chest chest, Point point)
		{
            return chest.Offset(point.X, point.Y);
		}

        internal static Chest Offset(this Chest chest, int x, int y)
		{
            chest.x += x;
            chest.y += y;
			return chest;
		}
	}
}
