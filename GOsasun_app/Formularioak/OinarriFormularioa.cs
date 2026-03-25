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
    public class OinarriFormularioa : Form
    {
        // -----------------------------------------------------------
        // Osagaiak
        // -----------------------------------------------------------
        protected GoiburuBarra? _goiburuBarra;
        protected FlowLayoutPanel _edukiPanela;
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

        private void InitializeComponent()
        {
            _edukiPanela = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.AutoScroll = true;
            _edukiPanela.BackColor = Color.Transparent;
            _edukiPanela.Dock = DockStyle.Fill;
            _edukiPanela.Location = new Point(0, 0);
            _edukiPanela.Name = "_edukiPanela";
            _edukiPanela.Padding = new Padding(40);
            _edukiPanela.Size = new Size(1600, 1000);
            _edukiPanela.TabIndex = 0;
            // 
            // OinarriFormularioa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1600, 1000);
            Controls.Add(_edukiPanela);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "OinarriFormularioa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GOsasun";
            ResumeLayout(false);

            // Goiburuko barra (diseinatzailean ere ikusteko) - Moved here to ensure it's added after _edukiPanela
            _goiburuBarra = new GoiburuBarra("Erabiltzailea", "Rola");
            this.Controls.Add(_goiburuBarra);
            _goiburuBarra.BringToFront();
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
                this.Controls.Remove(_goiburuBarra);
            }
            
            _goiburuBarra = new GoiburuBarra(_erabiltzailea.IzenOsoa, _erabiltzailea.Rola);
            _goiburuBarra.SaioaItxi += GoiburuBarra_SaioaItxi;
            this.Controls.Add(_goiburuBarra);
            _goiburuBarra.BringToFront();
        }

        // -----------------------------------------------------------
        // Formularioaren oinarrizko konfigurazioa
        // -----------------------------------------------------------
        private void KonfiguratuFormularioa()
        {
            // Tamaina eta estiloa (Tablet/Desktop: 1600x1000)
            this.ClientSize = new Size(1600, 1000);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Text = "GOsasun";

            // Eduki panela konfiguratu
            KonfiguratuEdukiPanela(_edukiPanela);
        }

        private void KonfiguratuEdukiPanela(FlowLayoutPanel panela)
        {
            panela.Dock = DockStyle.Fill;
            panela.AutoScroll = true;
            panela.BackColor = Color.Transparent;
            panela.Padding = new Padding(40, 100, 40, 40);
            panela.WrapContents = true;
            panela.FlowDirection = FlowDirection.LeftToRight;

            // Horizontalki zentraldu txartelak (3 zutabe tablet-erako optimizatuta)
            panela.Resize += (s, e) =>
            {
                int txartelZabalera = 400 + 40; // Zabalera + Margin
                int tzkop = 3; // 3 zutabe nahi ditugu tablet-erako grid-ean
                int guztiraZabalera = tzkop * txartelZabalera;
                int ezkerTartea = Math.Max(40, (panela.Width - guztiraZabalera) / 2);
                panela.Padding = new Padding(ezkerTartea, 100, ezkerTartea, 40);
            };
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
                Size = new Size(400, 320),
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
