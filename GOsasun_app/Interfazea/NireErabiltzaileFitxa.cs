using System.ComponentModel;
using System.Drawing.Drawing2D;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Login eginda dagoen erabiltzailearen fitxa profesionala erakusten du.
    /// </summary>
    public partial class NireErabiltzaileFitxa : OinarriPantaila
    {
        private readonly Erabiltzailea _erabiltzaileaOsoa;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public NireErabiltzaileFitxa() : this(SortuDiseinukoErabiltzailea())
        {
        }

        public NireErabiltzaileFitxa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            _erabiltzaileaOsoa = erabiltzailea;
            InitializeComponent();
            KonfiguratuPantaila();
            BeteDatuak();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ZentratuPantailaLanEremuan();
        }

        private void KonfiguratuPantaila()
        {
            Text = "GOsasun - Nire Erabiltzaile Fitxa";
            ClientSize = new Size(1500, 980);
            _goiburuBarra.Width = ClientSize.Width;
            _edukiPanela.Size = new Size(ClientSize.Width, _edukiPanela.Height);
            lblFitxaMota.Text = "NIRE ERABILTZAILE FITXA";
            KonfiguratuTxartelenEdukia();
            _atzeraBotoia.BringToFront();
        }

        private void KonfiguratuTxartelenEdukia()
        {
            KonfiguratuArgazkiaAtala();
            KonfiguratuIdentifikazioAtala();
            KonfiguratuHarremanAtala();
            KonfiguratuRolAtala();
            KonfiguratuKontuAtala();
        }

        private void KonfiguratuArgazkiaAtala()
        {
            lblArgazkiAzalpena.Text = "Erabiltzailearen identifikazio bisuala. Irudirik ez badago, avatar medikal lehenetsia erabiliko da.";
            lblArgazkiAzalpena.AutoSize = false;
            lblArgazkiAzalpena.Size = new Size(242, 104);
        }

        private void KonfiguratuIdentifikazioAtala()
        {
            KonfiguratuSectionTitle(lblIdentifikazioa, "IDENTIFIKAZIOA", 28);

            KonfiguratuField(lblNanTitulua, "NAN / DNI", 28, 86);
            KonfiguratuValue(lblNanBalioa, "---", 28, 118, 230);

            KonfiguratuField(lblJaiotzeDataTitulua, "JAIOTZE DATA", 318, 86);
            KonfiguratuValue(lblJaiotzeDataBalioa, "---", 318, 118, 230);

            KonfiguratuField(lblAdinaTitulua, "ADINA", 28, 202);
            KonfiguratuValue(lblAdinaBalioa, "---", 28, 234, 230);

            KonfiguratuField(lblErabiltzaileMotaTitulua, "ERABILTZAILE MOTA", 318, 202);
            KonfiguratuValue(lblErabiltzaileMotaBalioa, "---", 318, 234, 230);
        }

        private void KonfiguratuHarremanAtala()
        {
            KonfiguratuSectionTitle(lblHarremana, "HARREMANETARAKO DATUAK", 28);

            KonfiguratuField(lblEmailaTitulua, "EMAILA", 28, 86);
            lblEmailaTitulua.Size = new Size(360, 28);
            KonfiguratuValue(lblEmailaBalioa, "---", 28, 118, 500);
            lblEmailaBalioa.Height = 52;

            KonfiguratuField(lblTelefonoaTitulua, "TELEFONOA", 548, 86);
            lblTelefonoaTitulua.Size = new Size(220, 28);
            KonfiguratuValue(lblTelefonoaBalioa, "---", 548, 118, 240);
            lblTelefonoaBalioa.Height = 52;

            KonfiguratuField(lblHelbideaTitulua, "HELBIDEA", 28, 206);
            lblHelbideaTitulua.Size = new Size(360, 28);
            KonfiguratuValue(lblHelbideaBalioa, "---", 28, 238, 500);
            lblHelbideaBalioa.Height = 70;

            KonfiguratuField(lblHerriaTitulua, "P.K. / UDALERRIA", 548, 206);
            lblHerriaTitulua.Size = new Size(300, 28);
            KonfiguratuValue(lblHerriaBalioa, "---", 548, 238, 320);
            lblHerriaBalioa.Height = 108;
        }

        private void KonfiguratuRolAtala()
        {
            KonfiguratuSectionTitle(lblRolDatuak, "PROFILAREN DATUAK", 28);
        }

        private void KonfiguratuKontuAtala()
        {
            KonfiguratuSectionTitle(lblKontua, "KONTUAREN EGOERA", 24);

            KonfiguratuField(lblIdTitulua, "ERABILTZAILE ID", 24, 86);
            KonfiguratuValue(lblIdBalioa, "---", 24, 118, 308);
            lblIdBalioa.Height = 52;

            KonfiguratuField(lblRolIdTitulua, "ROL ID", 24, 206);
            KonfiguratuValue(lblRolIdBalioa, "---", 24, 238, 308);
            lblRolIdBalioa.Height = 52;

            KonfiguratuField(lblAltaDataTitulua, "ALTA DATA", 24, 326);
            KonfiguratuValue(lblAltaDataBalioa, "---", 24, 358, 360);
            lblAltaDataBalioa.Height = 52;
        }

        private void BeteDatuak()
        {
            lblIzena.Text = _erabiltzaileaOsoa.IzenOsoa;
            lblAzpiInformazioa.Text = $"{_erabiltzaileaOsoa.Rola}   |   {_erabiltzaileaOsoa.Emaila}   |   Alta data: {FormateatuData(_erabiltzaileaOsoa.SortzeData)}";

            EzarriBadge(lblRolBadge, _erabiltzaileaOsoa.Rola.ToUpperInvariant(), Color.FromArgb(226, 239, 252), Color.FromArgb(44, 93, 140));
            EzarriBadge(
                lblEgoeraBadge,
                _erabiltzaileaOsoa.Aktibo ? "AKTIBOA" : "EZ AKTIBOA",
                _erabiltzaileaOsoa.Aktibo ? Color.FromArgb(223, 245, 232) : Color.FromArgb(252, 231, 230),
                _erabiltzaileaOsoa.Aktibo ? Color.FromArgb(32, 102, 70) : Color.FromArgb(151, 44, 39));

            lblNanBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Nan);
            lblJaiotzeDataBalioa.Text = FormateatuData(_erabiltzaileaOsoa.JaiotzeData);
            lblAdinaBalioa.Text = KalkulatuAdina(_erabiltzaileaOsoa.JaiotzeData);
            lblErabiltzaileMotaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Rola);
            lblEmailaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Emaila);
            lblTelefonoaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Telefonoa);
            lblHelbideaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Helbidea);
            lblHerriaBalioa.Text = FormateatuKokalekua();
            lblIdBalioa.Text = _erabiltzaileaOsoa.Id.ToString();
            lblRolIdBalioa.Text = _erabiltzaileaOsoa.RolId.ToString();
            lblAltaDataBalioa.Text = FormateatuData(_erabiltzaileaOsoa.SortzeData);

            BeteRolarenDatuak();
            KargatuIrudia();
        }

        private void BeteRolarenDatuak()
        {
            foreach (Control kontrola in pnlRolPanela.Controls.Cast<Control>().ToList())
            {
                if (kontrola == lblRolDatuak)
                {
                    continue;
                }

                pnlRolPanela.Controls.Remove(kontrola);
                kontrola.Dispose();
            }

            if (_erabiltzaileaOsoa is Pazientea pazientea)
            {
                pnlRolPanela.Controls.Add(SortuDatuBlokea("SEXUA", out Label sexuaBalioa, 28, 86, 410, 82));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("ODOL TALDEA", out Label odolBalioa, 472, 86, 410, 82));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("AZKEN ALTUERA", out Label altueraBalioa, 28, 198, 410, 82));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("AZKEN PISUA", out Label pisuaBalioa, 472, 198, 410, 82));

                sexuaBalioa.Text = FormateatuTestua(pazientea.Sexua);
                odolBalioa.Text = FormateatuTestua(pazientea.OdolTaldea);
                altueraBalioa.Text = pazientea.AzkenAltuera.HasValue ? $"{pazientea.AzkenAltuera.Value:F2} cm" : "---";
                pisuaBalioa.Text = pazientea.AzkenPisua.HasValue ? $"{pazientea.AzkenPisua.Value:F2} kg" : "---";

                Label egoeraLabel = SortuOharMedikala(
                    "Egoera klinikoa",
                    FormateatuTestua(pazientea.EgoeraKlinikoa),
                    new Point(28, 334),
                    new Size(854, 56));
                pnlRolPanela.Controls.Add(egoeraLabel);
                return;
            }

            if (_erabiltzaileaOsoa is OsasunLangilea osasunLangilea)
            {
                pnlRolPanela.Controls.Add(SortuDatuBlokea("ELKARGOKIDE ZBK.", out Label elkargokideBalioa, 28, 86, 410, 96));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("ESPEZIALITATEA", out Label espezialitateBalioa, 472, 86, 410, 96));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("KONTSULTA", out Label kontsultaBalioa, 28, 206, 410, 96));
                pnlRolPanela.Controls.Add(SortuDatuBlokea("LANALDIA", out Label lanaldiBalioa, 472, 206, 410, 96));

                elkargokideBalioa.Text = FormateatuTestua(osasunLangilea.ElkargokideZenbakia);
                espezialitateBalioa.Text = FormateatuTestua(osasunLangilea.Espezialitatea);
                kontsultaBalioa.Text = FormateatuTestua(osasunLangilea.Kontsulta);
                lanaldiBalioa.Text = FormateatuTestua(osasunLangilea.Lanaldia);
                return;
            }

            if (_erabiltzaileaOsoa is HarrerakoLangilea harrerakoa)
            {
                pnlRolPanela.Controls.Add(SortuDatuBlokea("TXANDA", out Label txandaBalioa, 28, 86, 854, 82));
                txandaBalioa.Text = FormateatuTestua(harrerakoa.Txanda);

                Label azalpena = SortuOharMedikala(
                    "Arreta administratiboa",
                    "Harrerako profila hitzorduen, dokumentuen eta erabiltzaileen kudeaketa koordinatzeko prestatuta dago.",
                    new Point(28, 222),
                    new Size(854, 80));
                pnlRolPanela.Controls.Add(azalpena);
            }
        }

        private void KargatuIrudia()
        {
            string? bidea = BilatuFitxategiErlatiboa(_erabiltzaileaOsoa.Irudia);
            if (!string.IsNullOrWhiteSpace(bidea) && File.Exists(bidea))
            {
                using Image jatorrizkoa = Image.FromFile(bidea);
                pbIrudia.Image = new Bitmap(jatorrizkoa);
                return;
            }

            pbIrudia.Image = SortuPlaceholderIrudia(_erabiltzaileaOsoa.Rola);
        }

        private static Bitmap SortuPlaceholderIrudia(string rola)
        {
            Bitmap bitmap = new Bitmap(260, 320);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.FromArgb(241, 246, 250));

            using SolidBrush haloBrush = new SolidBrush(Color.FromArgb(220, 232, 243));
            using SolidBrush avatarBrush = new SolidBrush(Color.FromArgb(103, 132, 159));
            using SolidBrush accentBrush = new SolidBrush(Color.FromArgb(52, 152, 219));
            using Pen crossPen = new Pen(Color.FromArgb(255, 255, 255), 10f);

            graphics.FillEllipse(haloBrush, 52, 24, 156, 156);
            graphics.FillEllipse(avatarBrush, 94, 58, 72, 72);
            graphics.FillPie(avatarBrush, 72, 112, 116, 90, 200, 140);
            graphics.FillEllipse(accentBrush, 170, 214, 72, 72);
            graphics.DrawLine(crossPen, 206, 230, 206, 270);
            graphics.DrawLine(crossPen, 186, 250, 226, 250);

            using Font badgeFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            using SolidBrush textBrush = new SolidBrush(Color.FromArgb(77, 102, 126));
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(rola, badgeFont, textBrush, new RectangleF(20, 282, 220, 36), format);
            return bitmap;
        }

        private string FormateatuKokalekua()
        {
            string herria = string.IsNullOrWhiteSpace(_erabiltzaileaOsoa.Herria) ? string.Empty : _erabiltzaileaOsoa.Herria.Trim();
            string postaKodea = string.IsNullOrWhiteSpace(_erabiltzaileaOsoa.PostaKodea) ? string.Empty : _erabiltzaileaOsoa.PostaKodea.Trim();

            if (string.IsNullOrEmpty(herria) && string.IsNullOrEmpty(postaKodea))
            {
                return "---";
            }

            if (string.IsNullOrEmpty(herria))
            {
                return postaKodea;
            }

            if (string.IsNullOrEmpty(postaKodea))
            {
                return herria;
            }

            return $"{postaKodea}{Environment.NewLine}{herria}";
        }

        private static string FormateatuTestua(string? testua)
        {
            return string.IsNullOrWhiteSpace(testua) ? "---" : testua.Trim();
        }

        private static string FormateatuData(DateTime data)
        {
            return data == DateTime.MinValue ? "---" : data.ToString("yyyy/MM/dd");
        }

        private static string KalkulatuAdina(DateTime jaiotzeData)
        {
            if (jaiotzeData == DateTime.MinValue)
            {
                return "---";
            }

            DateTime gaur = DateTime.Today;
            int adina = gaur.Year - jaiotzeData.Year;
            if (jaiotzeData.Date > gaur.AddYears(-adina))
            {
                adina--;
            }

            return $"{adina} urte";
        }

        private static Panel SortuDatuBlokea(string titulua, out Label balioLabel, int x, int y, int zabalera, int altuera)
        {
            Panel panela = new Panel
            {
                BackColor = Color.FromArgb(248, 251, 253),
                Location = new Point(x, y),
                Size = new Size(zabalera, altuera)
            };

            Label tituluaLabel = new Label
            {
                Text = titulua,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(112, 127, 143),
                Location = new Point(16, 12),
                Size = new Size(zabalera - 32, 20)
            };

            balioLabel = new Label
            {
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 49, 69),
                Location = new Point(16, 44),
                Size = new Size(zabalera - 32, altuera - 56),
                Text = "---"
            };

            panela.Controls.Add(tituluaLabel);
            panela.Controls.Add(balioLabel);
            return panela;
        }

        private static Label SortuOharMedikala(string titulua, string edukia, Point kokapena, Size tamaina)
        {
            return new Label
            {
                Text = $"{titulua}: {edukia}",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(97, 113, 130),
                Location = kokapena,
                Size = tamaina
            };
        }

        private static void EzarriBadge(Label label, string testua, Color atzekoKolorea, Color testuKolorea)
        {
            label.Text = testua;
            label.BackColor = atzekoKolorea;
            label.ForeColor = testuKolorea;
        }

        private static Erabiltzailea SortuDiseinukoErabiltzailea()
        {
            return new OsasunLangilea
            {
                Id = 17,
                Izena = "June",
                Abizenak = "Arrieta",
                Emaila = "june.arrieta@gosasun.eus",
                Nan = "12345678Z",
                JaiotzeData = new DateTime(1988, 11, 24),
                Telefonoa = "688123123",
                Helbidea = "Zumalakarregi kalea 14",
                Herria = "Bilbo",
                PostaKodea = "48009",
                Hizkuntza = "Euskara",
                RolId = 1,
                Aktibo = true,
                SortzeData = DateTime.Today.AddYears(-3),
                ElkargokideZenbakia = "COL-45872",
                Espezialitatea = "Medikuntza Orokorra",
                Kontsulta = "2B",
                Lanaldia = "Osoa"
            };
        }
    }
}