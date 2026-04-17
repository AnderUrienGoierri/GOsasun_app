using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    public class GOsasunForm : Form
    {
        private static Icon? _ikonoNagusia;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            EzarriAplikazioIkonoa();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EgokituPantailara();
        }

        protected override void OnShown(EventArgs e)
        {
            EzarriAplikazioIkonoa();
            base.OnShown(e);
            EgokituPantailara();
        }

        protected bool EgokituPantailara()
        {
            if (DiseinuModuan() || WindowState == FormWindowState.Minimized)
            {
                return false;
            }

            Rectangle lanEremua = Owner != null
                ? Screen.FromControl(Owner).WorkingArea
                : Screen.FromControl(this).WorkingArea;

            int gehienezkoZabalera = Math.Max(320, lanEremua.Width - 32);
            int gehienezkoAltuera = Math.Max(240, lanEremua.Height - 32);

            if (Width <= gehienezkoZabalera && Height <= gehienezkoAltuera)
            {
                return false;
            }

            float eskala = Math.Min((float)gehienezkoZabalera / Width, (float)gehienezkoAltuera / Height);
            if (eskala >= 1f)
            {
                return false;
            }

            SuspendLayout();
            try
            {
                Scale(new SizeF(eskala, eskala));

                if (BackgroundImage != null && BackgroundImageLayout == ImageLayout.None)
                {
                    BackgroundImageLayout = ImageLayout.Zoom;
                }

                if (Width > gehienezkoZabalera || Height > gehienezkoAltuera)
                {
                    Size = new Size(Math.Min(Width, gehienezkoZabalera), Math.Min(Height, gehienezkoAltuera));
                }
            }
            finally
            {
                ResumeLayout(true);
            }

            return true;
        }

        private void EzarriAplikazioIkonoa()
        {
            if (DiseinuModuan())
            {
                return;
            }

            Icon? ikonoa = LortuAplikazioIkonoa();
            if (ikonoa == null)
            {
                return;
            }

            Icon?.Dispose();
            Icon = ikonoa;
            ShowIcon = true;
        }

        private static Icon? LortuAplikazioIkonoa()
        {
            if (_ikonoNagusia != null)
            {
                return (Icon)_ikonoNagusia.Clone();
            }

            Icon? exeIkonoa = null;
            try
            {
                exeIkonoa = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                if (exeIkonoa != null)
                {
                    _ikonoNagusia = (Icon)exeIkonoa.Clone();
                    return (Icon)_ikonoNagusia.Clone();
                }
            }
            finally
            {
                exeIkonoa?.Dispose();
            }

            return null;
        }

        private bool DiseinuModuan()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode || Site?.DesignMode == true;
        }
    }
}