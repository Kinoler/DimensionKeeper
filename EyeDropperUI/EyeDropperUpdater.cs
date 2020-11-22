using System;
using Terraria;

namespace DimensionKeeper.EyeDropperUI
{
    public class EyeDropperUpdater
    {
        private static EyeDropperUpdater _instance;

        internal static EyeDropperUpdater Instance
        {
            get => _instance ?? (_instance = new EyeDropperUpdater());
            set => _instance = value;
        }

        public static bool Visible => Instance.UIEyeDropper.Visible;

        public static void Show()
        {
            Instance.UIEyeDropper.Show();
        }

        public static void Hide()
        {
            Instance.UIEyeDropper.Show();
        }

        internal DimensionKeeper.EyeDropperUI.EyeDropperUI UIEyeDropper;

        internal EyeDropperUpdater()
        {
            if (!Main.dedServ)
            {
                UIEyeDropper = new DimensionKeeper.EyeDropperUI.EyeDropperUI { Visible = false };
            }
        }

        internal void UpdateMouseState()
        {
            UIEyeDropper.UpdateMouseInput();

            if (UIEyeDropper.Visible)
            {
                UIEyeDropper.SetupMouseInterface();
            }
        }

		internal void DrawUpdateEyeDropper()
        {
            if (!UIEyeDropper.Visible) 
                return;

            try
            {
                UIEyeDropper.UpdateEyeDropperCompute();
                UIEyeDropper.DrawEyeDropperBrush();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
