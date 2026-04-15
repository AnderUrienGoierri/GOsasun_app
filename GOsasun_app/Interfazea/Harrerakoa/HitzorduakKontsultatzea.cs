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

        public HitzorduakKontsultatzea(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            _kontrolatzailea = new HitzorduKontrolatzailea();

            KonfiguratuTaula();
            KargatuDatuak();

            calEgutegia.DateChanged += CalEgutegia_DateChanged;
            btnGuztiak.Click += BtnGuztiak_Click;
        }

        private void KonfiguratuTaula()
        {
            dgvHitzorduak.AutoGenerateColumns = false;
            dgvHitzorduak.ColumnHeadersHeight = 40;
            dgvHitzorduak.RowTemplate.Height = 35;

            if (_erabiltzailea is OsasunLangilea)
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PazienteIzenOsoa", HeaderText = "Pazientea", Width = 400 });
            }
            else
            {
                dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OsasunLangileIzenOsoa", HeaderText = "Osasun langilea", Width = 400 });
            }
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Data", HeaderText = "Data", Width = 180, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HasieraOrdua", HeaderText = "Hasiera", Width = 140 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BukaeraOrdua", HeaderText = "Bukaera", Width = 140 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Arrazoia", HeaderText = "Arrazoia", Width = 450 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Egoera", HeaderText = "Egoera", Width = 150 });
        }

        private void KargatuDatuak()
        {
            try
            {
                if (_erabiltzailea is OsasunLangilea m)
                {
                    _hitzorduGuztiak = _kontrolatzailea.LortuOsasunLangilearenHitzorduak(m.Id);
                }
                else if (_erabiltzailea is Pazientea p)
                {
                    _hitzorduGuztiak = _kontrolatzailea.LortuPazientearenHitzorduak(p.Id);
                }

                HasieratuEgutegia();
                ErakutsiDatuak(_hitzorduGuztiak);
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

        private void CalEgutegia_DateChanged(object? sender, DateRangeEventArgs e)
        {
            var aukeratutakoak = _hitzorduGuztiak.Where(h => h.Data.Date >= e.Start.Date && h.Data.Date <= e.End.Date).ToList();
            ErakutsiDatuak(aukeratutakoak);
        }

        private void BtnGuztiak_Click(object? sender, EventArgs e)
        {
            ErakutsiDatuak(_hitzorduGuztiak);
        }

        private void _goiburuBarra_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuztiak_Click_1(object sender, EventArgs e)
        {

        }
    }
}
