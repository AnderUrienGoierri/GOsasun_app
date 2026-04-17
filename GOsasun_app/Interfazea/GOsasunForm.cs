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

        protected override void OnShown(EventArgs e)
        {
            EzarriAplikazioIkonoa();
            base.OnShown(e);
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