using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GOsasun_WinForms
{
    public class ReceptionistForm : Form
    {
        private PictureBox pbMainImage;

        public ReceptionistForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pbMainImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).BeginInit();
            this.SuspendLayout();
            
            // pbMainImage
            this.pbMainImage.Dock = DockStyle.Fill;
            this.pbMainImage.SizeMode = PictureBoxSizeMode.Zoom;
            
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "index_harrera.png");
            if (File.Exists(imagePath))
            {
                this.pbMainImage.Image = Image.FromFile(imagePath);
            }
            // 
            // ReceptionistForm
            // 
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.pbMainImage);
            this.Name = "ReceptionistForm";
            this.Text = "Harrerako Menua";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
