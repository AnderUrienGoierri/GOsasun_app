using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Interfazea
{
    public partial class ErrezetaSortu : OinarriPantaila
    {
        private ErabiltzaileDB erabiltzaileDB = new ErabiltzaileDB();
        private BotikaDB botikaDB = new BotikaDB();
        private ErrezetaDB errezetaDB = new ErrezetaDB();
        
        private Errezeta? _editatzekoErrezeta;

        private List<Pazientea> pazienteak = new List<Pazientea>();
        private List<Botika> botikaGuztiak = new List<Botika>();
        private List<ErrezetaBotikaItem> saskia = new List<ErrezetaBotikaItem>();

        private BindingSource bsPazienteak = new BindingSource();
        private BindingSource bsSaskia = new BindingSource();
        
        private class PazienteGridItem
        {
            public string? Nan { get; set; }
            public string? IzenOsoa { get; set; }
        }

        public class ErrezetaBotikaItem
        {
            public int BotikaId { get; set; }
            public string BotikaIzena { get; set; } = string.Empty;
            public string Dosia { get; set; } = string.Empty;
            public string Maiztasuna { get; set; } = string.Empty;
        }

        public ErrezetaSortu() : base()
        {
            InitializeComponent();
            dgvPazienteak.DataSource = bsPazienteak;
            dgvBotikak.DataSource = bsSaskia;
            LoadData();
        }

        public ErrezetaSortu(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            dgvPazienteak.DataSource = bsPazienteak;
            dgvBotikak.DataSource = bsSaskia;
            LoadData();
        }

        public ErrezetaSortu(Erabiltzailea u, Errezeta editatzekoErrezeta) : base(u)
        {
            _editatzekoErrezeta = editatzekoErrezeta;
            InitializeComponent();
            dgvPazienteak.DataSource = bsPazienteak;
            dgvBotikak.DataSource = bsSaskia;
            LoadData();
        }

        private void LoadData()
        {
            txtBilatuPaz.TextChanged += TxtBilatuPaz_TextChanged;
            btnGehituBotika.Click += BtnGehituBotika_Click;
            btnKenduBotika.Click += BtnKenduBotika_Click;
            btnSortuErrezeta.Click += BtnSortuErrezeta_Click;

            botikaGuztiak = botikaDB.LortuBotikaGuztiak();
            cmbBotikak.DataSource = botikaGuztiak;
            cmbBotikak.DisplayMember = "Izena";
            cmbBotikak.ValueMember = "BotikaId";

            dtpIraungitzeData.Value = DateTime.Now.AddMonths(1);

            EguneratuPazienteak("");
            EguneratuSaskia();

            if (_editatzekoErrezeta != null)
            {
                lblIzenburua.Text = "ERREZETA EDITATU";
                btnSortuErrezeta.Text = "ERREZETA EGUNERATU";

                txtBilatuPaz.Text = _editatzekoErrezeta.PazienteNan;
                txtDiagnostikoa.Text = _editatzekoErrezeta.Diagnostikoa;
                if (_editatzekoErrezeta.IraungitzeData.HasValue) dtpIraungitzeData.Value = _editatzekoErrezeta.IraungitzeData.Value;

                foreach(var eb in _editatzekoErrezeta.Botikak)
                {
                    saskia.Add(new ErrezetaBotikaItem
                    {
                        BotikaId = eb.BotikaId,
                        BotikaIzena = eb.BotikaIzena ?? "Botika Ezezaguna",
                        Dosia = eb.Dosia ?? "",
                        Maiztasuna = eb.Maiztasuna ?? ""
                    });
                }

                // Aukeratu pazientea grid-ean NAN-aren arabera txukuntzea (TextChangeds kargatzen ari dena)
                EguneratuSaskia();
            }
        }

        private void TxtBilatuPaz_TextChanged(object? sender, EventArgs e)
        {
            EguneratuPazienteak(txtBilatuPaz.Text);
        }

        private void EguneratuPazienteak(string bilatzailea)
        {
            if (_erabiltzailea != null && _erabiltzailea is OsasunLangilea)
            {
                pazienteak = erabiltzaileDB.LortuLangilearenPazienteak(_erabiltzailea.Id, bilatzailea);

                // Fallback: If not found in doctor's list (e.g. editing an old prescription), search all patients
                if (pazienteak.Count == 0 && !string.IsNullOrEmpty(bilatzailea))
                {
                    var guztiak = erabiltzaileDB.LortuGuztiakPazienteak();
                    var aurkitua = guztiak.FirstOrDefault(p => p.Nan != null && p.Nan.Equals(bilatzailea, StringComparison.OrdinalIgnoreCase));
                    if (aurkitua != null) pazienteak.Add(aurkitua);
                }

            dgvPazienteak.CurrentCell = null;
            bsPazienteak.DataSource = null;

            if (pazienteak.Count > 0)
            {
                var dtoList = pazienteak.Select(p => new PazienteGridItem
                {
                    Nan = p.Nan,
                    IzenOsoa = $"{p.Izena} {p.Abizenak}"
                }).ToList();

                bsPazienteak.DataSource = dtoList;

                if (dgvPazienteak.Columns.Count > 0)
                {
                    dgvPazienteak.Columns["Nan"].HeaderText = "NAN";
                    dgvPazienteak.Columns["IzenOsoa"].HeaderText = "Pazientea";
                }

                // Auto-select if there's only one match (common during editing or specific searches)
                if (pazienteak.Count == 1 && dgvPazienteak.Rows.Count > 0)
                {
                    dgvPazienteak.ClearSelection();
                    dgvPazienteak.Rows[0].Selected = true;
                }
            }
            }
        }

        private void BtnGehituBotika_Click(object? sender, EventArgs e)
        {
            if (cmbBotikak.SelectedItem is Botika bot)
            {
                saskia.Add(new ErrezetaBotikaItem
                {
                    BotikaId = bot.BotikaId,
                    BotikaIzena = bot.Izena,
                    Dosia = txtDosia.Text,
                    Maiztasuna = txtMaiztasuna.Text
                });

                txtDosia.Clear();
                txtMaiztasuna.Clear();

                EguneratuSaskia();
            }
        }

        private void BtnKenduBotika_Click(object? sender, EventArgs e)
        {
            if (dgvBotikak.SelectedRows.Count > 0)
            {
                int index = dgvBotikak.SelectedRows[0].Index;
                saskia.RemoveAt(index);
                EguneratuSaskia();
            }
        }

        private void EguneratuSaskia()
        {
            bsSaskia.DataSource = null;
            bsSaskia.DataSource = saskia;
            if (dgvBotikak.Columns.Count > 0)
            {
                dgvBotikak.Columns["BotikaId"].Visible = false;
                dgvBotikak.Columns["BotikaIzena"].HeaderText = "Botika";
                dgvBotikak.Columns["Dosia"].HeaderText = "Dosia";
                dgvBotikak.Columns["Maiztasuna"].HeaderText = "Maiztasuna";
            }
        }

        private void BtnSortuErrezeta_Click(object? sender, EventArgs e)
        {
            if (_erabiltzailea == null) return;

            if (_editatzekoErrezeta != null)
            {
                _editatzekoErrezeta.Diagnostikoa = txtDiagnostikoa.Text;
                _editatzekoErrezeta.IraungitzeData = dtpIraungitzeData.Value;

                _editatzekoErrezeta.Botikak.Clear();
                foreach (var s in saskia)
                {
                    _editatzekoErrezeta.Botikak.Add(new ErrezetaBotika
                    {
                        BotikaId = s.BotikaId,
                        Dosia = s.Dosia,
                        Maiztasuna = s.Maiztasuna
                    });
                }

                bool eguneratuEmaitza = errezetaDB.EguneratuErrezeta(_editatzekoErrezeta);
                if (eguneratuEmaitza)
                {
                    MessageBox.Show("Errezeta zuzen eguneratu da.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Bukatu dugu edizioarekin
                }
                else
                {
                    MessageBox.Show("Errore bat egon da errezeta eguneratzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            if (dgvPazienteak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez, aukeratu paziente bat lehenik.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dgvPazienteak.SelectedRows[0].Index;
            var paz = pazienteak[index];

            Errezeta berria = new Errezeta
            {
                OsasunLangileId = _erabiltzailea.Id,
                PazienteId = paz.Id,
                IgorpenData = DateTime.Now,
                IraungitzeData = dtpIraungitzeData.Value,
                Diagnostikoa = txtDiagnostikoa.Text
            };

            foreach (var s in saskia)
            {
                berria.Botikak.Add(new ErrezetaBotika
                {
                    BotikaId = s.BotikaId,
                    Dosia = s.Dosia,
                    Maiztasuna = s.Maiztasuna
                });
            }

            bool emaitza = errezetaDB.SortuErrezeta(berria);
            if (emaitza)
            {
                MessageBox.Show("Errezeta zuzen sortu da. Datu-basean botikak gorde dira.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiagnostikoa.Clear();
                dtpIraungitzeData.Value = DateTime.Now.AddMonths(1);
                saskia.Clear();
                EguneratuSaskia();
            }
            else
            {
                MessageBox.Show("Errore bat egon da errezeta sortzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlEskuina_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
