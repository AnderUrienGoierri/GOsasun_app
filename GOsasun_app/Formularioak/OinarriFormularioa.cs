// ============================================================
// OinarriFormularioa.cs - Formulario oinarria (Base Form)
// ============================================================
// Formulario guztiek heredatzen duten oinarri klasea.
// Egurrezko atzeko planoa, GoiburuBarra eta atzera-botoia
// kudeatzen ditu.
// ============================================================

using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Formulario guztien oinarri klasea.
    /// Egurrezko atzeko planoa eta nabigazio estandarra eskaintzen ditu.
    /// </summary>
    public partial class OinarriFormularioa : Form
    {
        // -----------------------------------------------------------
        // Osagaiak
        // -----------------------------------------------------------
        protected Button? _atzeraBotoia;

        // Erabiltzaile informazioa (OOP)
        protected Erabiltzailea? _erabiltzailea;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public OinarriFormularioa()
        {
            InitializeComponent();
            KonfiguratuFormularioa();
            KargatuBaliabideak();
        }

        /// <summary>
        /// Erabiltzaile informazioarekin eraikitzailea.
        /// </summary>
        public OinarriFormularioa(Erabiltzailea erabiltzailea)
            : this()
        {
            _erabiltzailea = erabiltzailea;
            
            // Goiburuko barra informazioarekin eguneratu
            if (_goiburuBarra != null)
            {
                _goiburuBarra.EguneratuInformazioa(_erabiltzailea.IzenOsoa, _erabiltzailea.Rola);
                _goiburuBarra.SaioaItxi += GoiburuBarra_SaioaItxi;
            }
        }

        // -----------------------------------------------------------
        // Formularioaren oinarrizko konfigurazioa
        // -----------------------------------------------------------
        private void KonfiguratuFormularioa()
        {
            // Tamaina eta estiloa (Tablet: 1024x600)
            this.ClientSize = new Size(1024, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Text = "GOsasun";

            // Eduki panela konfiguratu
            KonfiguratuEdukiPanela(_edukiPanela);
        }

        private void KonfiguratuEdukiPanela(Panel panela)
        {
            panela.Dock = DockStyle.Fill;
            panela.AutoScroll = true;
            panela.BackColor = Color.Transparent;
            // Padding manualki kudeatuko dugu elementuen X,Y bitartez diseinatzailean
        }

        // -----------------------------------------------------------
        // Irudiak kargatu (Konstruktorian deitua)
        // -----------------------------------------------------------
        protected void KargatuBaliabideak()
        {
            if (this.DesignMode) return;

            // Egurrezko atzeko planoa
            string atzekoPlanoaBidea = Path.Combine(Application.StartupPath, "img", "wood_background.png");
            if (File.Exists(atzekoPlanoaBidea))
            {
                this.BackgroundImage = Image.FromFile(atzekoPlanoaBidea);
                this.BackgroundImageLayout = ImageLayout.Tile;
            }
            else
            {
                this.BackColor = Color.FromArgb(139, 119, 101);
            }
        }

        // -----------------------------------------------------------
        // Eduki panela sortu (FlowLayoutPanel txarteletarako)
        // -----------------------------------------------------------


        // -----------------------------------------------------------
        // Atzera botoia gehitu (azpi-formularioetarako)
        // -----------------------------------------------------------
        protected void GehituAtzeraBotoia()
        {
            _atzeraBotoia = new Button
            {
                Text = "⬅  Atzera",
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(180, 52, 73, 94), // #34495E gardena
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 50),
                Location = new Point(20, 75),
                Cursor = Cursors.Hand
            };
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.Click += (s, e) => this.Close();

            this.Controls.Add(_atzeraBotoia);
            _atzeraBotoia.BringToFront();
        }

        // -----------------------------------------------------------
        // Txartel bat sortu (laguntza-metodoa)
        // -----------------------------------------------------------
        protected CustomCardButton SortuTxartela(string testua, string ikonoFitxategia)
        {
            var txartela = new CustomCardButton
            {
                Testua = testua,
                Size = new Size(300, 200),
                Margin = new Padding(20) // Tartea txartelen artean
            };

            // Ikonoa kargatu
            string ikonoBidea = Path.Combine(Application.StartupPath, "img", "icons", ikonoFitxategia);
            
            // Garapen moduko fallback-ak
            if (!File.Exists(ikonoBidea))
            {
                string root = Directory.GetCurrentDirectory();
                string[] saioak = {
                    Path.Combine(root, "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "GOsasun_app", "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "..", "..", "..", "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", "icons", ikonoFitxategia)
                };

                foreach (string s in saioak)
                {
                    if (File.Exists(s)) { ikonoBidea = s; break; }
                }
            }

            if (File.Exists(ikonoBidea))
            {
                txartela.Ikonoa = Image.FromFile(ikonoBidea);
            }

            return txartela;
        }

        // -----------------------------------------------------------
        // Saioa itxi gertaera
        // -----------------------------------------------------------
        private void GoiburuBarra_SaioaItxi(object? sender, EventArgs e)
        {
            DialogResult emaitza = MessageBox.Show(
                "Ziur zaude saioa itxi nahi duzula?",
                "Saioa Itxi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (emaitza == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
