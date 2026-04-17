using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    public partial class JarraipenOharrakLaguntzailea : GOsasunForm
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public JarraipenOharrakLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public string? Oharrak => string.IsNullOrWhiteSpace(txtOharrak.Text)
            ? null
            : txtOharrak.Text.Trim();

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnGorde;
            CancelButton = btnUtzi;

            btnGorde.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
        }

        public void Hasieratu(string izenburua, string? edukia)
        {
            Text = izenburua;
            txtOharrak.Text = edukia ?? string.Empty;
            txtOharrak.SelectionStart = 0;
            txtOharrak.SelectionLength = txtOharrak.TextLength;
            ActiveControl = txtOharrak;
        }
    }
}