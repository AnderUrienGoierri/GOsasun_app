using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class JarraipenXehetasunakLaguntzailea : Form
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public JarraipenXehetasunakLaguntzailea()
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
            btnItxi.Click += (s, e) => Close();
        }

        public void Hasieratu(Jarraipena jarraipena, Jarraipena xehetasuna)
        {
            tlpXehetasunak.SuspendLayout();
            tlpXehetasunak.Controls.Clear();
            tlpXehetasunak.RowStyles.Clear();
            tlpXehetasunak.RowCount = 0;

            GehituXehetasunLerroa("Pazientea", jarraipena.PazienteIzenOsoa);
            GehituXehetasunLerroa("NAN/DNI", jarraipena.PazienteNan);
            GehituXehetasunLerroa("Erregistro data", xehetasuna.ErregistroData.ToString("g"));
            GehituXehetasunLerroa("Tentsio sistolikoa", BalioaTestuan(xehetasuna.TentsioSistolikoa));
            GehituXehetasunLerroa("Tentsio", BalioaTestuan(xehetasuna.TentsioDiastolikoa));
            GehituXehetasunLerroa("Pultsua", BalioaTestuan(xehetasuna.PultsuaPpm));
            GehituXehetasunLerroa("Pisua", BalioaTestuan(xehetasuna.PisuaKg, "N2", " kg"));
            GehituXehetasunLerroa("Altuera", BalioaTestuan(xehetasuna.Altuera, "N2", " m"));
            GehituXehetasunLerroa("XML bidea", xehetasuna.BideaZerbitzarian ?? "-");
            GehituXehetasunLerroa("Dokumentuak", jarraipena.DokumentuKopurua.ToString());
            tlpXehetasunak.ResumeLayout();

            txtOharrak.Text = xehetasuna.Oharrak ?? string.Empty;
        }

        private void GehituXehetasunLerroa(string etiketa, string balioa)
        {
            int row = tlpXehetasunak.RowCount++;
            tlpXehetasunak.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            Label lblEtiketa = new Label
            {
                Text = etiketa,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 8, 0)
            };

            Label lblBalioa = new Label
            {
                Text = balioa,
                Font = new Font("Segoe UI", 10F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            tlpXehetasunak.Controls.Add(lblEtiketa, 0, row);
            tlpXehetasunak.Controls.Add(lblBalioa, 1, row);
        }

        private static string BalioaTestuan<T>(T? balioa, string? formatua = null, string? atzizkia = null) where T : struct, IFormattable
        {
            if (!balioa.HasValue)
            {
                return "-";
            }

            string testua = balioa.Value.ToString(formatua, null);
            return atzizkia == null ? testua : testua + atzizkia;
        }
    }
}