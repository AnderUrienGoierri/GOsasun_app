using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GOsasun_WinForms
{
    public class DoctorForm : Form
    {
        private Panel pnlTopBar;
        private PictureBox pbLogo;
        private Label lblRole;
        private FlowLayoutPanel flpMenu;
        private LinkLabel lnkHasiera;
        private LinkLabel lnkNirePazienteak;
        private LinkLabel lnkHitzorduak;
        private LinkLabel lnkErrezetak;
        private LinkLabel lnkGrafikak;
        private LinkLabel lnkMezuak;
        private LinkLabel lnkAbisuak;
        private Button btnLogout;
        private PictureBox pbMainImage;

        public DoctorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pnlTopBar = new Panel();
            this.pbLogo = new PictureBox();
            this.lblRole = new Label();
            this.flpMenu = new FlowLayoutPanel();
            this.lnkHasiera = new LinkLabel();
            this.lnkNirePazienteak = new LinkLabel();
            this.lnkHitzorduak = new LinkLabel();
            this.lnkErrezetak = new LinkLabel();
            this.lnkGrafikak = new LinkLabel();
            this.lnkMezuak = new LinkLabel();
            this.lnkAbisuak = new LinkLabel();
            this.btnLogout = new Button();
            this.pbMainImage = new PictureBox();
            
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.flpMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).BeginInit();
            this.SuspendLayout();
            
            // pnlTopBar
            this.pnlTopBar.Dock = DockStyle.Top;
            this.pnlTopBar.Height = 70;
            this.pnlTopBar.BackColor = Color.White;
            this.pnlTopBar.Controls.Add(this.btnLogout); // Add first or set location correctly
            this.pnlTopBar.Controls.Add(this.flpMenu);
            this.pnlTopBar.Controls.Add(this.lblRole);
            this.pnlTopBar.Controls.Add(this.pbLogo);
            
            // pbLogo
            this.pbLogo.Location = new Point(20, 15);
            this.pbLogo.Size = new Size(140, 40);
            this.pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "GOsasun_logo_whatsap.png");
            if (File.Exists(imagePath)) this.pbLogo.Image = Image.FromFile(imagePath);
            
            // lblRole
            this.lblRole.Location = new Point(170, 24);
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            this.lblRole.ForeColor = Color.FromArgb(150, 160, 180); // Grayish blue
            this.lblRole.Text = "Medikua";
            
            // flpMenu
            this.flpMenu.Location = new Point(280, 26);
            this.flpMenu.AutoSize = true;
            this.flpMenu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flpMenu.Controls.Add(this.lnkHasiera);
            this.flpMenu.Controls.Add(this.lnkNirePazienteak);
            this.flpMenu.Controls.Add(this.lnkHitzorduak);
            this.flpMenu.Controls.Add(this.lnkErrezetak);
            this.flpMenu.Controls.Add(this.lnkGrafikak);
            this.flpMenu.Controls.Add(this.lnkMezuak);
            this.flpMenu.Controls.Add(this.lnkAbisuak);
            
            // link common setup
            LinkLabel[] links = { lnkHasiera, lnkNirePazienteak, lnkHitzorduak, lnkErrezetak, lnkGrafikak, lnkMezuak, lnkAbisuak };
            string[] linkTexts = { "Hasiera", "Nire Pazienteak", "Hitzorduak", "Errezetak", "Grafikak", "Mezuak", "Abisuak" };
            for(int i = 0; i < links.Length; i++) {
                links[i].Text = linkTexts[i];
                links[i].AutoSize = true;
                links[i].Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                links[i].LinkColor = Color.FromArgb(80, 80, 100);
                links[i].ActiveLinkColor = Color.FromArgb(0, 123, 255);
                links[i].LinkBehavior = LinkBehavior.HoverUnderline;
                links[i].Margin = new Padding(10, 0, 15, 0);
            }
            // Active style for Hasiera initially
            lnkHasiera.LinkColor = Color.FromArgb(80, 120, 255); // A bit more blue to show active state
            
            // btnLogout
            this.btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLogout.Location = new Point(1020, 15);
            this.btnLogout.Size = new Size(110, 40);
            this.btnLogout.Text = "Saioa Itxi";
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderColor = Color.FromArgb(60, 100, 220);
            this.btnLogout.FlatAppearance.BorderSize = 2;
            this.btnLogout.ForeColor = Color.FromArgb(60, 100, 220);
            this.btnLogout.BackColor = Color.White;
            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogout.Cursor = Cursors.Hand;
            this.btnLogout.Click += BtnLogout_Click;

            // pbMainImage
            this.pbMainImage.Dock = DockStyle.Fill;
            this.pbMainImage.SizeMode = PictureBoxSizeMode.Zoom;
            string mainImagePath = Path.Combine(Application.StartupPath, "Assets", "index_medikua.png");
            if (File.Exists(mainImagePath)) this.pbMainImage.Image = Image.FromFile(mainImagePath);
            
            // DoctorForm
            this.ClientSize = new Size(1160, 700); // Increased width
            this.Controls.Add(this.pbMainImage); // Added first so it's behind top bar or docked correctly
            this.Controls.Add(this.pnlTopBar); 
            this.Name = "DoctorForm";
            this.Text = "Mediku Menua";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.flpMenu.ResumeLayout(false);
            this.flpMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).EndInit();
            this.ResumeLayout(false);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
