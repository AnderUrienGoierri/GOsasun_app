namespace GOsasun_app.Interfazea
{
    partial class JarraipenXehetasunakLaguntzailea
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlpXehetasunak = new TableLayoutPanel();
            lblOharrak = new Label();
            txtOharrak = new TextBox();
            btnItxi = new Button();
            SuspendLayout();
            // 
            // tlpXehetasunak
            // 
            tlpXehetasunak.ColumnCount = 2;
            tlpXehetasunak.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpXehetasunak.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tlpXehetasunak.Location = new Point(24, 24);
            tlpXehetasunak.Name = "tlpXehetasunak";
            tlpXehetasunak.RowCount = 0;
            tlpXehetasunak.Size = new Size(792, 434);
            tlpXehetasunak.TabIndex = 0;
            // 
            // lblOharrak
            // 
            lblOharrak.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOharrak.Location = new Point(24, 470);
            lblOharrak.Name = "lblOharrak";
            lblOharrak.Size = new Size(120, 34);
            lblOharrak.TabIndex = 1;
            lblOharrak.Text = "Oharrak";
            // 
            // txtOharrak
            // 
            txtOharrak.Location = new Point(24, 510);
            txtOharrak.Multiline = true;
            txtOharrak.Name = "txtOharrak";
            txtOharrak.ReadOnly = true;
            txtOharrak.ScrollBars = ScrollBars.Vertical;
            txtOharrak.Size = new Size(792, 132);
            txtOharrak.TabIndex = 2;
            // 
            // btnItxi
            // 
            btnItxi.BackColor = Color.FromArgb(44, 62, 80);
            btnItxi.FlatAppearance.BorderSize = 0;
            btnItxi.FlatStyle = FlatStyle.Flat;
            btnItxi.ForeColor = Color.White;
            btnItxi.Location = new Point(0, 692);
            btnItxi.Name = "btnItxi";
            btnItxi.Size = new Size(840, 48);
            btnItxi.TabIndex = 3;
            btnItxi.Text = "Itxi";
            btnItxi.UseVisualStyleBackColor = false;
            // 
            // JarraipenXehetasunakLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 740);
            Controls.Add(btnItxi);
            Controls.Add(txtOharrak);
            Controls.Add(lblOharrak);
            Controls.Add(tlpXehetasunak);
            Name = "JarraipenXehetasunakLaguntzailea";
            Text = "Jarraipen xehetasunak";
            ResumeLayout(false);
            PerformLayout();
        }

        private TableLayoutPanel tlpXehetasunak;
        private Label lblOharrak;
        private TextBox txtOharrak;
        private Button btnItxi;
    }
}