using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola;
using Svg;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Medikuari esleitutako pazienteen zerrenda taula bidez erakusten duen formularioa.
    /// </summary>
    public partial class PazienteenZerrenda : OinarriPantaila
    {
        private const int PazienteenZerrendaZabalera = 2360;
        private const int OinarrizkoZutabeZabaleraGuztira = 2100;
        private const int EkintzaZutabeZabalera = 220;
        private const int EkintzaIkonoTamaina = 22;
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;
        private List<Pazientea> _pazienteak = new List<Pazientea>();
        private readonly Dictionary<string, Bitmap?> _ekintzaIkonoak = new Dictionary<string, Bitmap?>();

        public PazienteenZerrenda(Erabiltzailea medikua)
            : base(medikua)
        {
            InitializeComponent();
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            EzarriFormularioZabalera();
            
            // Izenburua aldatu rolaran arabera
            if (_erabiltzailea is HarrerakoLangilea)
            {
                lblIzenburua.Text = "PAZIENTEEN KUDEAKETA";
                chkPazienteGuztiak.Checked = true;
                chkPazienteGuztiak.Enabled = false;
                btnPazienteBerria.Visible = false;
            }
            else
            {
                chkPazienteGuztiak.Checked = false;
                btnPazienteBerria.Visible = true;
            }

            KonfiguratuTaula();
            KargatuEkintzaIkonoak();
            KargatuPazienteak();

            // Gertaerak
            txtBilatu.TextChanged += TxtBilatu_TextChanged;
            chkPazienteGuztiak.CheckedChanged += PazienteMotaFiltroa_CheckedChanged;
            chkAltan.CheckedChanged += EgoeraFiltroa_CheckedChanged;
            chkBajan.CheckedChanged += EgoeraFiltroa_CheckedChanged;
            btnPazienteBerria.Click += BtnPazienteBerria_Click;
        }

        private void KonfiguratuTaula()
        {
            dgvPazienteak.AutoGenerateColumns = false;
            dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPazienteak.Columns.Clear();

            dgvPazienteak.Columns.Add(SortuTestuZutabea("Nan", "NAN", LortuDoitutakoZabalera(185)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Izena", "Izena", LortuDoitutakoZabalera(150)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Abizenak", "Abizenak", LortuDoitutakoZabalera(220)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Emaila", "Emaila", LortuDoitutakoZabalera(380)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Telefonoa", "Telefonoa", LortuDoitutakoZabalera(180)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("JaiotzeData", "Jaiotze data", LortuDoitutakoZabalera(195), "yyyy/MM/dd"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Sexua", "Sexua", LortuDoitutakoZabalera(110)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("OdolTaldea", "Odol taldea", LortuDoitutakoZabalera(120)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("AzkenAltuera", "Altuera", LortuDoitutakoZabalera(110), "N2"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("AzkenPisua", "Pisua", LortuDoitutakoZabalera(110), "N2"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("EgoeraKlinikoa", "Egoera", LortuDoitutakoZabalera(120)));

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
            else
            {
                dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "IzenOsoa",
                    HeaderText = "EKINTZAK",
                    Name = "Ekintzak",
                    Width = LortuDoitutakoZabalera(EkintzaZutabeZabalera),
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });
            }

            dgvPazienteak.Cursor = Cursors.Default;
            dgvPazienteak.CellMouseEnter += (s, e) => {
                DataGridViewColumn? editZutabea = dgvPazienteak.Columns["btnEditatu"];
                DataGridViewColumn? ezabatuZutabea = dgvPazienteak.Columns["btnEzabatu"];

                if (e.RowIndex >= 0 && ((editZutabea != null && e.ColumnIndex == editZutabea.Index)
                    || (ezabatuZutabea != null && e.ColumnIndex == ezabatuZutabea.Index)
                    || EkintzakZutabeaDa(e.ColumnIndex)))
                    dgvPazienteak.Cursor = Cursors.Hand;
                else
                    dgvPazienteak.Cursor = Cursors.Default;
            };

            dgvPazienteak.ColumnHeaderMouseClick += DgvPazienteak_ColumnHeaderMouseClick;
            dgvPazienteak.CellPainting += DgvPazienteak_CellPainting;
            dgvPazienteak.CellMouseClick += DgvPazienteak_CellMouseClick;
        }

        private void EzarriFormularioZabalera()
        {
            int pantailaZabalera = Screen.FromControl(this).WorkingArea.Width;
            int zabalera = Math.Min(PazienteenZerrendaZabalera, Math.Max(1660, pantailaZabalera - 60));

            ClientSize = new Size(zabalera, ClientSize.Height);
            _goiburuBarra.Width = zabalera;
            _edukiPanela.Size = new Size(zabalera, _edukiPanela.Height);
            EguneratuBilatzailearenDiseinua();
            ZentratuPantailaLanEremuan();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EzarriFormularioZabalera();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(() =>
            {
                EzarriFormularioZabalera();
                EguneratuTaularenZabalerak();
                KargatuPazienteak(txtBilatu.Text.Trim());
                ZentratuPantailaLanEremuan();
            }));
        }

        private int LortuDoitutakoZabalera(int oinarrizkoZabalera)
        {
            if (_erabiltzailea is HarrerakoLangilea)
            {
                return oinarrizkoZabalera;
            }

            int erabilgarri = Math.Max(1480, ClientSize.Width - 140);
            double eskala = Math.Min(1d, (double)erabilgarri / OinarrizkoZutabeZabaleraGuztira);
            return Math.Max(70, (int)Math.Round(oinarrizkoZabalera * eskala));
        }

        private void EguneratuTaularenZabalerak()
        {
            if (dgvPazienteak.Columns.Count == 0 || _erabiltzailea is HarrerakoLangilea)
            {
                return;
            }

            Dictionary<string, int> zabalerak = new Dictionary<string, int>
            {
                ["Nan"] = 185,
                ["Izena"] = 150,
                ["Abizenak"] = 220,
                ["Emaila"] = 380,
                ["Telefonoa"] = 180,
                ["JaiotzeData"] = 195,
                ["Sexua"] = 110,
                ["OdolTaldea"] = 120,
                ["AzkenAltuera"] = 110,
                ["AzkenPisua"] = 110,
                ["EgoeraKlinikoa"] = 120,
                ["Ekintzak"] = EkintzaZutabeZabalera
            };

            foreach (DataGridViewColumn zutabea in dgvPazienteak.Columns)
            {
                if (zabalerak.TryGetValue(zutabea.Name, out int oinarrizkoZabalera))
                {
                    zutabea.Width = LortuDoitutakoZabalera(oinarrizkoZabalera);
                }
            }
        }

        private void EguneratuBilatzailearenDiseinua()
        {
            int eskuinMuga = pnlBilatzailea.Width - 22;

            if (btnPazienteBerria.Visible)
            {
                btnPazienteBerria.Location = new Point(eskuinMuga - btnPazienteBerria.Width, btnPazienteBerria.Location.Y);
                eskuinMuga = btnPazienteBerria.Left - 18;
            }

            chkBajan.Location = new Point(eskuinMuga - chkBajan.Width, chkBajan.Location.Y);
            eskuinMuga = chkBajan.Left - 18;
            chkAltan.Location = new Point(eskuinMuga - chkAltan.Width, chkAltan.Location.Y);
            eskuinMuga = chkAltan.Left - 18;
            chkPazienteGuztiak.Location = new Point(eskuinMuga - chkPazienteGuztiak.Width, chkPazienteGuztiak.Location.Y);

            txtBilatu.Width = Math.Max(560, chkPazienteGuztiak.Left - txtBilatu.Left - 26);
        }

        private static DataGridViewTextBoxColumn SortuTestuZutabea(string propertyName, string headerText, int width, string? format = null)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                Width = width,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = format ?? string.Empty,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
        }

        private void KargatuEkintzaIkonoak()
        {
            if (_erabiltzailea is HarrerakoLangilea) return;

            _ekintzaIkonoak["fitxa"] = KargatuSvgIkonoa("eye.svg");
            _ekintzaIkonoak["jarraipena"] = KargatuSvgIkonoa("stethoscope.svg");
        }

        private static Bitmap? KargatuSvgIkonoa(string fileName)
        {
            string? bidea = BilatuSvgBidea(fileName);
            if (string.IsNullOrWhiteSpace(bidea) || !File.Exists(bidea)) return null;

            try
            {
                string svg = File.ReadAllText(bidea).Replace("currentColor", "#FFFFFF", StringComparison.OrdinalIgnoreCase);
                using MemoryStream memoria = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svg));
                SvgDocument dokumentua = SvgDocument.Open<SvgDocument>(memoria);
                return dokumentua.Draw(EkintzaIkonoTamaina, EkintzaIkonoTamaina);
            }
            catch
            {
                return null;
            }
        }

        private static string? BilatuSvgBidea(string fileName)
        {
            string[] erroak =
            {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(PazienteenZerrenda).Assembly.Location) ?? string.Empty
            };

            foreach (string hasiera in erroak.Where(Directory.Exists))
            {
                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    string[] aukerak =
                    {
                        Path.Combine(karpeta.FullName, "img", "svg", fileName),
                        Path.Combine(karpeta.FullName, "GOsasun_app", "img", "svg", fileName)
                    };

                    string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                    if (!string.IsNullOrWhiteSpace(aurkitua)) return aurkitua;
                    karpeta = karpeta.Parent;
                }
            }

            return null;
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            return columnIndex >= 0
                && columnIndex < dgvPazienteak.Columns.Count
                && string.Equals(dgvPazienteak.Columns[columnIndex].Name, "Ekintzak", StringComparison.OrdinalIgnoreCase);
        }

        private static Dictionary<string, Rectangle> LortuEkintzaBotoiak(Rectangle cellBounds)
        {
            int buttonSize = 42;
            int spacing = 14;
            int totalWidth = (buttonSize * 2) + spacing;
            int left = cellBounds.Left + Math.Max(10, (cellBounds.Width - totalWidth) / 2);
            int top = cellBounds.Top + Math.Max(6, (cellBounds.Height - buttonSize) / 2);

            return new Dictionary<string, Rectangle>
            {
                ["fitxa"] = new Rectangle(left, top, buttonSize, buttonSize),
                ["jarraipena"] = new Rectangle(left + buttonSize + spacing, top, buttonSize, buttonSize)
            };
        }

        private void DgvPazienteak_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0 || e.Graphics == null) return;

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                MarraztuEkintzaBotoia(e.Graphics, botoia.Key, botoia.Value);
            }

            e.Handled = true;
        }

        private void MarraztuEkintzaBotoia(Graphics graphics, string ekintza, Rectangle rectangle)
        {
            Color kolorea = ekintza == "fitxa"
                ? Color.FromArgb(41, 128, 185)
                : Color.FromArgb(39, 174, 96);
            string fallbackIkurra = ekintza == "fitxa" ? "F" : "J";

            using GraphicsPath path = SortuBiribildua(rectangle, 12);
            using SolidBrush brush = new SolidBrush(kolorea);
            using Pen pen = new Pen(Color.White, 1);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.DrawPath(pen, path);

            if (_ekintzaIkonoak.TryGetValue(ekintza, out Bitmap? ikonoa) && ikonoa != null)
            {
                int left = rectangle.Left + ((rectangle.Width - ikonoa.Width) / 2);
                int top = rectangle.Top + ((rectangle.Height - ikonoa.Height) / 2);
                graphics.DrawImage(ikonoa, new Rectangle(left, top, ikonoa.Width, ikonoa.Height));
            }
            else
            {
                TextRenderer.DrawText(graphics, fallbackIkurra, new Font("Segoe UI", 10F, FontStyle.Bold), rectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath SortuBiribildua(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DgvPazienteak_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0) return;

            Pazientea? pazientea = dgvPazienteak.Rows[e.RowIndex].DataBoundItem as Pazientea;
            if (pazientea == null) return;

            Rectangle cellBounds = dgvPazienteak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(cellBounds))
            {
                if (!botoia.Value.Contains(posizioa)) continue;

                if (botoia.Key == "fitxa")
                {
                    IrekiFormularioa(new PazienteXehetasunak(pazientea));
                }
                else if (_erabiltzailea != null)
                {
                    IrekiFormularioa(new Jarraipenak(_erabiltzailea, pazientea.Id, pazientea.IzenOsoa));
                }

                break;
            }
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
                string? egoeraFiltroa = LortuEgoeraFiltroa();
                if (_erabiltzailea is HarrerakoLangilea)
                {
                    _pazienteak = _kontrolatzailea.LortuGuztiakPazienteak(testua, egoeraFiltroa);
                }
                else if (chkPazienteGuztiak.Checked)
                {
                    _pazienteak = _kontrolatzailea.LortuGuztiakPazienteak(testua, egoeraFiltroa);
                }
                else
                {
                    _pazienteak = _kontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea!.Id, testua, egoeraFiltroa);
                }
                
                dgvPazienteak.DataSource = null;
                dgvPazienteak.DataSource = _pazienteak;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPazienteBerria_Click(object? sender, EventArgs e)
        {
            if (_erabiltzailea == null) return;

            ErabiltzaileaSortu formularioa = new ErabiltzaileaSortu("Pazientea", _erabiltzailea, _erabiltzailea.Id);
            formularioa.FormClosed += (s, args) =>
            {
                Show();
                KargatuPazienteak(txtBilatu.Text.Trim());
            };

            Hide();
            formularioa.Show();
        }

        private void TxtBilatu_TextChanged(object? sender, EventArgs e)
        {
            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private void PazienteMotaFiltroa_CheckedChanged(object? sender, EventArgs e)
        {
            if (_erabiltzailea is HarrerakoLangilea) return;
            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private void EgoeraFiltroa_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not CheckBox aukeratutakoCheck) return;

            if (!aukeratutakoCheck.Checked)
            {
                return;
            }

            foreach (CheckBox check in new[] { chkAltan, chkBajan })
            {
                if (!ReferenceEquals(check, aukeratutakoCheck))
                {
                    check.Checked = false;
                }
            }

            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private string? LortuEgoeraFiltroa()
        {
            if (chkAltan.Checked) return "Alta";
            if (chkBajan.Checked) return "Baja";
            return null;
        }

        private void dgvPazienteak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var pazientea = dgvPazienteak.Rows[e.RowIndex].DataBoundItem as Pazientea;
            if (pazientea == null) return;

            if (EkintzakZutabeaDa(e.ColumnIndex))
            {
                return;
            }

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
