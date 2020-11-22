using System;
using Terraria;

namespace DimensionKeeper.EyeDropperUI
{
    public class EyeDropperUpdater
    {
        private static EyeDropperUpdater _instance;

        public static EyeDropperUpdater Instance
        {
            get => _instance ?? (_instance = new EyeDropperUpdater());
            internal set => _instance = value;
        }

        public DimensionKeeper.EyeDropperUI.EyeDropperUI PaintTooltipUI;

        public EyeDropperUpdater()
        {
            if (!Main.dedServ)
            {
                PaintTooltipUI = new DimensionKeeper.EyeDropperUI.EyeDropperUI { Visible = false };
            }
        }

        public void UpdateMouseState()
        {
            PaintTooltipUI.UpdateMouseInput();

            if (PaintTooltipUI.Visible)
            {
                PaintTooltipUI.SetupMouseInterface();
            }
        }

		public void DrawUpdateEyeDropper()
        {
            if (!PaintTooltipUI.Visible) 
                return;

            try
            {
                PaintTooltipUI.UpdateEyeDropperCompute();
                PaintTooltipUI.DrawEyeDropperBrush();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
