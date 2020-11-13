using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria;
using Terraria.ModLoader;

namespace TestMod
{
    public static class ModUtils
    {
        public static Texture2D Offset(this Texture2D texture, int x, int y, int width, int height)
		{
			Texture2D result = new Texture2D(Main.graphics.GraphicsDevice, width, height);
			Color[] data = new Color[height * width];
			texture.GetData(0, new Rectangle?(new Rectangle(x, y, width, height)), data, 0, data.Length);
			result.SetData(data);
			return result;
		}

		internal static Vector2 Offset(this Vector2 position, float x, float y)
		{
			position.X += x;
			position.Y += y;
			return position;
		}
	}
}
