using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Medikuari esleitutako pazienteen zerrenda taula bidez erakusten duen formularioa.
    /// </summary>
    public partial class PazienteenZerrenda : OinarriPantaila
    {
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;
        private List<Pazientea> _pazienteak = new List<Pazientea>();

        public PazienteenZerrenda(Erabiltzailea medikua)
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
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nan", HeaderText = "NAN", Name = "Nan", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Izena", HeaderText = "Izena", Name = "Izena", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Abizenak", HeaderText = "Abizenak", Name = "Abizenak", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EgoeraKlinikoa", HeaderText = "Egoera", Name = "EgoeraKlinikoa", SortMode = DataGridViewColumnSortMode.Programmatic });

            // Kurtsorea aldatu "esteka" efektua emateko
            dgvPazienteak.Cursor = Cursors.Hand;

            // Ordenazioa gaitu (header click)
            dgvPazienteak.ColumnHeaderMouseClick += DgvPazienteak_ColumnHeaderMouseClick;
        }

        // Ordenatzeko aldagaiak
        private string _azkenOrdenazioZutabea = "";
        private bool _ordenazioGorakorra = true;

        private void DgvPazienteak_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_pazienteak == null || _pazienteak.Count == 0 || e.ColumnIndex < 0) return;

            string kolumnaIzena = dgvPazienteak.Columns[e.ColumnIndex].DataPropertyName;

            if (_azkenOrdenazioZutabea == kolumnaIzena)
            {
                _ordenazioGorakorra = !_ordenazioGorakorra; // Alderantzikatu
            }
            else
            {
                _azkenOrdenazioZutabea = kolumnaIzena;
                _ordenazioGorakorra = true;
            }

            var unekoPazienteak = _pazienteak.AsEnumerable();
            var pi = typeof(Pazientea).GetProperty(kolumnaIzena);

            if (pi != null)
            {
                if (_ordenazioGorakorra)
                    _pazienteak = unekoPazienteak.OrderBy(x => pi.GetValue(x, null)).ToList();
                else
                    _pazienteak = unekoPazienteak.OrderByDescending(x => pi.GetValue(x, null)).ToList();

                dgvPazienteak.DataSource = null;
                dgvPazienteak.DataSource = _pazienteak;

                // Sort glyphs (gezi txikiak) aktibatzeko
                foreach (DataGridViewColumn col in dgvPazienteak.Columns)
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;

                dgvPazienteak.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
            }
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
                    IrekiFormularioa(new PazienteXehetasunak(pazientea));
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
