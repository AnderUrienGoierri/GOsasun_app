using System.ComponentModel;
using System.Drawing.Drawing2D;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Login eginda dagoen erabiltzailearen fitxa profesionala erakusten du.
    /// </summary>
    public class NireErabiltzaileFitxa : OinarriPantaila
    {
        private readonly Erabiltzailea _erabiltzaileaOsoa;

        private Panel _laburpenPanela = null!;
        private Panel _irudiPanela = null!;
        private Panel _datuKomunenPanela = null!;
        private Panel _rolPanela = null!;
        private Panel _kontuPanela = null!;
        private PictureBox _irudiKutxa = null!;
        private Label _fitxaMotaLabel = null!;
        private Label _izenLabel = null!;
        private Label _azpiInformazioLabel = null!;
        private Label _rolBadgeLabel = null!;
        private Label _egoeraBadgeLabel = null!;
        private Label _irudiAzalpenaLabel = null!;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public NireErabiltzaileFitxa() : this(SortuDiseinukoErabiltzailea())
        {
        }

        public NireErabiltzaileFitxa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            _erabiltzaileaOsoa = erabiltzailea;
            SortuInterfazea();
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
            ClientSize = new Size(1600, 980);
            _goiburuBarra.Width = ClientSize.Width;
            _edukiPanela.Size = new Size(ClientSize.Width, ClientSize.Height - _goiburuBarra.Height);
            _fitxaMotaLabel.Text = "NIRE ERABILTZAILE FITXA";
            _atzeraBotoia.BringToFront();
        }

        private void SortuInterfazea()
        {
            _edukiPanela.SuspendLayout();
            _edukiPanela.Controls.Clear();
            _edukiPanela.AutoScroll = true;

            _laburpenPanela = SortuTxartelPanela(new Point(24, 34), new Size(1536, 210), Color.FromArgb(246, 250, 252));
            _irudiPanela = SortuTxartelPanela(new Point(24, 268), new Size(380, 620), Color.White);
            _datuKomunenPanela = SortuTxartelPanela(new Point(424, 268), new Size(1136, 344), Color.White);
            _rolPanela = SortuTxartelPanela(new Point(424, 632), new Size(760, 256), Color.White);
            _kontuPanela = SortuTxartelPanela(new Point(1204, 632), new Size(356, 256), Color.White);

            EraikiLaburpena();
            EraikiIrudiaPanela();
            EraikiDatuKomunakPanela();
            EraikiRolPanela();
            EraikiKontuPanela();

            _edukiPanela.Controls.Add(_laburpenPanela);
            _edukiPanela.Controls.Add(_irudiPanela);
            _edukiPanela.Controls.Add(_datuKomunenPanela);
            _edukiPanela.Controls.Add(_rolPanela);
            _edukiPanela.Controls.Add(_kontuPanela);
            _edukiPanela.ResumeLayout(false);
        }

        private void EraikiLaburpena()
        {
            _fitxaMotaLabel = new Label
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(78, 105, 130),
                Location = new Point(28, 18),
                Size = new Size(420, 36)
            };

            _izenLabel = new Label
            {
                Font = new Font("Segoe UI", 25F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 49, 69),
                Location = new Point(24, 56),
                Size = new Size(860, 76)
            };

            _azpiInformazioLabel = new Label
            {
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(86, 103, 121),
                Location = new Point(28, 142),
                Size = new Size(960, 42)
            };

            _rolBadgeLabel = SortuBadgeLabel(new Point(1320, 20), new Size(188, 40));
            _egoeraBadgeLabel = SortuBadgeLabel(new Point(1320, 76), new Size(188, 40));

            Label azalpenLabel = new Label
            {
                Text = "Erabiltzailearen datu administratiboak eta rolaren profil osoa toki bakarrean.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(102, 118, 136),
                Location = new Point(1028, 144),
                Size = new Size(458, 42),
                TextAlign = ContentAlignment.TopRight
            };

            _laburpenPanela.Controls.Add(_fitxaMotaLabel);
            _laburpenPanela.Controls.Add(_izenLabel);
            _laburpenPanela.Controls.Add(_azpiInformazioLabel);
            _laburpenPanela.Controls.Add(_rolBadgeLabel);
            _laburpenPanela.Controls.Add(_egoeraBadgeLabel);
            _laburpenPanela.Controls.Add(azalpenLabel);
        }

        private void EraikiIrudiaPanela()
        {
            Panel goikoMarra = new Panel
            {
                BackColor = Color.FromArgb(52, 152, 219),
                Dock = DockStyle.Top,
                Height = 8
            };

            Label titulua = new Label
            {
                Text = "PROFIL IRUDIA",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(77, 102, 126),
                Location = new Point(28, 28),
                Size = new Size(220, 28)
            };

            _irudiKutxa = new PictureBox
            {
                BackColor = Color.FromArgb(241, 246, 250),
                Location = new Point(40, 78),
                Size = new Size(300, 350),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            _irudiAzalpenaLabel = new Label
            {
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(97, 113, 130),
                Location = new Point(40, 454),
                Size = new Size(300, 110),
                Text = "Erabiltzailearen identifikazio bisuala. Irudirik ez badago, avatar medikal lehenetsia erabiliko da."
            };

            _irudiPanela.Controls.Add(goikoMarra);
            _irudiPanela.Controls.Add(titulua);
            _irudiPanela.Controls.Add(_irudiKutxa);
            _irudiPanela.Controls.Add(_irudiAzalpenaLabel);
        }

        private void EraikiDatuKomunakPanela()
        {
            Label titulua = SortuSekzioTitulua("DATU OROKORRAK", new Point(28, 22), 280);
            _datuKomunenPanela.Controls.Add(titulua);

            int ezkerX = 28;
            int eskuinX = 572;
            int zabalera = 520;

            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("NAN / DNI", out _nanBalioa, ezkerX, 72, zabalera, 76));
            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("EMAILA", out _emailaBalioa, eskuinX, 72, zabalera, 76));
            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("JAIOTZE DATA", out _jaiotzeDataBalioa, ezkerX, 162, zabalera, 76));
            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("ADINA", out _adinaBalioa, eskuinX, 162, zabalera, 76));
            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("TELEFONOA", out _telefonoaBalioa, ezkerX, 252, zabalera, 76));
            _datuKomunenPanela.Controls.Add(SortuDatuBlokea("HELBIDEA ETA UDALERRIA", out _helbideaBalioa, eskuinX, 252, zabalera, 76));
        }

        private void EraikiRolPanela()
        {
            _rolPanela.Controls.Add(SortuSekzioTitulua("ROLAREN DATUAK", new Point(28, 22), 280));
        }

        private void EraikiKontuPanela()
        {
            _kontuPanela.Controls.Add(SortuSekzioTitulua("KONTUAREN EGOERA", new Point(24, 22), 260));
            _kontuPanela.Controls.Add(SortuDatuBlokea("ERABILTZAILE ID", out _idBalioa, 24, 70, 308, 60));
            _kontuPanela.Controls.Add(SortuDatuBlokea("ROL ID", out _rolIdBalioa, 24, 136, 308, 60));
            _kontuPanela.Controls.Add(SortuDatuBlokea("HIZKUNTZA", out _hizkuntzaBalioa, 24, 202, 308, 60));
        }

        private Label _nanBalioa = null!;
        private Label _emailaBalioa = null!;
        private Label _jaiotzeDataBalioa = null!;
        private Label _adinaBalioa = null!;
        private Label _telefonoaBalioa = null!;
        private Label _helbideaBalioa = null!;
        private Label _idBalioa = null!;
        private Label _rolIdBalioa = null!;
        private Label _hizkuntzaBalioa = null!;

        private void BeteDatuak()
        {
            _izenLabel.Text = _erabiltzaileaOsoa.IzenOsoa;
            _azpiInformazioLabel.Text = $"{_erabiltzaileaOsoa.Rola}   |   {_erabiltzaileaOsoa.Emaila}   |   Alta data: {FormateatuData(_erabiltzaileaOsoa.SortzeData)}";

            EzarriBadge(_rolBadgeLabel, _erabiltzaileaOsoa.Rola.ToUpperInvariant(), Color.FromArgb(226, 239, 252), Color.FromArgb(44, 93, 140));
            EzarriBadge(
                _egoeraBadgeLabel,
                _erabiltzaileaOsoa.Aktibo ? "AKTIBOA" : "EZ AKTIBOA",
                _erabiltzaileaOsoa.Aktibo ? Color.FromArgb(223, 245, 232) : Color.FromArgb(252, 231, 230),
                _erabiltzaileaOsoa.Aktibo ? Color.FromArgb(32, 102, 70) : Color.FromArgb(151, 44, 39));

            _nanBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Nan);
            _emailaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Emaila);
            _jaiotzeDataBalioa.Text = FormateatuData(_erabiltzaileaOsoa.JaiotzeData);
            _adinaBalioa.Text = KalkulatuAdina(_erabiltzaileaOsoa.JaiotzeData);
            _telefonoaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Telefonoa);
            _helbideaBalioa.Text = FormateatuHelbidea();
            _idBalioa.Text = _erabiltzaileaOsoa.Id.ToString();
            _rolIdBalioa.Text = _erabiltzaileaOsoa.RolId.ToString();
            _hizkuntzaBalioa.Text = FormateatuTestua(_erabiltzaileaOsoa.Hizkuntza);

            BeteRolarenDatuak();
            KargatuIrudia();
        }

        private void BeteRolarenDatuak()
        {
            foreach (Control kontrola in _rolPanela.Controls.Cast<Control>().ToList())
            {
                if (kontrola is Label label && label.Text == "ROLAREN DATUAK")
                {
                    continue;
                }

                _rolPanela.Controls.Remove(kontrola);
                kontrola.Dispose();
            }

            if (_erabiltzaileaOsoa is Pazientea pazientea)
            {
                _rolPanela.Controls.Add(SortuDatuBlokea("SEXUA", out Label sexuaBalioa, 28, 72, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("ODOL TALDEA", out Label odolBalioa, 392, 72, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("AZKEN ALTUERA", out Label altueraBalioa, 28, 160, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("AZKEN PISUA", out Label pisuaBalioa, 392, 160, 334, 72));

                sexuaBalioa.Text = FormateatuTestua(pazientea.Sexua);
                odolBalioa.Text = FormateatuTestua(pazientea.OdolTaldea);
                altueraBalioa.Text = pazientea.AzkenAltuera.HasValue ? $"{pazientea.AzkenAltuera.Value:F2} cm" : "---";
                pisuaBalioa.Text = pazientea.AzkenPisua.HasValue ? $"{pazientea.AzkenPisua.Value:F2} kg" : "---";

                Label egoeraLabel = SortuOharMedikala(
                    "Egoera klinikoa",
                    FormateatuTestua(pazientea.EgoeraKlinikoa),
                    new Point(28, 224),
                    new Size(698, 22));
                _rolPanela.Controls.Add(egoeraLabel);
                return;
            }

            if (_erabiltzaileaOsoa is OsasunLangilea osasunLangilea)
            {
                _rolPanela.Controls.Add(SortuDatuBlokea("ELKARGOKIDE ZBK.", out Label elkargokideBalioa, 28, 72, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("ESPEZIALITATEA", out Label espezialitateBalioa, 392, 72, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("KONTSULTA", out Label kontsultaBalioa, 28, 160, 334, 72));
                _rolPanela.Controls.Add(SortuDatuBlokea("LANALDIA", out Label lanaldiBalioa, 392, 160, 334, 72));

                elkargokideBalioa.Text = FormateatuTestua(osasunLangilea.ElkargokideZenbakia);
                espezialitateBalioa.Text = FormateatuTestua(osasunLangilea.Espezialitatea);
                kontsultaBalioa.Text = FormateatuTestua(osasunLangilea.Kontsulta);
                lanaldiBalioa.Text = FormateatuTestua(osasunLangilea.Lanaldia);
                return;
            }

            if (_erabiltzaileaOsoa is HarrerakoLangilea harrerakoa)
            {
                _rolPanela.Controls.Add(SortuDatuBlokea("TXANDA", out Label txandaBalioa, 28, 72, 698, 72));
                txandaBalioa.Text = FormateatuTestua(harrerakoa.Txanda);

                Label azalpena = SortuOharMedikala(
                    "Arreta administratiboa",
                    "Harrerako profila hitzorduen, dokumentuen eta erabiltzaileen kudeaketa koordinatzeko prestatuta dago.",
                    new Point(28, 170),
                    new Size(698, 52));
                _rolPanela.Controls.Add(azalpena);
            }
        }

        private void KargatuIrudia()
        {
            string? bidea = BilatuFitxategiErlatiboa(_erabiltzaileaOsoa.Irudia);
            if (!string.IsNullOrWhiteSpace(bidea) && File.Exists(bidea))
            {
                using Image jatorrizkoa = Image.FromFile(bidea);
                _irudiKutxa.Image = new Bitmap(jatorrizkoa);
                return;
            }

            _irudiKutxa.Image = SortuPlaceholderIrudia(_erabiltzaileaOsoa.Rola);
        }

        private static Bitmap SortuPlaceholderIrudia(string rola)
        {
            Bitmap bitmap = new Bitmap(300, 350);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.FromArgb(241, 246, 250));

            using SolidBrush haloBrush = new SolidBrush(Color.FromArgb(220, 232, 243));
            using SolidBrush avatarBrush = new SolidBrush(Color.FromArgb(103, 132, 159));
            using SolidBrush accentBrush = new SolidBrush(Color.FromArgb(52, 152, 219));
            using Pen crossPen = new Pen(Color.FromArgb(255, 255, 255), 10f);

            graphics.FillEllipse(haloBrush, 72, 24, 156, 156);
            graphics.FillEllipse(avatarBrush, 114, 58, 72, 72);
            graphics.FillPie(avatarBrush, 92, 112, 116, 90, 200, 140);
            graphics.FillEllipse(accentBrush, 188, 222, 72, 72);
            graphics.DrawLine(crossPen, 224, 238, 224, 278);
            graphics.DrawLine(crossPen, 204, 258, 244, 258);

            using Font badgeFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            using SolidBrush textBrush = new SolidBrush(Color.FromArgb(77, 102, 126));
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(rola, badgeFont, textBrush, new RectangleF(26, 292, 248, 36), format);
            return bitmap;
        }

        private string FormateatuHelbidea()
        {
            List<string> zatiak = new List<string>();
            if (!string.IsNullOrWhiteSpace(_erabiltzaileaOsoa.Helbidea))
            {
                zatiak.Add(_erabiltzaileaOsoa.Helbidea!.Trim());
            }

            string herria = string.IsNullOrWhiteSpace(_erabiltzaileaOsoa.Herria) ? string.Empty : _erabiltzaileaOsoa.Herria!.Trim();
            string postaKodea = string.IsNullOrWhiteSpace(_erabiltzaileaOsoa.PostaKodea) ? string.Empty : _erabiltzaileaOsoa.PostaKodea!.Trim();
            string kokalekua = string.Join(" - ", new[] { postaKodea, herria }.Where(zatia => !string.IsNullOrWhiteSpace(zatia)));
            if (!string.IsNullOrWhiteSpace(kokalekua))
            {
                zatiak.Add(kokalekua);
            }

            return zatiak.Count == 0 ? "---" : string.Join(Environment.NewLine, zatiak);
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

        private static Panel SortuTxartelPanela(Point kokapena, Size tamaina, Color atzekoKolorea)
        {
            return new Panel
            {
                BackColor = atzekoKolorea,
                BorderStyle = BorderStyle.FixedSingle,
                Location = kokapena,
                Size = tamaina
            };
        }

        private static Label SortuSekzioTitulua(string testua, Point kokapena, int zabalera)
        {
            return new Label
            {
                Text = testua,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(77, 102, 126),
                Location = kokapena,
                Size = new Size(zabalera, 28)
            };
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
                Font = new Font("Segoe UI", 9.2F, FontStyle.Bold),
                ForeColor = Color.FromArgb(112, 127, 143),
                Location = new Point(16, 10),
                Size = new Size(zabalera - 32, 22)
            };

            balioLabel = new Label
            {
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 49, 69),
                Location = new Point(16, 34),
                Size = new Size(zabalera - 32, altuera - 40),
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

        private static Label SortuBadgeLabel(Point kokapena, Size tamaina)
        {
            return new Label
            {
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = kokapena,
                Size = tamaina,
                TextAlign = ContentAlignment.MiddleCenter
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