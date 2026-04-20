using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErrezetakIkusi : OinarriPantaila
    {
        private readonly ErrezetaKontrolatzailea errezetaKontrolatzailea = new ErrezetaKontrolatzailea();
        private List<Errezeta> errezetakGuztiak = new List<Errezeta>();
        private DateTime? filtroData = null;

        private readonly BindingSource bsErrezetak = new BindingSource();
        private readonly BindingSource bsBotikak = new BindingSource();

        private bool ErrezetaKudeaketaBaimenduta => _erabiltzailea is OsasunLangilea;

        private class ErrezetaGridItem
        {
            public int ErrezetaId { get; set; }
            public string? Pazientea { get; set; }
            public string? NAN { get; set; }
            public string? Aktibo { get; set; }
            public string? Diagnostikoa { get; set; }
            public string? IgorpenData { get; set; }
            public string? Hitzordua { get; set; }
        }

        public ErrezetakIkusi() : base()
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        public ErrezetakIkusi(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        private void HasieratuPantaila()
        {
            dgvErrezetak.DataSource = bsErrezetak;
            dgvBotikak.DataSource = bsBotikak;
            chkPazienteGuztiak.Checked = false;
            chkErrezetaAktiboak.Checked = false;
            KonfiguratuIkuspegiaErabiltzailearenArabera();
            KargatuGertaerak();

            if (_erabiltzailea != null)
            {
                KargatuDatuak();
            }
        }

        private void KonfiguratuIkuspegiaErabiltzailearenArabera()
        {
            lblFiltroa.Visible = ErrezetaKudeaketaBaimenduta;
            txtBilatuPaz.Visible = ErrezetaKudeaketaBaimenduta;
            chkPazienteGuztiak.Visible = ErrezetaKudeaketaBaimenduta;
            btnEditatu.Visible = ErrezetaKudeaketaBaimenduta;
            btnEzabatu.Visible = ErrezetaKudeaketaBaimenduta;

            if (ErrezetaKudeaketaBaimenduta)
            {
                lblEgutegia.Location = new Point(30, 198);
                mcDataFiltroa.Location = new Point(30, 254);
                chkErrezetaAktiboak.Location = new Point(20, 490);
                btnGarbituFiltroak.Location = new Point(20, 540);
                btnEditatu.Location = new Point(20, 610);
                btnEzabatu.Location = new Point(20, btnEditatu.Bottom + 12);
                return;
            }

            lblEgutegia.Location = new Point(20, 0);
            mcDataFiltroa.Location = new Point(20, 56);
            chkErrezetaAktiboak.Location = new Point(20, 292);
            btnGarbituFiltroak.Location = new Point(20, 342);
        }

        private void KargatuGertaerak()
        {
            txtBilatuPaz.TextChanged += TxtBilatuPaz_TextChanged;
            mcDataFiltroa.DateSelected += McDataFiltroa_DateSelected;
            chkErrezetaAktiboak.CheckedChanged += ChkErrezetaAktiboak_CheckedChanged;
            btnGarbituFiltroak.Click += BtnGarbituFiltroak_Click;
            chkPazienteGuztiak.CheckedChanged += ChkPazienteGuztiak_CheckedChanged;
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
                bsBotikak.DataSource = aurkitua.Botikak.Select(b => new
                {
                    Botika = b.BotikaIzena,
                    b.Dosia,
                    b.Maiztasuna
                }).ToList();

                if (dgvBotikak.Columns.Count > 0)
                {
                    if (dgvBotikak.Columns["Botika"] is DataGridViewColumn botikaZutabea)
                    {
                        botikaZutabea.HeaderText = "Botika";
                    }

                    if (dgvBotikak.Columns["Dosia"] is DataGridViewColumn dosiZutabea)
                    {
                        dosiZutabea.HeaderText = "Dosia";
                    }

                    if (dgvBotikak.Columns["Maiztasuna"] is DataGridViewColumn maiztasunZutabea)
                    {
                        maiztasunZutabea.HeaderText = "Maiztasuna";
                    }
                }
            }
        }

        public void KargatuDatuak()
        {
            if (_erabiltzailea is OsasunLangilea osasunLangilea)
            {
                errezetakGuztiak = chkPazienteGuztiak.Checked
                    ? errezetaKontrolatzailea.LortuErrezetaGuztiak(false)
                    : errezetaKontrolatzailea.LortuOsasunLangilearenErrezetak(osasunLangilea.Id, false);
                IragaziDatuak();
            }
            else if (_erabiltzailea is Pazientea pazientea)
            {
                errezetakGuztiak = errezetaKontrolatzailea.LortuPazientearenErrezetak(pazientea.Id, false);
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

        private void ChkErrezetaAktiboak_CheckedChanged(object? sender, EventArgs e)
        {
            IragaziDatuak();
        }

        private void BtnGarbituFiltroak_Click(object? sender, EventArgs e)
        {
            txtBilatuPaz.Clear();
            filtroData = null;
            chkErrezetaAktiboak.Checked = false;
            // Egin behar dugu reset MonthCalendar-i
            mcDataFiltroa.SelectionStart = DateTime.Today;
            mcDataFiltroa.SelectionEnd = DateTime.Today;
            IragaziDatuak();
        }

        private void IragaziDatuak()
        {
            string query = txtBilatuPaz.Text.Trim();
            var iragazita = errezetakGuztiak.Where(e =>
                (string.IsNullOrEmpty(query) ||
                BalioaDauka(e.PazienteNan, query) ||
                BalioaDauka(e.PazienteIzenOsoa, query))
                &&
                (!chkErrezetaAktiboak.Checked || e.Aktibo)
                &&
                (!filtroData.HasValue ||
                e.IgorpenData.Date == filtroData.Value ||
                (e.HitzorduData.HasValue && e.HitzorduData.Value.Date == filtroData.Value))
            ).ToList();

            dgvErrezetak.CurrentCell = null;
            bsErrezetak.DataSource = null;

            if (iragazita.Count > 0)
            {
                var dtoList = iragazita.Select(e => new ErrezetaGridItem
                {
                    ErrezetaId = e.ErrezetaId,
                    Pazientea = e.PazienteIzenOsoa,
                    NAN = e.PazienteNan,
                    Aktibo = e.Aktibo ? "Bai" : "Ez",
                    Diagnostikoa = e.Diagnostikoa,
                    IgorpenData = e.IgorpenData.ToShortDateString(),
                    Hitzordua = e.HitzorduData.HasValue ? e.HitzorduData.Value.ToShortDateString() : "-"
                }).ToList();

                bsErrezetak.DataSource = dtoList;

                if (dgvErrezetak.Columns.Count > 0)
                {
                    if (dgvErrezetak.Columns["ErrezetaId"] is DataGridViewColumn errezetaIdZutabea)
                    {
                        errezetaIdZutabea.Visible = false;
                    }

                    if (dgvErrezetak.Columns["Aktibo"] is DataGridViewColumn aktiboZutabea)
                    {
                        aktiboZutabea.HeaderText = "AKTIBO";
                    }
                }
            }

            EguneratuBotikaGrid();
        }

        private static bool BalioaDauka(string? testua, string bilaketa)
        {
            return !string.IsNullOrWhiteSpace(testua)
                && testua.Contains(bilaketa, StringComparison.OrdinalIgnoreCase);
        }

        private void ChkPazienteGuztiak_CheckedChanged(object? sender, EventArgs e)
        {
            KargatuDatuak();
        }

        private void BtnEditatu_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ErrezetaKudeaketaBaimenduta)
                {
                    return;
                }

                if (dgvErrezetak.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Aukeratu errezeta bat mesedez.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cellValue = dgvErrezetak.SelectedRows[0].Cells["ErrezetaId"]?.Value;
                if (cellValue == null) return;

                int id = Convert.ToInt32(cellValue);
                var aurkitua = errezetakGuztiak.FirstOrDefault(x => x.ErrezetaId == id);

                if (aurkitua != null && _erabiltzailea != null)
                {
                    IrekiAzpiPantaila(() => new ErrezetaSortu(_erabiltzailea, aurkitua), KargatuDatuak);
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
                if (!ErrezetaKudeaketaBaimenduta)
                {
                    return;
                }

                if (dgvErrezetak.SelectedRows.Count == 0) return;

                var cellValue = dgvErrezetak.SelectedRows[0].Cells["ErrezetaId"]?.Value;
                if (cellValue == null) return;

                int id = Convert.ToInt32(cellValue);

                var result = MessageBox.Show(
                    "Ziur zaude errezeta hau eta lotutako botikak ezabatu nahi dituzula?",
                    "Ezabatu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (errezetaKontrolatzailea.EzabatuErrezeta(id))
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

        private void txtBilatuPaz_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dgvBotikak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
