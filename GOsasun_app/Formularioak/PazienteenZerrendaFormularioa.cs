using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;
using GOsasun_app.Kontrolatzaileak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Medikuari esleitutako pazienteen zerrenda taula bidez erakusten duen formularioa.
    /// </summary>
    public partial class PazienteenZerrendaFormularioa : OinarriFormularioa
    {
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;
        private List<Pazientea> _pazienteak = new List<Pazientea>();

        public PazienteenZerrendaFormularioa(Erabiltzailea medikua)
            : base(medikua)
        {
            InitializeComponent();
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            KonfiguratuTaula();
            KargatuPazienteak();

            // Gertaerak
            txtBilatu.TextChanged += TxtBilatu_TextChanged;
            dgvPazienteak.CellDoubleClick += DgvPazienteak_CellDoubleClick;
        }

        private void KonfiguratuTaula()
        {
            dgvPazienteak.AutoGenerateColumns = false;

            // Zutabeak definitu
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nan", HeaderText = "NAN", Name = "Nan" });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Izena", HeaderText = "Izena", Name = "Izena" });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Abizenak", HeaderText = "Abizenak", Name = "Abizenak" });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EgoeraKlinikoa", HeaderText = "Egoera", Name = "Egoera" });

            // Kurtsorea aldatu "esteka" efektua emateko
            dgvPazienteak.Cursor = Cursors.Hand;
        }

        private void KargatuPazienteak(string? testua = null)
        {
            try
            {
                _pazienteak = _kontrolatzailea.LortuMedikuarenPazienteak(_erabiltzailea!.Id, testua);
                dgvPazienteak.DataSource = null;
                dgvPazienteak.DataSource = _pazienteak;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBilatu_TextChanged(object? sender, EventArgs e)
        {
            // DB mailan bilaketa egiten dugu (erabiltzaileak eskatu bezala)
            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private void DgvPazienteak_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var pazientea = dgvPazienteak.Rows[e.RowIndex].DataBoundItem as Pazientea;
                if (pazientea != null)
                {
                    // Xehetasun pantaila ireki (sortzeko dago)
                    IrekiFormularioa(new PazienteXehetasunakFormularioa(pazientea));
                }
            }
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void dgvPazienteak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
