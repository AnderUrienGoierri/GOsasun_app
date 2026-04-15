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
            
            // Izenburua aldatu rolaran arabera
            if (_erabiltzailea is HarrerakoLangilea)
            {
                lblIzenburua.Text = "PAZIENTEEN KUDEAKETA";
            }

            KonfiguratuTaula();
            KargatuPazienteak();

            // Gertaerak
            txtBilatu.TextChanged += TxtBilatu_TextChanged;
        }

        private void KonfiguratuTaula()
        {
            dgvPazienteak.AutoGenerateColumns = false;

            // Zutabeak definitu
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nan", HeaderText = "NAN", Name = "Nan", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Izena", HeaderText = "Izena", Name = "Izena", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Abizenak", HeaderText = "Abizenak", Name = "Abizenak", SortMode = DataGridViewColumnSortMode.Programmatic });
            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EgoeraKlinikoa", HeaderText = "Egoera", Name = "EgoeraKlinikoa", SortMode = DataGridViewColumnSortMode.Programmatic });

            // Editatu eta Ezabatu botoiak (Harrerako langilearentzat soilik agian? Erabiltzaileak harrerakoak eskatu du)
            if (_erabiltzailea is HarrerakoLangilea)
            {
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                btnEdit.HeaderText = "Akzioak";
                btnEdit.Text = "Editatu";
                btnEdit.Name = "btnEditatu";
                btnEdit.UseColumnTextForButtonValue = true;
                btnEdit.FlatStyle = FlatStyle.Flat;
                btnEdit.DefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                btnEdit.DefaultCellStyle.ForeColor = Color.White;
                dgvPazienteak.Columns.Add(btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.HeaderText = "";
                btnDelete.Text = "Ezabatu";
                btnDelete.Name = "btnEzabatu";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Flat;
                btnDelete.DefaultCellStyle.BackColor = Color.FromArgb(231, 76, 60);
                btnDelete.DefaultCellStyle.ForeColor = Color.White;
                dgvPazienteak.Columns.Add(btnDelete);
            }

            // Kurtsorea aldatu "esteka" efektua emateko
            dgvPazienteak.Cursor = Cursors.Default; // Kurtsorea lehenetsia grid osorako
            dgvPazienteak.CellMouseEnter += (s, e) => {
                if (e.RowIndex >= 0 && (e.ColumnIndex == dgvPazienteak.Columns["btnEditatu"]?.Index || e.ColumnIndex == dgvPazienteak.Columns["btnEzabatu"]?.Index))
                    dgvPazienteak.Cursor = Cursors.Hand;
                else
                    dgvPazienteak.Cursor = Cursors.Default;
            };

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
                if (_erabiltzailea is HarrerakoLangilea)
                {
                    _pazienteak = _kontrolatzailea.LortuGuztiakPazienteak(testua);
                }
                else
                {
                    _pazienteak = _kontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea!.Id, testua);
                }
                
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

        private void dgvPazienteak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var pazientea = dgvPazienteak.Rows[e.RowIndex].DataBoundItem as Pazientea;
            if (pazientea == null) return;

            // Editatu botoia
            if (dgvPazienteak.Columns[e.ColumnIndex].Name == "btnEditatu")
            {
                // Ireki informazio zehatza (editatzeko aukerarik badugu bertan)
                // Oraintxe bertan PazienteXehetasunak bakarrik erakusteko da, 
                // baina erabiltzaileari editatzen utzi nahi diogu.
                IrekiFormularioa(new PazienteXehetasunak(pazientea)); 
            }
            // Ezabatu botoia
            else if (dgvPazienteak.Columns[e.ColumnIndex].Name == "btnEzabatu")
            {
                var emaitza = MessageBox.Show($"Ziur zaude {pazientea.IzenOsoa} pazientea ezabatu (desaktibatu) nahi duzula?", 
                    "Berretsi ezabatzea", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (emaitza == DialogResult.Yes)
                {
                    if (_kontrolatzailea.EzabatuPazientea(pazientea.Id))
                    {
                        MessageBox.Show("Pazientea ondo desaktibatu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        KargatuPazienteak(txtBilatu.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Errorea gertatu da pazientea desaktibatzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
