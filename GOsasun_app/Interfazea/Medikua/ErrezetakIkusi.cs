using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Interfazea
{
    public partial class ErrezetakIkusi : OinarriPantaila
    {
        private ErrezetaDB errezetaDB = new ErrezetaDB();
        private List<Errezeta> errezetakGuztiak = new List<Errezeta>();
        private DateTime? filtroData = null;

        private BindingSource bsErrezetak = new BindingSource();
        private BindingSource bsBotikak = new BindingSource();

        private class ErrezetaGridItem
        {
            public int ErrezetaId { get; set; }
            public string? Pazientea { get; set; }
            public string? NAN { get; set; }
            public string? Diagnostikoa { get; set; }
            public string? IgorpenData { get; set; }
            public string? Hitzordua { get; set; }
        }

        public ErrezetakIkusi() : base()
        {
            InitializeComponent();
        }

        public ErrezetakIkusi(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            dgvErrezetak.DataSource = bsErrezetak;
            dgvBotikak.DataSource = bsBotikak;
            KargatuGertaerak();
            KargatuDatuak();
        }

        private void KargatuGertaerak()
        {
            txtBilatuPaz.TextChanged += TxtBilatuPaz_TextChanged;
            mcDataFiltroa.DateSelected += McDataFiltroa_DateSelected;
            btnGarbituFiltroak.Click += BtnGarbituFiltroak_Click;
            dgvErrezetak.SelectionChanged += DgvErrezetak_SelectionChanged;
            btnEditatu.Click += BtnEditatu_Click;
            btnEzabatu.Click += BtnEzabatu_Click;
        }

        private void DgvErrezetak_SelectionChanged(object? sender, EventArgs e)
        {
            EguneratuBotikaGrid();
        }

        private void EguneratuBotikaGrid()
        {
            bsBotikak.DataSource = null;
            
            if (dgvErrezetak.SelectedRows.Count == 0) return;

            var cellValue = dgvErrezetak.SelectedRows[0].Cells["ErrezetaId"].Value;
            if (cellValue == null) return;
            
            int id = Convert.ToInt32(cellValue);
            var aurkitua = errezetakGuztiak.FirstOrDefault(x => x.ErrezetaId == id);
            
            if (aurkitua != null && aurkitua.Botikak != null && aurkitua.Botikak.Count > 0)
            {
                bsBotikak.DataSource = aurkitua.Botikak.Select(b => new {
                    Botika = b.BotikaIzena,
                    b.Dosia,
                    b.Maiztasuna
                }).ToList();
                
                if (dgvBotikak.Columns.Count > 0)
                {
                    dgvBotikak.Columns["Botika"].HeaderText = "Botika";
                    dgvBotikak.Columns["Dosia"].HeaderText = "Dosia";
                    dgvBotikak.Columns["Maiztasuna"].HeaderText = "Maiztasuna";
                }
            }
        }

        public void KargatuDatuak()
        {
            if (_erabiltzailea != null)
            {
                errezetakGuztiak = errezetaDB.LortuMedikuarenErrezetak(_erabiltzailea.Id);
                IragaziDatuak();
            }
        }

        private void TxtBilatuPaz_TextChanged(object? sender, EventArgs e)
        {
            IragaziDatuak();
        }

        private void McDataFiltroa_DateSelected(object? sender, DateRangeEventArgs e)
        {
            filtroData = e.Start.Date;
            IragaziDatuak();
        }

        private void BtnGarbituFiltroak_Click(object? sender, EventArgs e)
        {
            txtBilatuPaz.Clear();
            filtroData = null;
            // Egin behar dugu reset MonthCalendar-i
            mcDataFiltroa.SelectionStart = DateTime.Today;
            mcDataFiltroa.SelectionEnd = DateTime.Today;
            IragaziDatuak();
        }

        private void IragaziDatuak()
        {
            string query = txtBilatuPaz.Text.ToLower();
            var iragazita = errezetakGuztiak.Where(e => 
                (string.IsNullOrEmpty(query) || 
                 (e.PazienteNan != null && e.PazienteNan.ToLower().Contains(query)) ||
                 (e.PazienteIzenOsoa != null && e.PazienteIzenOsoa.ToLower().Contains(query)))
                &&
                (!filtroData.HasValue || 
                 e.IgorpenData.Date == filtroData.Value || 
                 (e.HitzorduData.HasValue && e.HitzorduData.Value.Date == filtroData.Value))
            ).ToList();

            dgvErrezetak.CurrentCell = null;
            bsErrezetak.DataSource = null;

            if (iragazita.Count > 0)
            {
                var dtoList = iragazita.Select(e => new ErrezetaGridItem {
                    ErrezetaId = e.ErrezetaId,
                    Pazientea = e.PazienteIzenOsoa,
                    NAN = e.PazienteNan,
                    Diagnostikoa = e.Diagnostikoa,
                    IgorpenData = e.IgorpenData.ToShortDateString(),
                    Hitzordua = e.HitzorduData.HasValue ? e.HitzorduData.Value.ToShortDateString() : "-"
                }).ToList();

                bsErrezetak.DataSource = dtoList;

                if (dgvErrezetak.Columns.Count > 0)
                {
                    dgvErrezetak.Columns["ErrezetaId"].Visible = false;
                }
            }

            EguneratuBotikaGrid();
        }

        private void BtnEditatu_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvErrezetak.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Aukeratu errezeta bat mesedez.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cellValue = dgvErrezetak.SelectedRows[0].Cells["ErrezetaId"].Value;
                if (cellValue == null) return;

                int id = Convert.ToInt32(cellValue);
                var aurkitua = errezetakGuztiak.FirstOrDefault(x => x.ErrezetaId == id);
                
                if (aurkitua != null && _erabiltzailea != null)
                {
                    var editForm = new ErrezetaSortu(_erabiltzailea, aurkitua);
                    editForm.FormClosed += (s, args) => { this.Show(); KargatuDatuak(); };
                    this.Hide();
                    editForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea editatzean: {ex.Message}\n{ex.StackTrace}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEzabatu_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvErrezetak.SelectedRows.Count == 0) return;

                var cellValue = dgvErrezetak.SelectedRows[0].Cells["ErrezetaId"].Value;
                if (cellValue == null) return;

                int id = Convert.ToInt32(cellValue);

                var result = MessageBox.Show(
                    "Ziur zaude errezeta hau eta lotutako botikak ezabatu nahi dituzula?", 
                    "Ezabatu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if(errezetaDB.EzabatuErrezeta(id))
                    {
                        MessageBox.Show("Errezeta ongi ezabatu da.");
                        KargatuDatuak();
                    }
                    else
                    {
                        MessageBox.Show("Arazo bat egon da.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea ezabatzean: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
