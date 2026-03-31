using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOsasun_app.Modeloak;
using GOsasun_app.Kontrolatzaileak;

namespace GOsasun_app.Formularioak
{
    public partial class NireNeurketakFormularioa : OinarriFormularioa
    {
        private readonly NeurketaKontrolatzailea _neurketaKontrolatzailea = new NeurketaKontrolatzailea();

        public NireNeurketakFormularioa() : base()
        {
            InitializeComponent();
        }

        public NireNeurketakFormularioa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
            HasieratuDatuak();
        }

        private void KonfiguratuGertaerak()
        {
            btnNeurketaBerria.Click += (s, e) => IrekiFormularioa(new NeurketaMotakFormularioa(_erabiltzailea!));
            
            // Filtratze gertaerak
            dtpBilatuData.ValueChanged += (s, e) => KargatuHistoriala(dtpBilatuData.Value.Date);
            btnGarbituFiltroa.Click += (s, e) => {
                dtpBilatuData.Value = DateTime.Now;
                KargatuHistoriala();
            };
        }

        private void HasieratuDatuak()
        {
            KargatuHistoriala();
        }

        private void KargatuHistoriala(DateTime? dataFiltroa = null)
        {
            if (_erabiltzailea == null) return;

            try
            {
                var neurketak = _neurketaKontrolatzailea.LortuPazientearenNeurketak(_erabiltzailea.Id);

                // Dataren arabera filtratu behar bada
                if (dataFiltroa.HasValue)
                {
                    neurketak = neurketak.Where(n => n.ErregistroData.Date == dataFiltroa.Value).ToList();
                }

                // DataTable bat erabili dgv-rako, ordenazio eta filtrazio hobea izateko
                DataTable dt = new DataTable();
                dt.Columns.Add("Data", typeof(DateTime));
                dt.Columns.Add("Sistole", typeof(int));
                dt.Columns.Add("Diastole", typeof(int));
                dt.Columns.Add("Pultsua", typeof(int));
                dt.Columns.Add("Pisua", typeof(decimal));
                dt.Columns.Add("Altuera", typeof(decimal));
                dt.Columns.Add("Sintomak", typeof(string));

                foreach (var n in neurketak)
                {
                    dt.Rows.Add(
                        n.ErregistroData,
                        (object?)n.TentsioSistolikoa ?? DBNull.Value,
                        (object?)n.TentsioDiastolikoa ?? DBNull.Value,
                        (object?)n.PultsuaPpm ?? DBNull.Value,
                        (object?)n.PisuaKg ?? DBNull.Value,
                        (object?)n.Altuera ?? DBNull.Value,
                        n.Sintomak ?? ""
                    );
                }

                dgvHistoriala.DataSource = dt;

                // Zutabeen izenak eta formatua
                if (dgvHistoriala.Columns.Count > 0)
                {
                    dgvHistoriala.Columns["Data"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
                    dgvHistoriala.Columns["Data"].HeaderText = "Data eta Ordua";
                    dgvHistoriala.Columns["Sistole"].HeaderText = "Sistole (mmHg)";
                    dgvHistoriala.Columns["Diastole"].HeaderText = "Diastole (mmHg)";
                    dgvHistoriala.Columns["Pultsua"].HeaderText = "Pultsua (ppm)";
                    dgvHistoriala.Columns["Pisua"].HeaderText = "Pisua (kg)";
                    dgvHistoriala.Columns["Altuera"].HeaderText = "Altuera (cm)";

                    // Null balioen bistaratzea
                    foreach (DataGridViewColumn col in dgvHistoriala.Columns)
                    {
                        col.DefaultCellStyle.NullValue = "-";
                        col.SortMode = DataGridViewColumnSortMode.Automatic;
                    }

                    // Zabalera doikuntzak
                    dgvHistoriala.Columns["Data"].Width = 280;
                    dgvHistoriala.Columns["Sintomak"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvHistoriala.Columns["Sintomak"].MinimumWidth = 200;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea historiala kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => {
                this.Show();
                KargatuHistoriala(); // Freskatu zerrenda itzultzean
            };
            this.Hide();
            formularioa.Show();
        }
    }
}
