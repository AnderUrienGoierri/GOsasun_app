using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class HitzorduKudeaketa : OinarriPantaila
    {
        private HitzorduKontrolatzailea _kontrolatzailea;
        private ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea;
        private List<Hitzordua> _hitzorduGuztiak = new List<Hitzordua>();
        private int _aukeratutakoHitzorduId = 0;

        public HitzorduKudeaketa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            _kontrolatzailea = new HitzorduKontrolatzailea();
            _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();

            KonfiguratuTaula();
            KargatuKonboak();
            KargatuDatuak();

            btnGorde.Click += BtnGorde_Click;
            btnEzabatu.Click += BtnEzabatu_Click;
            btnGarbitu.Click += (s, e) => GarbituPantaila();
            dgvHitzorduak.CellMouseDoubleClick += DgvHitzorduak_CellMouseDoubleClick;
        }

        private void KonfiguratuTaula()
        {
            dgvHitzorduak.AutoGenerateColumns = false;
            dgvHitzorduak.ColumnHeadersHeight = 40;
            dgvHitzorduak.RowTemplate.Height = 35;

            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PazienteIzenOsoa", HeaderText = "Pazientea", Width = 280 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MedikuIzenOsoa", HeaderText = "Medikua", Width = 280 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Data", HeaderText = "Data", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HasieraOrdua", HeaderText = "Hasiera", Width = 120 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Egoera", HeaderText = "Egoera", Width = 150 });
            dgvHitzorduak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Arrazoia", HeaderText = "Arrazoia", Width = 320 });
        }

        private void KargatuKonboak()
        {
            try
            {
                var pazienteak = _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak();
                cmbPazienteak.DataSource = pazienteak;
                cmbPazienteak.DisplayMember = "IzenOsoa";
                cmbPazienteak.ValueMember = "Id";

                var medikuak = _erabiltzaileKontrolatzailea.LortuGuztiakMedikuak();
                cmbMedikuak.DataSource = medikuak;
                cmbMedikuak.DisplayMember = "IzenOsoa";
                cmbMedikuak.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea zerrendak kargatzean: " + ex.Message);
            }
        }

        private void KargatuDatuak()
        {
            try
            {
                _hitzorduGuztiak = _kontrolatzailea.LortuHitzorduGuztiak();
                dgvHitzorduak.DataSource = null;
                dgvHitzorduak.DataSource = _hitzorduGuztiak;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GarbituPantaila()
        {
            _aukeratutakoHitzorduId = 0;
            txtArrazoia.Text = string.Empty;
            dtpData.Value = DateTime.Now;
            cmbEgoera.SelectedIndex = 0;
            if (cmbPazienteak.Items.Count > 0) cmbPazienteak.SelectedIndex = 0;
            if (cmbMedikuak.Items.Count > 0) cmbMedikuak.SelectedIndex = 0;
            btnGorde.Text = "Gertatu / Sortu Berria";
        }

        private void DgvHitzorduak_CellMouseDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var hitzordua = dgvHitzorduak.Rows[e.RowIndex].DataBoundItem as Hitzordua;
                if (hitzordua != null)
                {
                    _aukeratutakoHitzorduId = hitzordua.HitzorduId;
                    dtpData.Value = hitzordua.Data;
                    dtpHasiera.Value = DateTime.Today.Add(hitzordua.HasieraOrdua);
                    if (hitzordua.BukaeraOrdua.HasValue) dtpBukaera.Value = DateTime.Today.Add(hitzordua.BukaeraOrdua.Value);
                    txtArrazoia.Text = hitzordua.Arrazoia ?? "";
                    cmbEgoera.SelectedItem = hitzordua.Egoera;
                    cmbPazienteak.SelectedValue = hitzordua.PazienteId;
                    cmbMedikuak.SelectedValue = hitzordua.MedikuId;

                    btnGorde.Text = "Gorde Aldaketak";
                }
            }
        }

        private void BtnGorde_Click(object? sender, EventArgs e)
        {
            if (cmbPazienteak.SelectedValue == null || cmbMedikuak.SelectedValue == null)
            {
                MessageBox.Show("Pazientea eta Medikua aukeratu behar dira.");
                return;
            }

            try
            {
                Hitzordua h = new Hitzordua
                {
                    HitzorduId = _aukeratutakoHitzorduId,
                    PazienteId = (int)cmbPazienteak.SelectedValue,
                    MedikuId = (int)cmbMedikuak.SelectedValue,
                    Data = dtpData.Value.Date,
                    HasieraOrdua = dtpHasiera.Value.TimeOfDay,
                    BukaeraOrdua = dtpBukaera.Value.TimeOfDay,
                    Arrazoia = txtArrazoia.Text,
                    Egoera = cmbEgoera.SelectedItem?.ToString() ?? "Zain"
                };

                if (_aukeratutakoHitzorduId == 0)
                {
                    _kontrolatzailea.GehituHitzordua(h);
                    MessageBox.Show("Hitzordu berria sortu da.");
                }
                else
                {
                    _kontrolatzailea.EguneratuHitzordua(h);
                    MessageBox.Show("Hitzordua eguneratu da.");
                }

                GarbituPantaila();
                KargatuDatuak();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea gordetzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEzabatu_Click(object? sender, EventArgs e)
        {
            if (_aukeratutakoHitzorduId > 0)
            {
                var result = MessageBox.Show("Ziur zaude hitzordu hau ezabatu nahi duzula?", "Ezabatu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _kontrolatzailea.EzabatuHitzordua(_aukeratutakoHitzorduId);
                    GarbituPantaila();
                    KargatuDatuak();
                }
            }
        }

        private void _edukiPanela_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblBukaera_Click(object sender, EventArgs e)
        {

        }
    }
}
