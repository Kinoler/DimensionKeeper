using System;
using DimensionKeeper.DimensionService;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;

namespace DimensionKeeper.EyeDropperUI
{
    public class EyeDropperUI
    {
        private bool _mouseLeftButton = false;

        private bool _mousePrevLeftButton = false;

        public bool Visible { get; set; }

        private bool _justLeftMouseDown;
        private bool _leftMouseDown;

        private int _startTileX = -1;
        private int _startTileY = -1;
        private int _lastMouseTileX = -1;
        private int _lastMouseTileY = -1;

		public EyeDropperUI()
        {
            Visible = false;
        }
        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        internal void UpdateMouseInput()
        {
            var mouseState = Mouse.GetState();
            _mousePrevLeftButton = _mouseLeftButton;
            _mouseLeftButton = mouseState.LeftButton == ButtonState.Pressed;

            if (!this.Visible)
                return;

            if (!_mousePrevLeftButton &&
                _mouseLeftButton &&
                !Main.LocalPlayer.mouseInterface)
            {
                _leftMouseDown = true;
                Main.LocalPlayer.mouseInterface = true;
            }

            if (_mousePrevLeftButton &&
                !_mouseLeftButton &&
                !Main.LocalPlayer.mouseInterface)
            {
                _leftMouseDown = false;
                _justLeftMouseDown = true;
            }
        }

        internal void SetupMouseInterface()
		{
            Main.LocalPlayer.mouseInterface = true;
        }

        internal void UpdateEyeDropperCompute()
        {
            Main.LocalPlayer.showItemIcon2 = ItemID.EmptyDropper;
            if (_leftMouseDown)
            {
                var point = Main.MouseWorld.ToTileCoordinates();
                if (_startTileX == -1)
                {
                    _startTileX = point.X;
                    _startTileY = point.Y;
                    _lastMouseTileX = -1;
                    _lastMouseTileY = -1;
                }

                _lastMouseTileX = point.X;
                _lastMouseTileY = point.Y;
            }

            if (_justLeftMouseDown)
            {
                if (_startTileX != -1 && _startTileY != -1 && _lastMouseTileX != -1 && _lastMouseTileY != -1)
                {
                    var minX = Math.Min(_startTileX, _lastMouseTileX);
                    var maxX = Math.Max(_startTileX, _lastMouseTileX);
                    var minY = Math.Min(_startTileY, _lastMouseTileY);
                    var maxY = Math.Max(_startTileY, _lastMouseTileY);

                    var width = (int)(maxX - minX + 1);
                    var height = (int)(maxY - minY + 1);

                    var location = new Point(minX, minY);
                    var size = new Point(width, height);

                    SingleEntryFactory
                        .CreateNewEntry(DimensionKeeperMod.EyeDropperTypeName, size, location)
                        .SynchronizeDimension();

                    Hide();
                }

                _startTileX = -1;
                _startTileY = -1;
                _lastMouseTileX = -1;
                _lastMouseTileY = -1;
                _justLeftMouseDown = false;
            }

            Main.LocalPlayer.showItemIcon = true;
        }

        internal void DrawEyeDropperBrush()
		{
            if (Main.LocalPlayer.mouseInterface)
                return;

            Vector2 upperLeft;
            Vector2 lowerRight;
            if (_leftMouseDown)
            {
                upperLeft = new Vector2(Math.Min(_startTileX, _lastMouseTileX), Math.Min(_startTileY, _lastMouseTileY));
                lowerRight = new Vector2(Math.Max(_startTileX, _lastMouseTileX) + 1, Math.Max(_startTileY, _lastMouseTileY) + 1);
            }
            else
            {
                upperLeft = Main.MouseWorld.ToTileCoordinates().ToVector2();
                lowerRight = Offset(upperLeft, 1, 1);
            }
            var upperLeftScreen = upperLeft * 16f;
            var lowerRightScreen = lowerRight * 16f;
            upperLeftScreen -= Main.screenPosition;
            lowerRightScreen -= Main.screenPosition;
            if (Math.Abs(Main.LocalPlayer.gravDir - (-1f)) < .005)
            {
                upperLeftScreen.Y = (float)Main.screenHeight - upperLeftScreen.Y;
                lowerRightScreen.Y = (float)Main.screenHeight - lowerRightScreen.Y;

                Utils.Swap(ref upperLeftScreen.Y, ref lowerRightScreen.Y);
            }

            DrawSelectedRectangle(upperLeftScreen, lowerRight - upperLeft, _leftMouseDown);
        }

        private static Color BuffColor(Color newColor, float r, float g, float b, float a)
        {
            newColor.R = (byte)((float)newColor.R * r);
            newColor.G = (byte)((float)newColor.G * g);
            newColor.B = (byte)((float)newColor.B * b);
            newColor.A = (byte)((float)newColor.A * a);
            return newColor;
        }

        private void DrawSelectedRectangle(Vector2 upperLeftScreen, Vector2 brushSize, bool drawBack = true)
		{
			var value = new Rectangle(0, 0, 1, 1);
            var scale = 0.6f;
			var color = BuffColor(Color.White, 1f, 0.9f, 0.1f, 1f);
			if (drawBack)
			{
				Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, 16f * brushSize, SpriteEffects.None, 0f);
			}

            scale = 1f;
			color = BuffColor(Color.White, 1f, 0.95f, 0.3f, 1f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitX * -2f, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(2f, 16f * brushSize.Y), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitX * 16f * brushSize.X, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(2f, 16f * brushSize.Y), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitY * -2f, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(16f * brushSize.X, 2f), SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(Main.magicPixel, upperLeftScreen + Vector2.UnitY * 16f * brushSize.Y, new Microsoft.Xna.Framework.Rectangle?(value), color * scale, 0f, Vector2.Zero, new Vector2(16f * brushSize.X, 2f), SpriteEffects.None, 0f);

            var pos = Offset(Main.MouseScreen, 48, 24);
			Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, $"{brushSize.X} x {brushSize.Y}", pos.X, pos.Y, Color.White, Color.Black, Vector2.Zero, 1f);
		}

        private Vector2 Offset(Vector2 position, float x, float y)
        {
            position.X += x;
            position.Y += y;
            return position;
        }
    }
}
