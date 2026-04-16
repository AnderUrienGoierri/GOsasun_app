using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class ErabiltzaileaSortu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                this.pbIrudia?.Image?.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblIntegrazioa = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblHizkuntza = new System.Windows.Forms.Label();
            this.cmbHizkuntza = new System.Windows.Forms.ComboBox();
            this.lblIzena = new System.Windows.Forms.Label();
            this.txtIzena = new System.Windows.Forms.TextBox();
            this.lblAbizenak = new System.Windows.Forms.Label();
            this.txtAbizenak = new System.Windows.Forms.TextBox();
            this.lblEmaila = new System.Windows.Forms.Label();
            this.txtEmaila = new System.Windows.Forms.TextBox();
            this.lblPasahitza = new System.Windows.Forms.Label();
            this.txtPasahitza = new System.Windows.Forms.TextBox();
            this.lblNan = new System.Windows.Forms.Label();
            this.txtNan = new System.Windows.Forms.TextBox();
            this.lblSexua = new System.Windows.Forms.Label();
            this.cmbSexua = new System.Windows.Forms.ComboBox();
            this.lblJaiotzeData = new System.Windows.Forms.Label();
            this.dtpJaiotzeData = new System.Windows.Forms.DateTimePicker();
            this.lblTelefonoa = new System.Windows.Forms.Label();
            this.txtTelefonoa = new System.Windows.Forms.TextBox();
            this.lblHelbidea = new System.Windows.Forms.Label();
            this.txtHelbidea = new System.Windows.Forms.TextBox();
            this.lblHerria = new System.Windows.Forms.Label();
            this.txtHerria = new System.Windows.Forms.TextBox();
            this.lblPostaKodea = new System.Windows.Forms.Label();
            this.txtPostaKodea = new System.Windows.Forms.TextBox();
            this.lblElkargokide = new System.Windows.Forms.Label();
            this.txtElkargokide = new System.Windows.Forms.TextBox();
            this.lblEspezialitatea = new System.Windows.Forms.Label();
            this.txtEspezialitatea = new System.Windows.Forms.TextBox();
            this.lblKontsulta = new System.Windows.Forms.Label();
            this.txtKontsulta = new System.Windows.Forms.TextBox();
            this.lblLanaldia = new System.Windows.Forms.Label();
            this.cmbLanaldia = new System.Windows.Forms.ComboBox();
            this.lblTxanda = new System.Windows.Forms.Label();
            this.cmbTxanda = new System.Windows.Forms.ComboBox();
            this.lblIrudia = new System.Windows.Forms.Label();
            this.pbIrudia = new System.Windows.Forms.PictureBox();
            this.btnIrudiaAukeratu = new System.Windows.Forms.Button();
            this.lblIrudiFitxategia = new System.Windows.Forms.Label();
            this.lblOsasunLangilea = new System.Windows.Forms.Label();
            this.cmbOsasunLangileak = new System.Windows.Forms.ComboBox();
            this.btnLangileaGehitu = new System.Windows.Forms.Button();
            this.lblEsleitutakoLangileak = new System.Windows.Forms.Label();
            this.lstEsleitutakoLangileak = new System.Windows.Forms.ListBox();
            this.btnLangileaKendu = new System.Windows.Forms.Button();
            this.btnGorde = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbIrudia)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            //
            // lblIntegrazioa
            //
            this.lblIntegrazioa.AutoSize = true;
            this.lblIntegrazioa.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblIntegrazioa.Location = new System.Drawing.Point(40, 20);
            this.lblIntegrazioa.Name = "lblIntegrazioa";
            this.lblIntegrazioa.Size = new System.Drawing.Size(720, 86);
            this.lblIntegrazioa.TabIndex = 1;
            this.lblIntegrazioa.Text = "Erabiltzaile Berria Sortu";
            //
            // pnlForm
            //
            this.pnlForm.AutoScroll = true;
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.lblHizkuntza);
            this.pnlForm.Controls.Add(this.cmbHizkuntza);
            this.pnlForm.Controls.Add(this.lblIzena);
            this.pnlForm.Controls.Add(this.txtIzena);
            this.pnlForm.Controls.Add(this.lblAbizenak);
            this.pnlForm.Controls.Add(this.txtAbizenak);
            this.pnlForm.Controls.Add(this.lblEmaila);
            this.pnlForm.Controls.Add(this.txtEmaila);
            this.pnlForm.Controls.Add(this.lblPasahitza);
            this.pnlForm.Controls.Add(this.txtPasahitza);
            this.pnlForm.Controls.Add(this.lblNan);
            this.pnlForm.Controls.Add(this.txtNan);
            this.pnlForm.Controls.Add(this.lblSexua);
            this.pnlForm.Controls.Add(this.cmbSexua);
            this.pnlForm.Controls.Add(this.lblJaiotzeData);
            this.pnlForm.Controls.Add(this.dtpJaiotzeData);
            this.pnlForm.Controls.Add(this.lblTelefonoa);
            this.pnlForm.Controls.Add(this.txtTelefonoa);
            this.pnlForm.Controls.Add(this.lblHelbidea);
            this.pnlForm.Controls.Add(this.txtHelbidea);
            this.pnlForm.Controls.Add(this.lblHerria);
            this.pnlForm.Controls.Add(this.txtHerria);
            this.pnlForm.Controls.Add(this.lblPostaKodea);
            this.pnlForm.Controls.Add(this.txtPostaKodea);
            this.pnlForm.Controls.Add(this.lblElkargokide);
            this.pnlForm.Controls.Add(this.txtElkargokide);
            this.pnlForm.Controls.Add(this.lblEspezialitatea);
            this.pnlForm.Controls.Add(this.txtEspezialitatea);
            this.pnlForm.Controls.Add(this.lblKontsulta);
            this.pnlForm.Controls.Add(this.txtKontsulta);
            this.pnlForm.Controls.Add(this.lblLanaldia);
            this.pnlForm.Controls.Add(this.cmbLanaldia);
            this.pnlForm.Controls.Add(this.lblTxanda);
            this.pnlForm.Controls.Add(this.cmbTxanda);
            this.pnlForm.Controls.Add(this.lblIrudia);
            this.pnlForm.Controls.Add(this.pbIrudia);
            this.pnlForm.Controls.Add(this.btnIrudiaAukeratu);
            this.pnlForm.Controls.Add(this.lblIrudiFitxategia);
            this.pnlForm.Controls.Add(this.lblOsasunLangilea);
            this.pnlForm.Controls.Add(this.cmbOsasunLangileak);
            this.pnlForm.Controls.Add(this.btnLangileaGehitu);
            this.pnlForm.Controls.Add(this.lblEsleitutakoLangileak);
            this.pnlForm.Controls.Add(this.lstEsleitutakoLangileak);
            this.pnlForm.Controls.Add(this.btnLangileaKendu);
            this.pnlForm.Controls.Add(this.btnGorde);
            this.pnlForm.Location = new System.Drawing.Point(50, 110);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(1200, 850);
            this.pnlForm.TabIndex = 0;
            //
            // lblHizkuntza
            //
            this.lblHizkuntza.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHizkuntza.Location = new System.Drawing.Point(40, 30);
            this.lblHizkuntza.Size = new System.Drawing.Size(200, 40);
            this.lblHizkuntza.Text = "Hizkuntza:";
            //
            // cmbHizkuntza
            //
            this.cmbHizkuntza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHizkuntza.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbHizkuntza.Items.AddRange(new object[] { "Euskara", "Gaztelania" });
            this.cmbHizkuntza.Location = new System.Drawing.Point(300, 30);
            this.cmbHizkuntza.Size = new System.Drawing.Size(600, 48);
            //
            // lblIzena
            //
            this.lblIzena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblIzena.Location = new System.Drawing.Point(40, 90);
            this.lblIzena.Size = new System.Drawing.Size(200, 40);
            this.lblIzena.Text = "Izena (*):";
            //
            // txtIzena
            //
            this.txtIzena.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtIzena.Location = new System.Drawing.Point(300, 90);
            this.txtIzena.Size = new System.Drawing.Size(600, 47);
            //
            // lblAbizenak
            //
            this.lblAbizenak.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblAbizenak.Location = new System.Drawing.Point(40, 150);
            this.lblAbizenak.Size = new System.Drawing.Size(200, 40);
            this.lblAbizenak.Text = "Abizenak (*):";
            //
            // txtAbizenak
            //
            this.txtAbizenak.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAbizenak.Location = new System.Drawing.Point(300, 150);
            this.txtAbizenak.Size = new System.Drawing.Size(600, 47);
            //
            // lblEmaila
            //
            this.lblEmaila.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmaila.Location = new System.Drawing.Point(40, 210);
            this.lblEmaila.Size = new System.Drawing.Size(200, 40);
            this.lblEmaila.Text = "Emaila (*):";
            //
            // txtEmaila
            //
            this.txtEmaila.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmaila.Location = new System.Drawing.Point(300, 210);
            this.txtEmaila.Size = new System.Drawing.Size(600, 47);
            //
            // lblPasahitza
            //
            this.lblPasahitza.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPasahitza.Location = new System.Drawing.Point(40, 270);
            this.lblPasahitza.Size = new System.Drawing.Size(200, 40);
            this.lblPasahitza.Text = "Pasahitza (*):";
            //
            // txtPasahitza
            //
            this.txtPasahitza.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPasahitza.Location = new System.Drawing.Point(300, 270);
            this.txtPasahitza.Size = new System.Drawing.Size(600, 47);
            this.txtPasahitza.UseSystemPasswordChar = true;
            //
            // lblNan
            //
            this.lblNan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNan.Location = new System.Drawing.Point(40, 330);
            this.lblNan.Size = new System.Drawing.Size(200, 40);
            this.lblNan.Text = "NAN/DNI (*):";
            //
            // txtNan
            //
            this.txtNan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNan.Location = new System.Drawing.Point(300, 330);
            this.txtNan.Size = new System.Drawing.Size(600, 47);
            //
            // lblSexua
            //
            this.lblSexua.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSexua.Location = new System.Drawing.Point(40, 390);
            this.lblSexua.Size = new System.Drawing.Size(200, 40);
            this.lblSexua.Text = "Sexua:";
            //
            // cmbSexua
            //
            this.cmbSexua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexua.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbSexua.Items.AddRange(new object[] { "Gizona", "Emakumea" });
            this.cmbSexua.Location = new System.Drawing.Point(300, 390);
            this.cmbSexua.Size = new System.Drawing.Size(600, 48);
            //
            // lblJaiotzeData
            //
            this.lblJaiotzeData.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblJaiotzeData.Location = new System.Drawing.Point(40, 450);
            this.lblJaiotzeData.Size = new System.Drawing.Size(200, 40);
            this.lblJaiotzeData.Text = "Jaiotze data:";
            //
            // dtpJaiotzeData
            //
            this.dtpJaiotzeData.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpJaiotzeData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpJaiotzeData.Location = new System.Drawing.Point(300, 450);
            this.dtpJaiotzeData.Size = new System.Drawing.Size(600, 47);
            //
            // lblTelefonoa
            //
            this.lblTelefonoa.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTelefonoa.Location = new System.Drawing.Point(40, 510);
            this.lblTelefonoa.Size = new System.Drawing.Size(200, 40);
            this.lblTelefonoa.Text = "Telefonoa:";
            //
            // txtTelefonoa
            //
            this.txtTelefonoa.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTelefonoa.Location = new System.Drawing.Point(300, 510);
            this.txtTelefonoa.Size = new System.Drawing.Size(600, 47);
            //
            // lblHelbidea
            //
            this.lblHelbidea.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHelbidea.Location = new System.Drawing.Point(40, 570);
            this.lblHelbidea.Size = new System.Drawing.Size(200, 40);
            this.lblHelbidea.Text = "Helbidea:";
            //
            // txtHelbidea
            //
            this.txtHelbidea.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHelbidea.Location = new System.Drawing.Point(300, 570);
            this.txtHelbidea.Size = new System.Drawing.Size(600, 47);
            //
            // lblHerria
            //
            this.lblHerria.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHerria.Location = new System.Drawing.Point(40, 630);
            this.lblHerria.Size = new System.Drawing.Size(200, 40);
            this.lblHerria.Text = "Herria / PK:";
            //
            // txtHerria
            //
            this.txtHerria.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHerria.Location = new System.Drawing.Point(300, 630);
            this.txtHerria.Size = new System.Drawing.Size(300, 47);
            //
            // lblPostaKodea
            //
            this.lblPostaKodea.Location = new System.Drawing.Point(0, 0);
            this.lblPostaKodea.Name = "lblPostaKodea";
            this.lblPostaKodea.Size = new System.Drawing.Size(100, 23);
            this.lblPostaKodea.TabIndex = 0;
            //
            // txtPostaKodea
            //
            this.txtPostaKodea.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPostaKodea.Location = new System.Drawing.Point(610, 630);
            this.txtPostaKodea.Size = new System.Drawing.Size(290, 47);
            //
            // lblElkargokide
            //
            this.lblElkargokide.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblElkargokide.Location = new System.Drawing.Point(40, 690);
            this.lblElkargokide.Size = new System.Drawing.Size(220, 40);
            this.lblElkargokide.Text = "Elkargokide / Esp:";
            //
            // txtElkargokide
            //
            this.txtElkargokide.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtElkargokide.Location = new System.Drawing.Point(300, 690);
            this.txtElkargokide.Size = new System.Drawing.Size(300, 47);
            //
            // lblEspezialitatea
            //
            this.lblEspezialitatea.Location = new System.Drawing.Point(0, 0);
            this.lblEspezialitatea.Name = "lblEspezialitatea";
            this.lblEspezialitatea.Size = new System.Drawing.Size(233, 50);
            this.lblEspezialitatea.TabIndex = 0;
            //
            // txtEspezialitatea
            //
            this.txtEspezialitatea.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEspezialitatea.Location = new System.Drawing.Point(610, 690);
            this.txtEspezialitatea.Size = new System.Drawing.Size(290, 47);
            //
            // lblKontsulta
            //
            this.lblKontsulta.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblKontsulta.Location = new System.Drawing.Point(40, 750);
            this.lblKontsulta.Size = new System.Drawing.Size(220, 40);
            this.lblKontsulta.Text = "Kontsulta / Lan:";
            //
            // txtKontsulta
            //
            this.txtKontsulta.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKontsulta.Location = new System.Drawing.Point(300, 750);
            this.txtKontsulta.Size = new System.Drawing.Size(300, 47);
            //
            // lblLanaldia
            //
            this.lblLanaldia.Location = new System.Drawing.Point(0, 0);
            this.lblLanaldia.Name = "lblLanaldia";
            this.lblLanaldia.Size = new System.Drawing.Size(100, 23);
            this.lblLanaldia.TabIndex = 0;
            //
            // cmbLanaldia
            //
            this.cmbLanaldia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanaldia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbLanaldia.Items.AddRange(new object[] { "Osoa", "Murriztua" });
            this.cmbLanaldia.Location = new System.Drawing.Point(610, 750);
            this.cmbLanaldia.Size = new System.Drawing.Size(290, 48);
            //
            // lblTxanda
            //
            this.lblTxanda.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTxanda.Location = new System.Drawing.Point(40, 810);
            this.lblTxanda.Size = new System.Drawing.Size(200, 40);
            this.lblTxanda.Text = "Txanda:";
            //
            // cmbTxanda
            //
            this.cmbTxanda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTxanda.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTxanda.Items.AddRange(new object[] { "Goizez", "Arratsaldez", "Gauez" });
            this.cmbTxanda.Location = new System.Drawing.Point(300, 810);
            this.cmbTxanda.Size = new System.Drawing.Size(600, 48);
            //
            // lblIrudia
            //
            this.lblIrudia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblIrudia.Location = new System.Drawing.Point(40, 890);
            this.lblIrudia.Name = "lblIrudia";
            this.lblIrudia.Size = new System.Drawing.Size(200, 40);
            this.lblIrudia.TabIndex = 21;
            this.lblIrudia.Text = "Irudia:";
            //
            // pbIrudia
            //
            this.pbIrudia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbIrudia.Location = new System.Drawing.Point(300, 890);
            this.pbIrudia.Name = "pbIrudia";
            this.pbIrudia.Size = new System.Drawing.Size(180, 180);
            this.pbIrudia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIrudia.TabIndex = 22;
            this.pbIrudia.TabStop = false;
            //
            // btnIrudiaAukeratu
            //
            this.btnIrudiaAukeratu.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnIrudiaAukeratu.FlatAppearance.BorderSize = 0;
            this.btnIrudiaAukeratu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIrudiaAukeratu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnIrudiaAukeratu.ForeColor = System.Drawing.Color.White;
            this.btnIrudiaAukeratu.Location = new System.Drawing.Point(520, 905);
            this.btnIrudiaAukeratu.Name = "btnIrudiaAukeratu";
            this.btnIrudiaAukeratu.Size = new System.Drawing.Size(250, 56);
            this.btnIrudiaAukeratu.TabIndex = 23;
            this.btnIrudiaAukeratu.Text = "Irudia aukeratu";
            this.btnIrudiaAukeratu.UseVisualStyleBackColor = false;
            //
            // lblIrudiFitxategia
            //
            this.lblIrudiFitxategia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIrudiFitxategia.Location = new System.Drawing.Point(520, 980);
            this.lblIrudiFitxategia.Name = "lblIrudiFitxategia";
            this.lblIrudiFitxategia.Size = new System.Drawing.Size(380, 90);
            this.lblIrudiFitxategia.TabIndex = 24;
            this.lblIrudiFitxategia.Text = "Irudi lehenetsia";
            //
            // lblOsasunLangilea
            //
            this.lblOsasunLangilea.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblOsasunLangilea.Location = new System.Drawing.Point(40, 1100);
            this.lblOsasunLangilea.Name = "lblOsasunLangilea";
            this.lblOsasunLangilea.Size = new System.Drawing.Size(240, 40);
            this.lblOsasunLangilea.TabIndex = 25;
            this.lblOsasunLangilea.Text = "Osasun langilea(k):";
            //
            // cmbOsasunLangileak
            //
            this.cmbOsasunLangileak.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbOsasunLangileak.FormattingEnabled = true;
            this.cmbOsasunLangileak.Location = new System.Drawing.Point(300, 1100);
            this.cmbOsasunLangileak.Name = "cmbOsasunLangileak";
            this.cmbOsasunLangileak.Size = new System.Drawing.Size(450, 48);
            this.cmbOsasunLangileak.TabIndex = 26;
            //
            // btnLangileaGehitu
            //
            this.btnLangileaGehitu.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnLangileaGehitu.FlatAppearance.BorderSize = 0;
            this.btnLangileaGehitu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLangileaGehitu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLangileaGehitu.ForeColor = System.Drawing.Color.White;
            this.btnLangileaGehitu.Location = new System.Drawing.Point(770, 1096);
            this.btnLangileaGehitu.Name = "btnLangileaGehitu";
            this.btnLangileaGehitu.Size = new System.Drawing.Size(130, 56);
            this.btnLangileaGehitu.TabIndex = 27;
            this.btnLangileaGehitu.Text = "Gehitu";
            this.btnLangileaGehitu.UseVisualStyleBackColor = false;
            //
            // lblEsleitutakoLangileak
            //
            this.lblEsleitutakoLangileak.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblEsleitutakoLangileak.Location = new System.Drawing.Point(300, 1165);
            this.lblEsleitutakoLangileak.Name = "lblEsleitutakoLangileak";
            this.lblEsleitutakoLangileak.Size = new System.Drawing.Size(360, 40);
            this.lblEsleitutakoLangileak.TabIndex = 28;
            this.lblEsleitutakoLangileak.Text = "Esleitutako osasun langileak";
            //
            // lstEsleitutakoLangileak
            //
            this.lstEsleitutakoLangileak.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lstEsleitutakoLangileak.FormattingEnabled = true;
            this.lstEsleitutakoLangileak.ItemHeight = 37;
            this.lstEsleitutakoLangileak.Location = new System.Drawing.Point(300, 1210);
            this.lstEsleitutakoLangileak.Name = "lstEsleitutakoLangileak";
            this.lstEsleitutakoLangileak.Size = new System.Drawing.Size(600, 152);
            this.lstEsleitutakoLangileak.TabIndex = 29;
            //
            // btnLangileaKendu
            //
            this.btnLangileaKendu.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnLangileaKendu.FlatAppearance.BorderSize = 0;
            this.btnLangileaKendu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLangileaKendu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLangileaKendu.ForeColor = System.Drawing.Color.White;
            this.btnLangileaKendu.Location = new System.Drawing.Point(920, 1210);
            this.btnLangileaKendu.Name = "btnLangileaKendu";
            this.btnLangileaKendu.Size = new System.Drawing.Size(180, 56);
            this.btnLangileaKendu.TabIndex = 30;
            this.btnLangileaKendu.Text = "Kendu";
            this.btnLangileaKendu.UseVisualStyleBackColor = false;
            //
            // btnGorde
            //
            this.btnGorde.BackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            this.btnGorde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGorde.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnGorde.ForeColor = System.Drawing.Color.White;
            this.btnGorde.Location = new System.Drawing.Point(350, 1390);
            this.btnGorde.Name = "btnGorde";
            this.btnGorde.Size = new System.Drawing.Size(300, 60);
            this.btnGorde.TabIndex = 31;
            this.btnGorde.Text = "GORDE";
            this.btnGorde.UseVisualStyleBackColor = false;
            this.btnGorde.Click += new System.EventHandler(this.btnGorde_Click);
            //
            // ErabiltzaileaSortu
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1159);
            this.Name = "ErabiltzaileaSortu";
            this.Text = "Erabiltzaile Berria Sortu";
            ((System.ComponentModel.ISupportInitialize)(this.pbIrudia)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblIntegrazioa;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblIzena;
        private System.Windows.Forms.TextBox txtIzena;
        private System.Windows.Forms.Label lblAbizenak;
        private System.Windows.Forms.TextBox txtAbizenak;
        private System.Windows.Forms.Label lblEmaila;
        private System.Windows.Forms.TextBox txtEmaila;
        private System.Windows.Forms.Label lblPasahitza;
        private System.Windows.Forms.TextBox txtPasahitza;
        private System.Windows.Forms.Label lblNan;
        private System.Windows.Forms.TextBox txtNan;
        private System.Windows.Forms.Label lblSexua;
        private System.Windows.Forms.ComboBox cmbSexua;
        private System.Windows.Forms.Label lblJaiotzeData;
        private System.Windows.Forms.DateTimePicker dtpJaiotzeData;
        private System.Windows.Forms.Label lblTelefonoa;
        private System.Windows.Forms.TextBox txtTelefonoa;
        private System.Windows.Forms.Label lblHelbidea;
        private System.Windows.Forms.TextBox txtHelbidea;
        private System.Windows.Forms.Label lblHerria;
        private System.Windows.Forms.TextBox txtHerria;
        private System.Windows.Forms.Label lblPostaKodea;
        private System.Windows.Forms.TextBox txtPostaKodea;
        private System.Windows.Forms.Label lblElkargokide;
        private System.Windows.Forms.TextBox txtElkargokide;
        private System.Windows.Forms.Label lblEspezialitatea;
        private System.Windows.Forms.TextBox txtEspezialitatea;
        private System.Windows.Forms.Label lblKontsulta;
        private System.Windows.Forms.TextBox txtKontsulta;
        private System.Windows.Forms.Label lblLanaldia;
        private System.Windows.Forms.ComboBox cmbLanaldia;
        private System.Windows.Forms.Label lblTxanda;
        private System.Windows.Forms.ComboBox cmbTxanda;
        private System.Windows.Forms.Label lblHizkuntza;
        private System.Windows.Forms.ComboBox cmbHizkuntza;
        private System.Windows.Forms.Label lblIrudia;
        private System.Windows.Forms.PictureBox pbIrudia;
        private System.Windows.Forms.Button btnIrudiaAukeratu;
        private System.Windows.Forms.Label lblIrudiFitxategia;
        private System.Windows.Forms.Label lblOsasunLangilea;
        private System.Windows.Forms.ComboBox cmbOsasunLangileak;
        private System.Windows.Forms.Button btnLangileaGehitu;
        private System.Windows.Forms.Label lblEsleitutakoLangileak;
        private System.Windows.Forms.ListBox lstEsleitutakoLangileak;
        private System.Windows.Forms.Button btnLangileaKendu;
        private System.Windows.Forms.Button btnGorde;
    }
}
