using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    internal static class JarraipenOharLaguntzailea
    {
        public static string? EskatuAukerakoOharra(IWin32Window? jabea, string izenburua, string azalpena)
        {
            using Form elkarrizketa = new Form
            {
                Text = izenburua,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(720, 420)
            };

            Label lblAzalpena = new Label
            {
                AutoSize = false,
                Text = azalpena,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(24, 24),
                Size = new Size(672, 70)
            };

            TextBox txtOharra = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(24, 108),
                Size = new Size(672, 220),
                PlaceholderText = "Idatzi nahi baduzu, jarraipen honen oharra hemen..."
            };

            Button btnOharrikGabe = new Button
            {
                Text = "Oharrik gabe",
                DialogResult = DialogResult.No,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(358, 346),
                Size = new Size(160, 46)
            };
            btnOharrikGabe.FlatAppearance.BorderSize = 0;

            Button btnGordeOharra = new Button
            {
                Text = "Oharra gorde",
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(536, 346),
                Size = new Size(160, 46)
            };
            btnGordeOharra.FlatAppearance.BorderSize = 0;

            elkarrizketa.AcceptButton = btnGordeOharra;
            elkarrizketa.CancelButton = btnOharrikGabe;
            elkarrizketa.Controls.Add(lblAzalpena);
            elkarrizketa.Controls.Add(txtOharra);
            elkarrizketa.Controls.Add(btnOharrikGabe);
            elkarrizketa.Controls.Add(btnGordeOharra);

            DialogResult emaitza = jabea == null ? elkarrizketa.ShowDialog() : elkarrizketa.ShowDialog(jabea);
            if (emaitza != DialogResult.OK)
            {
                return null;
            }

            string testua = txtOharra.Text.Trim();
            return string.IsNullOrWhiteSpace(testua) ? null : testua;
        }

        public static string BatuOharrak(params string?[] zatiak)
        {
            return string.Join(Environment.NewLine, zatiak.Where(zatia => !string.IsNullOrWhiteSpace(zatia)).Select(zatia => zatia!.Trim()));
        }
    }
}