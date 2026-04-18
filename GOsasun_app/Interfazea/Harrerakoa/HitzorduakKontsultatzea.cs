using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class HitzorduakKontsultatzea : OinarriPantaila
    {
        private readonly HitzorduKontrolatzailea _kontrolatzailea;
        private List<Hitzordua> _hitzorduGuztiak = new List<Hitzordua>();
        private bool _dataIragazkiaAldiBaterakoKendu;

        public HitzorduakKontsultatzea(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            _kontrolatzailea = new HitzorduKontrolatzailea();

            KonfiguratuTaula();
            KargatuDatuak();

            calEgutegia.DateChanged += CalEgutegia_DateChanged;
            btnGuztiak.Click += BtnGuztiak_Click;
            txtPazienteBilatu.TextChanged += TxtPazienteBilatu_TextChanged;
            chkPazienteGuztiak.CheckedChanged += ChkPazienteGuztiak_CheckedChanged;
        }

        private void KonfiguratuTaula()
        {
            dgvHitzorduak.Columns.Clear();
            dgvHitzorduak.AutoGenerateColumns = false;
            dgvHitzorduak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHitzorduak.ColumnHeadersHeight = 40;
            dgvHitzorduak.RowTemplate.Height = 35;
            dgvHitzorduak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHitzorduak.ScrollBars = ScrollBars.Vertical;

            if (_erabiltzailea is OsasunLangilea)
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PazienteIzenOsoa",
                    HeaderText = "Pazientea",
                    FillWeight = 26,
                    MinimumWidth = 190
                });
            }
            else
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OsasunLangileIzenOsoa",
                    HeaderText = "Osasun langilea",
                    FillWeight = 26,
                    MinimumWidth = 190
                });
            }

            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Data",
                HeaderText = "Data",
                FillWeight = 13,
                MinimumWidth = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HasieraOrdua",
                HeaderText = "Hasiera",
                FillWeight = 10,
                MinimumWidth = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BukaeraOrdua",
                HeaderText = "Bukaera",
                FillWeight = 10,
                MinimumWidth = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm", NullValue = "-" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Arrazoia",
                HeaderText = "Arrazoia",
                FillWeight = 27,
                MinimumWidth = 180
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Egoera",
                HeaderText = "Egoera",
                FillWeight = 14,
                MinimumWidth = 105
            });

            bool osasunLangileaDa = _erabiltzailea is OsasunLangilea;
            lblBilatuPazientea.Visible = osasunLangileaDa;
            txtPazienteBilatu.Visible = osasunLangileaDa;
            chkPazienteGuztiak.Visible = osasunLangileaDa;
        }

        private void KargatuDatuak()
        {
            try
            {
                if (_erabiltzailea is OsasunLangilea m)
                {
                    _hitzorduGuztiak = chkPazienteGuztiak.Checked
                        ? _kontrolatzailea.LortuHitzorduGuztiak()
                        : _kontrolatzailea.LortuOsasunLangilearenHitzorduak(m.Id);
                }
                else if (_erabiltzailea is Pazientea p)
                {
                    _hitzorduGuztiak = _kontrolatzailea.LortuPazientearenHitzorduak(p.Id);
                }

                HasieratuEgutegia();
                AplikatuIragazkiak();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea hitzorduak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HasieratuEgutegia()
        {
            var egunMarkatuak = _hitzorduGuztiak.Select(h => h.Data.Date).Distinct().ToArray();
            calEgutegia.BoldedDates = egunMarkatuak;
        }

        private void ErakutsiDatuak(List<Hitzordua> datuak)
        {
            dgvHitzorduak.DataSource = null;
            dgvHitzorduak.DataSource = datuak;
        }

        private void AplikatuIragazkiak()
        {
            IEnumerable<Hitzordua> emaitzak = _hitzorduGuztiak;

            if (_erabiltzailea is OsasunLangilea)
            {
                string bilaketa = txtPazienteBilatu.Text.Trim();
                if (!string.IsNullOrWhiteSpace(bilaketa))
                {
                    emaitzak = emaitzak.Where(h => BatDatorBilaketarekin(h, bilaketa));
                }
            }

            if (!_dataIragazkiaAldiBaterakoKendu)
            {
                DateTime hasiera = calEgutegia.SelectionStart.Date;
                DateTime amaiera = calEgutegia.SelectionEnd.Date;
                emaitzak = emaitzak.Where(h => h.Data.Date >= hasiera && h.Data.Date <= amaiera);
            }

            ErakutsiDatuak(emaitzak
                .OrderByDescending(h => h.Data)
                .ThenByDescending(h => h.HasieraOrdua)
                .ToList());
        }

        private static bool BatDatorBilaketarekin(Hitzordua hitzordua, string bilaketa)
        {
            return BalioaDauka(hitzordua.PazienteAbizenak, bilaketa)
                || BalioaDauka(hitzordua.PazienteIzena, bilaketa)
                || BalioaDauka(hitzordua.PazienteIzenOsoa, bilaketa)
                || BalioaDauka(hitzordua.PazienteNan, bilaketa);
        }

        private static bool BalioaDauka(string? testua, string bilaketa)
        {
            return !string.IsNullOrWhiteSpace(testua)
                && testua.Contains(bilaketa, StringComparison.OrdinalIgnoreCase);
        }

        private void CalEgutegia_DateChanged(object? sender, DateRangeEventArgs e)
        {
            _dataIragazkiaAldiBaterakoKendu = false;
            AplikatuIragazkiak();
        }

        private void BtnGuztiak_Click(object? sender, EventArgs e)
        {
            _dataIragazkiaAldiBaterakoKendu = true;
            AplikatuIragazkiak();
        }

        private void TxtPazienteBilatu_TextChanged(object? sender, EventArgs e)
        {
            AplikatuIragazkiak();
        }

        private void ChkPazienteGuztiak_CheckedChanged(object? sender, EventArgs e)
        {
            KargatuDatuak();
        }

        private void _goiburuBarra_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuztiak_Click_1(object sender, EventArgs e)
        {

        }
    }
}
