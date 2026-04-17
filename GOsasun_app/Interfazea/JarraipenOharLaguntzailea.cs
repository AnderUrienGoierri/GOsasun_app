using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    public partial class JarraipenOharLaguntzailea : GOsasunForm
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public JarraipenOharLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnGordeOharra;
            CancelButton = btnOharrikGabe;
            txtOharra.PlaceholderText = "Idatzi nahi baduzu, jarraipen honen oharra hemen...";
        }

        public void EzarriEdukia(string izenburua, string azalpena)
        {
            Text = izenburua;
            lblAzalpena.Text = azalpena;
            txtOharra.Clear();
        }

        private void BtnOharrikGabe_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void BtnGordeOharra_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public static string? EskatuAukerakoOharra(IWin32Window? jabea, string izenburua, string azalpena)
        {
            using JarraipenOharLaguntzailea elkarrizketa = new JarraipenOharLaguntzailea();
            elkarrizketa.EzarriEdukia(izenburua, azalpena);

            DialogResult emaitza = jabea == null ? elkarrizketa.ShowDialog() : elkarrizketa.ShowDialog(jabea);
            if (emaitza != DialogResult.OK)
            {
                return null;
            }

            string testua = elkarrizketa.txtOharra.Text.Trim();
            return string.IsNullOrWhiteSpace(testua) ? null : testua;
        }

        public static string BatuOharrak(params string?[] zatiak)
        {
            return string.Join(Environment.NewLine, zatiak.Where(zatia => !string.IsNullOrWhiteSpace(zatia)).Select(zatia => zatia!.Trim()));
        }
    }
}