using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class HitzorduakKontsultatzea : OinarriPantaila
    {
        private readonly HitzorduKontrolatzailea _kontrolatzailea;
        private readonly int? _pazienteIragazkiId;
        private List<Hitzordua> _hitzorduGuztiak = new List<Hitzordua>();
        private bool _dataIragazkiaAldiBaterakoKendu;
        private bool _hasierakoDatuakKargatuta;
        private bool _hasierakoDatuakKargatzen;

        public HitzorduakKontsultatzea(Erabiltzailea erabiltzailea, int? pazienteIragazkiId = null) : base(erabiltzailea)
        {
            InitializeComponent();
            _kontrolatzailea = new HitzorduKontrolatzailea();
            _pazienteIragazkiId = pazienteIragazkiId;

            KonfiguratuTaula();

            calEgutegia.DateChanged += CalEgutegia_DateChanged;
            btnGuztiak.Click += BtnGuztiak_Click;
            txtPazienteBilatu.TextChanged += TxtPazienteBilatu_TextChanged;
            chkPazienteGuztiak.CheckedChanged += ChkPazienteGuztiak_CheckedChanged;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_hasierakoDatuakKargatuta || _hasierakoDatuakKargatzen || DiseinuModuan())
            {
                return;
            }

            await Task.Yield();
            await KargatuHasierakoDatuakAsync();
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

            bool osasunLangileIkuspegia = _pazienteIragazkiId is null && _erabiltzailea is OsasunLangilea;

            if (osasunLangileIkuspegia)
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PazienteIzenOsoa",
                    HeaderText = "Pazientea",
                    FillWeight = 19,
                    MinimumWidth = 150
                });
            }
            else
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OsasunLangileIzenOsoa",
                    HeaderText = "Osasun langilea",
                    FillWeight = 19,
                    MinimumWidth = 150
                });
            }

            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Data",
                HeaderText = "Data",
                FillWeight = 9,
                MinimumWidth = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HasieraOrdua",
                HeaderText = "Hasiera",
                FillWeight = 8,
                MinimumWidth = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BukaeraOrdua",
                HeaderText = "Bukaera",
                FillWeight = 8,
                MinimumWidth = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm", NullValue = "-" }
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Arrazoia",
                HeaderText = "Arrazoia",
                FillWeight = 40,
                MinimumWidth = 180
            });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Egoera",
                HeaderText = "Egoera",
                FillWeight = 10,
                MinimumWidth = 85
            });

            bool osasunLangileaDa = osasunLangileIkuspegia;
            lblBilatuPazientea.Visible = osasunLangileaDa;
            txtPazienteBilatu.Visible = osasunLangileaDa;
            chkPazienteGuztiak.Visible = osasunLangileaDa;
        }

        private void KargatuDatuak()
        {
            try
            {
                bool pazienteGuztiak = chkPazienteGuztiak.Checked;
                List<Hitzordua> hitzorduak = LortuHitzorduak(pazienteGuztiak);
                AplikatuHitzorduak(hitzorduak);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea hitzorduak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task KargatuHasierakoDatuakAsync()
        {
            _hasierakoDatuakKargatzen = true;
            EzarriHasierakoKargaEgoera(true);

            try
            {
                bool pazienteGuztiak = chkPazienteGuztiak.Checked;
                List<Hitzordua> hitzorduak = await Task.Run(() => LortuHitzorduak(pazienteGuztiak));

                if (IsDisposed)
                {
                    return;
                }

                AplikatuHitzorduak(hitzorduak);
                _hasierakoDatuakKargatuta = true;
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show("Errorea hitzorduak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _hasierakoDatuakKargatzen = false;
                if (!IsDisposed)
                {
                    EzarriHasierakoKargaEgoera(false);
                }
            }
        }

        private void EzarriHasierakoKargaEgoera(bool kargatzen)
        {
            UseWaitCursor = kargatzen;
            Cursor = kargatzen ? Cursors.WaitCursor : Cursors.Default;
            dgvHitzorduak.Enabled = !kargatzen;
            calEgutegia.Enabled = !kargatzen;
            btnGuztiak.Enabled = !kargatzen;
            txtPazienteBilatu.Enabled = !kargatzen;
            chkPazienteGuztiak.Enabled = !kargatzen;
        }

        private List<Hitzordua> LortuHitzorduak(bool pazienteGuztiak)
        {
            if (_pazienteIragazkiId.HasValue)
            {
                return _kontrolatzailea.LortuPazientearenHitzorduak(_pazienteIragazkiId.Value);
            }

            if (_erabiltzailea is OsasunLangilea m)
            {
                return pazienteGuztiak
                    ? _kontrolatzailea.LortuHitzorduGuztiak()
                    : _kontrolatzailea.LortuOsasunLangilearenHitzorduak(m.Id);
            }

            if (_erabiltzailea is Pazientea p)
            {
                return _kontrolatzailea.LortuPazientearenHitzorduak(p.Id);
            }

            return new List<Hitzordua>();
        }

        private void AplikatuHitzorduak(List<Hitzordua> hitzorduak)
        {
            _hitzorduGuztiak = hitzorduak;
            HasieratuEgutegia();
            AplikatuIragazkiak();
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

        private void lblIzenburua_Click(object sender, EventArgs e)
        {

        }
    }
}
