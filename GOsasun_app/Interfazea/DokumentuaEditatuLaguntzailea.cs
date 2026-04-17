using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class DokumentuaEditatuLaguntzailea : GOsasunForm
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DokumentuaEditatuLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public string? DokumentuIzena => string.IsNullOrWhiteSpace(txtDokumentuIzena.Text)
            ? null
            : txtDokumentuIzena.Text.Trim();

        public string? Deskribapena => string.IsNullOrWhiteSpace(txtDeskribapena.Text)
            ? null
            : txtDeskribapena.Text.Trim();

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

        public void Hasieratu(Dokumentua dokumentua)
        {
            txtDokumentuIzena.Text = dokumentua.DokumentuIzena ?? string.Empty;
            txtDeskribapena.Text = dokumentua.Deskribapena ?? string.Empty;
            txtDokumentuIzena.SelectionStart = 0;
            txtDokumentuIzena.SelectionLength = txtDokumentuIzena.TextLength;
            ActiveControl = txtDokumentuIzena;
        }
    }
}