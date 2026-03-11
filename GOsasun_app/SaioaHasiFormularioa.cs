using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GOsasun_WinForms
{
    public class SaioaHasiFormularioa : Form
    {
        private PictureBox pbLogo;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblError;

        public SaioaHasiFormularioa()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pbLogo = new PictureBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblError = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            
            // pbLogo
            this.pbLogo.Location = new Point(125, 20);
            this.pbLogo.Size = new Size(150, 150);
            this.pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            string imagePath = Path.Combine(Application.StartupPath, "Assets", "GOsasun_logoa_whatsap.png");
            if (File.Exists(imagePath))
            {
                this.pbLogo.Image = Image.FromFile(imagePath);
            }
            
            // lblEmail
            this.lblEmail.Location = new Point(50, 190);
            this.lblEmail.AutoSize = true;
            this.lblEmail.Text = "E-posta helbidea:";
            
            // txtEmail
            this.txtEmail.Location = new Point(50, 215);
            this.txtEmail.Size = new Size(300, 20);
            
            // lblPassword
            this.lblPassword.Location = new Point(50, 250);
            this.lblPassword.AutoSize = true;
            this.lblPassword.Text = "Pasahitza:";
            
            // txtPassword
            this.txtPassword.Location = new Point(50, 275);
            this.txtPassword.Size = new Size(300, 20);
            this.txtPassword.PasswordChar = '*';
            
            // btnLogin
            this.btnLogin.Location = new Point(50, 320);
            this.btnLogin.Size = new Size(300, 40);
            this.btnLogin.Text = "Sartu";
            this.btnLogin.BackColor = Color.FromArgb(0, 123, 255);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);
            
            // lblError
            this.lblError.Location = new Point(50, 380);
            this.lblError.Size = new Size(300, 40);
            this.lblError.ForeColor = Color.Red;
            this.lblError.Text = "";
            
            // SaioaHasiFormularioa
            this.ClientSize = new Size(400, 450);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblError);
            this.Name = "SaioaHasiFormularioa";
            this.Text = "Saioa Hasi - GOsasun";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Mesedez, bete eremu guztiak.";
                return;
            }

            try
            {
                DatuBaseKonexioa dbConn = new DatuBaseKonexioa();
                using (MySqlConnection conn = dbConn.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT erabiltzaile_id, email, pasahitza, rol_id, rol_izena FROM V_Login WHERE email = @email AND aktibo = 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["pasahitza"].ToString();
                                string rolIzen = reader["rol_izena"].ToString();

                                if (password == storedPassword)
                                {
                                    // Login arrakastatsua
                                    this.Hide();

                                    Form nextForm = null;
                                    if (rolIzen == "Medikua")
                                    {
                                        nextForm = new MedikuFormularioa();
                                    }
                                    else if (rolIzen == "Harrera")
                                    {
                                        nextForm = new HarrerakoFormularioa();
                                    }

                                    if (nextForm != null)
                                    {
                                        nextForm.FormClosed += (s, args) => this.Close();
                                        nextForm.Show();
                                    }
                                    else
                                    {
                                        lblError.Text = "Sarbide deuseztatua rol ezezagunagatik edo baimen faltagatik.";
                                        this.Show();
                                    }
                                }
                                else
                                {
                                    lblError.Text = "Helbide elektronikoa edo pasahitza ez dira zuzenak.";
                                }
                            }
                            else
                            {
                                lblError.Text = "Helbide elektronikoa edo pasahitza ez dira zuzenak.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Errorea datu-basean: " + ex.Message;
            }
        }
    }
}
