using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class DokumentuaEzabatuLaguntzailea : Form
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DokumentuaEzabatuLaguntzailea()
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
            AcceptButton = btnBai;
            CancelButton = btnEz;
            pbAbisua.Image = SystemIcons.Warning.ToBitmap();
            btnBai.Click += (s, e) =>
            {
                DialogResult = DialogResult.Yes;
                Close();
            };
            btnEz.Click += (s, e) =>
            {
                DialogResult = DialogResult.No;
                Close();
            };
        }

        public void Hasieratu(Dokumentua dokumentua)
        {
            string izena = dokumentua.DokumentuIzena ?? dokumentua.FitxategiIzena ?? "dokumentua";
            lblGaldera.Text = $"Ziur zaude '{izena}' dokumentua ezabatu nahi duzula?";
        }

        public static bool Baieztatu(IWin32Window? jabea, Dokumentua dokumentua)
        {
            using DokumentuaEzabatuLaguntzailea elkarrizketa = new DokumentuaEzabatuLaguntzailea();
            elkarrizketa.Hasieratu(dokumentua);
            DialogResult emaitza = jabea == null ? elkarrizketa.ShowDialog() : elkarrizketa.ShowDialog(jabea);
            return emaitza == DialogResult.Yes;
        }

        private void btnEz_Click(object sender, EventArgs e)
        {

        }
    }
}