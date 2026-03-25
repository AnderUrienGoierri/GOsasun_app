// ============================================================
// MenuNagusia.cs - Menu Nagusia (Main Menu)
// ============================================================
// Aplikazioaren sarrera nagusia login egin ondoren.
// Erabiltzailearen rolaren arabera (Pazientea/Medikua)
// txartelak dinamikoki kargatzen ditu.
// ============================================================

using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Menu nagusiaren formularioa.
    /// Rolaren arabera txartel desberdinak erakusten ditu.
    /// </summary>
    public class MenuNagusia : OinarriFormularioa
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public MenuNagusia() : base()
        {
            // Diseinatzailearentzat
        }

        public MenuNagusia(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            KargatuTxartelak();
        }

        // -----------------------------------------------------------
        // Txartelak kargatu rolaren arabera
        // -----------------------------------------------------------
        private void KargatuTxartelak()
        {
            _edukiPanela.Controls.Clear();

            if (this.DesignMode) return;
            if (_erabiltzailea == null) return;

            if (_erabiltzailea.DaPazientea())
            {
                SortuPazienteMenua();
            }
            else if (_erabiltzailea.DaMedikua())
            {
                SortuMedikuMenua();
            }
        }

        // -----------------------------------------------------------
        // PAZIENTEAREN menuko txartelak
        // -----------------------------------------------------------
        private void SortuPazienteMenua()
        {
            // 1. Kontaktua
            var btnKontaktua = SortuTxartela("KONTAKTUA", "kontaktua.png");
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnKontaktua);

            // 2. Osasuna
            var btnOsasuna = SortuTxartela("OSASUNA", "osasuna.png");
            btnOsasuna.Click += (s, e) => IrekiFormularioa(new OsasunaFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnOsasuna);
        }

        // -----------------------------------------------------------
        // MEDIKUAREN menuko txartelak
        // -----------------------------------------------------------
        private void SortuMedikuMenua()
        {
            // 1. Nire Pazienteak
            var btnPazienteak = SortuTxartela("NIRE PAZIENTEAK", "pazienteak.png");
            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteenFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnPazienteak);

            // 2. Kontaktua
            var btnKontaktua = SortuTxartela("KONTAKTUA", "kontaktua.png");
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnKontaktua);

            // 3. Neurketak
            var btnNeurketak = SortuTxartela("NEURKETAK", "neurketak.png");
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketenFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnNeurketak);

            // 4. Errezetak
            var btnErrezetak = SortuTxartela("ERREZETAK", "errezetak.png");
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetenFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnErrezetak);

            // 5. Grafikak
            var btnGrafikak = SortuTxartela("GRAFIKAK", "grafikak.png");
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new GrafikenFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnGrafikak);

            // 6. Abisuak
            var btnAbisuak = SortuTxartela("ABISUAK", "abisuak.png");
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new AbisuenFormularioa(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnAbisuak);
        }

        // -----------------------------------------------------------
        // Azpi-formularioa ireki eta hau ezkutatu (itxita bueltatu)
        // -----------------------------------------------------------
        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
