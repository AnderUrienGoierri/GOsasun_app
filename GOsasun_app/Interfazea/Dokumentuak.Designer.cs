using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class Dokumentuak
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
            panelEdukia = new Panel();
            panelTxartela = new Panel();
            _osasunTxostenaSortuBotoia = new Button();
            _dokumentuBerriaBotoia = new Button();
            _dokumentuakGrid = new DataGridView();
            _egoeraLabel = new Label();
            iragazkiPanela = new FlowLayoutPanel();
            _bilaketaLabel = new Label();
            _bilaketaTextBox = new TextBox();
            _hasieraDataPicker = new DateTimePicker();
            _amaieraDataPicker = new DateTimePicker();
            _bilatuBotoia = new Button();
            _garbituBotoia = new Button();
            _jarraipenGuztiakCheckBox = new CheckBox();
            _azalpenaLabel = new Label();
            _izenburuaLabel = new Label();
            _edukiPanela.SuspendLayout();
            panelEdukia.SuspendLayout();
            panelTxartela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dokumentuakGrid).BeginInit();
            iragazkiPanela.SuspendLayout();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(panelEdukia);
            //
            // _atzeraBotoia
            //
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            //
            // panelEdukia
            //
            panelEdukia.BackColor = Color.Transparent;
            panelEdukia.Controls.Add(panelTxartela);
            panelEdukia.Dock = DockStyle.Fill;
            panelEdukia.Location = new Point(2, 2);
            panelEdukia.Name = "panelEdukia";
            panelEdukia.Padding = new Padding(36);
            panelEdukia.Size = new Size(1898, 1390);
            panelEdukia.TabIndex = 0;
            //
            // panelTxartela
            //
            panelTxartela.BackColor = Color.FromArgb(230, 255, 255, 255);
            panelTxartela.Controls.Add(_osasunTxostenaSortuBotoia);
            panelTxartela.Controls.Add(_dokumentuBerriaBotoia);
            panelTxartela.Controls.Add(_dokumentuakGrid);
            panelTxartela.Controls.Add(_egoeraLabel);
            panelTxartela.Controls.Add(iragazkiPanela);
            panelTxartela.Controls.Add(_jarraipenGuztiakCheckBox);
            panelTxartela.Controls.Add(_azalpenaLabel);
            panelTxartela.Controls.Add(_izenburuaLabel);
            panelTxartela.Dock = DockStyle.Fill;
            panelTxartela.Location = new Point(36, 36);
            panelTxartela.Name = "panelTxartela";
            panelTxartela.Padding = new Padding(32);
            panelTxartela.Size = new Size(1826, 1318);
            panelTxartela.TabIndex = 0;
            // 
            // _osasunTxostenaSortuBotoia
            // 
            _osasunTxostenaSortuBotoia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _osasunTxostenaSortuBotoia.BackColor = Color.FromArgb(41, 128, 185);
            _osasunTxostenaSortuBotoia.Cursor = Cursors.Hand;
            _osasunTxostenaSortuBotoia.FlatAppearance.BorderSize = 0;
            _osasunTxostenaSortuBotoia.FlatStyle = FlatStyle.Flat;
            _osasunTxostenaSortuBotoia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _osasunTxostenaSortuBotoia.ForeColor = Color.White;
            _osasunTxostenaSortuBotoia.ImageAlign = ContentAlignment.MiddleLeft;
            _osasunTxostenaSortuBotoia.Location = new Point(1430, 104);
            _osasunTxostenaSortuBotoia.Margin = new Padding(0);
            _osasunTxostenaSortuBotoia.Name = "_osasunTxostenaSortuBotoia";
            _osasunTxostenaSortuBotoia.Padding = new Padding(14, 6, 16, 6);
            _osasunTxostenaSortuBotoia.Size = new Size(364, 61);
            _osasunTxostenaSortuBotoia.TabIndex = 5;
            _osasunTxostenaSortuBotoia.Text = "Osasun Txostena Sortu";
            _osasunTxostenaSortuBotoia.TextImageRelation = TextImageRelation.ImageBeforeText;
            _osasunTxostenaSortuBotoia.UseVisualStyleBackColor = false;
            // 
            // _dokumentuBerriaBotoia
            // 
            _dokumentuBerriaBotoia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _dokumentuBerriaBotoia.BackColor = Color.FromArgb(83, 148, 117);
            _dokumentuBerriaBotoia.Cursor = Cursors.Hand;
            _dokumentuBerriaBotoia.FlatAppearance.BorderSize = 0;
            _dokumentuBerriaBotoia.FlatStyle = FlatStyle.Flat;
            _dokumentuBerriaBotoia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _dokumentuBerriaBotoia.ForeColor = Color.White;
            _dokumentuBerriaBotoia.ImageAlign = ContentAlignment.MiddleLeft;
            _dokumentuBerriaBotoia.Location = new Point(1430, 179);
            _dokumentuBerriaBotoia.Margin = new Padding(0);
            _dokumentuBerriaBotoia.Name = "_dokumentuBerriaBotoia";
            _dokumentuBerriaBotoia.Padding = new Padding(14, 6, 16, 6);
            _dokumentuBerriaBotoia.Size = new Size(364, 61);
            _dokumentuBerriaBotoia.TabIndex = 6;
            _dokumentuBerriaBotoia.Text = "Dokumentu berria";
            _dokumentuBerriaBotoia.TextImageRelation = TextImageRelation.ImageBeforeText;
            _dokumentuBerriaBotoia.UseVisualStyleBackColor = false;
            // 
            // _dokumentuakGrid
            // 
            _dokumentuakGrid.AllowUserToAddRows = false;
            _dokumentuakGrid.AllowUserToDeleteRows = false;
            _dokumentuakGrid.AllowUserToResizeRows = false;
            _dokumentuakGrid.BackgroundColor = Color.White;
            _dokumentuakGrid.BorderStyle = BorderStyle.None;
            _dokumentuakGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dokumentuakGrid.Dock = DockStyle.Fill;
            _dokumentuakGrid.GridColor = Color.FromArgb(220, 220, 220);
            _dokumentuakGrid.Location = new Point(32, 397);
            _dokumentuakGrid.MultiSelect = false;
            _dokumentuakGrid.Name = "_dokumentuakGrid";
            _dokumentuakGrid.ReadOnly = true;
            _dokumentuakGrid.RowHeadersVisible = false;
            _dokumentuakGrid.RowHeadersWidth = 82;
            _dokumentuakGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dokumentuakGrid.Size = new Size(1762, 889);
            _dokumentuakGrid.TabIndex = 4;
            // 
            // _egoeraLabel
            // 
            _egoeraLabel.Dock = DockStyle.Top;
            _egoeraLabel.Font = new Font("Segoe UI", 10F);
            _egoeraLabel.ForeColor = Color.FromArgb(90, 90, 90);
            _egoeraLabel.Location = new Point(32, 324);
            _egoeraLabel.Name = "_egoeraLabel";
            _egoeraLabel.Padding = new Padding(0, 4, 280, 10);
            _egoeraLabel.Size = new Size(1762, 73);
            _egoeraLabel.TabIndex = 3;
            _egoeraLabel.Click += _egoeraLabel_Click;
            // 
            // iragazkiPanela
            // 
            iragazkiPanela.BackColor = Color.Transparent;
            iragazkiPanela.Controls.Add(_bilaketaLabel);
            iragazkiPanela.Controls.Add(_bilaketaTextBox);
            iragazkiPanela.Controls.Add(_hasieraDataPicker);
            iragazkiPanela.Controls.Add(_amaieraDataPicker);
            iragazkiPanela.Controls.Add(_bilatuBotoia);
            iragazkiPanela.Controls.Add(_garbituBotoia);
            iragazkiPanela.Location = new Point(2, 254);
            iragazkiPanela.Margin = new Padding(0);
            iragazkiPanela.Name = "iragazkiPanela";
            iragazkiPanela.Padding = new Padding(0, 8, 0, 8);
            iragazkiPanela.Size = new Size(1824, 112);
            iragazkiPanela.TabIndex = 2;
            iragazkiPanela.WrapContents = false;
            // 
            // _bilaketaLabel
            // 
            _bilaketaLabel.AutoSize = true;
            _bilaketaLabel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            _bilaketaLabel.ForeColor = Color.FromArgb(50, 50, 50);
            _bilaketaLabel.Location = new Point(0, 17);
            _bilaketaLabel.Margin = new Padding(0, 9, 12, 0);
            _bilaketaLabel.Name = "_bilaketaLabel";
            _bilaketaLabel.Size = new Size(131, 38);
            _bilaketaLabel.TabIndex = 0;
            _bilaketaLabel.Text = "Bilaketa:";
            // 
            // _bilaketaTextBox
            // 
            _bilaketaTextBox.Font = new Font("Segoe UI", 10.5F);
            _bilaketaTextBox.Location = new Point(143, 12);
            _bilaketaTextBox.Margin = new Padding(0, 4, 12, 0);
            _bilaketaTextBox.Name = "_bilaketaTextBox";
            _bilaketaTextBox.Size = new Size(420, 45);
            _bilaketaTextBox.TabIndex = 1;
            // 
            // _hasieraDataPicker
            // 
            _hasieraDataPicker.Checked = false;
            _hasieraDataPicker.CustomFormat = "'Hasiera data: 'dd/MM/yyyy";
            _hasieraDataPicker.Font = new Font("Segoe UI", 10F);
            _hasieraDataPicker.Format = DateTimePickerFormat.Custom;
            _hasieraDataPicker.Location = new Point(575, 10);
            _hasieraDataPicker.Margin = new Padding(0, 2, 12, 0);
            _hasieraDataPicker.Name = "_hasieraDataPicker";
            _hasieraDataPicker.ShowCheckBox = true;
            _hasieraDataPicker.Size = new Size(414, 43);
            _hasieraDataPicker.TabIndex = 2;
            // 
            // _amaieraDataPicker
            // 
            _amaieraDataPicker.Checked = false;
            _amaieraDataPicker.CustomFormat = "'Amaiera data: 'dd/MM/yyyy";
            _amaieraDataPicker.Font = new Font("Segoe UI", 10F);
            _amaieraDataPicker.Format = DateTimePickerFormat.Custom;
            _amaieraDataPicker.Location = new Point(1001, 10);
            _amaieraDataPicker.Margin = new Padding(0, 2, 12, 0);
            _amaieraDataPicker.Name = "_amaieraDataPicker";
            _amaieraDataPicker.ShowCheckBox = true;
            _amaieraDataPicker.Size = new Size(430, 43);
            _amaieraDataPicker.TabIndex = 3;
            // 
            // _bilatuBotoia
            // 
            _bilatuBotoia.AutoSize = true;
            _bilatuBotoia.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _bilatuBotoia.BackColor = Color.FromArgb(44, 62, 80);
            _bilatuBotoia.Cursor = Cursors.Hand;
            _bilatuBotoia.FlatAppearance.BorderSize = 0;
            _bilatuBotoia.FlatStyle = FlatStyle.Flat;
            _bilatuBotoia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _bilatuBotoia.ForeColor = Color.White;
            _bilatuBotoia.Location = new Point(1443, 8);
            _bilatuBotoia.Margin = new Padding(0, 0, 12, 0);
            _bilatuBotoia.Name = "_bilatuBotoia";
            _bilatuBotoia.Padding = new Padding(16, 6, 16, 6);
            _bilatuBotoia.Size = new Size(134, 59);
            _bilatuBotoia.TabIndex = 4;
            _bilatuBotoia.Text = "Bilatu";
            _bilatuBotoia.UseVisualStyleBackColor = false;
            //
            // _garbituBotoia
            //
            _garbituBotoia.AutoSize = true;
            _garbituBotoia.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _garbituBotoia.BackColor = Color.FromArgb(44, 62, 80);
            _garbituBotoia.Cursor = Cursors.Hand;
            _garbituBotoia.FlatAppearance.BorderSize = 0;
            _garbituBotoia.FlatStyle = FlatStyle.Flat;
            _garbituBotoia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _garbituBotoia.ForeColor = Color.White;
            _garbituBotoia.Location = new Point(1589, 8);
            _garbituBotoia.Margin = new Padding(0);
            _garbituBotoia.Name = "_garbituBotoia";
            _garbituBotoia.Padding = new Padding(16, 6, 16, 6);
            _garbituBotoia.Size = new Size(156, 59);
            _garbituBotoia.TabIndex = 6;
            _garbituBotoia.Text = "Garbitu";
            _garbituBotoia.UseVisualStyleBackColor = false;
            //
            // _jarraipenGuztiakCheckBox
            //
            _jarraipenGuztiakCheckBox.AutoSize = true;
            _jarraipenGuztiakCheckBox.BackColor = Color.Transparent;
            _jarraipenGuztiakCheckBox.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            _jarraipenGuztiakCheckBox.ForeColor = Color.FromArgb(50, 50, 50);
            _jarraipenGuztiakCheckBox.Location = new Point(32, 214);
            _jarraipenGuztiakCheckBox.Margin = new Padding(0, 0, 0, 8);
            _jarraipenGuztiakCheckBox.Name = "_jarraipenGuztiakCheckBox";
            _jarraipenGuztiakCheckBox.Size = new Size(424, 42);
            _jarraipenGuztiakCheckBox.TabIndex = 7;
            _jarraipenGuztiakCheckBox.Text = "Dokumentu guztiak erakutsi";
            _jarraipenGuztiakCheckBox.UseVisualStyleBackColor = true;
            // 
            // _azalpenaLabel
            // 
            _azalpenaLabel.Dock = DockStyle.Top;
            _azalpenaLabel.Font = new Font("Segoe UI", 10.5F);
            _azalpenaLabel.ForeColor = Color.FromArgb(90, 90, 90);
            _azalpenaLabel.Location = new Point(32, 128);
            _azalpenaLabel.Name = "_azalpenaLabel";
            _azalpenaLabel.Padding = new Padding(0, 0, 0, 8);
            _azalpenaLabel.Size = new Size(1762, 196);
            _azalpenaLabel.TabIndex = 1;
            // 
            // _izenburuaLabel
            // 
            _izenburuaLabel.Dock = DockStyle.Top;
            _izenburuaLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            _izenburuaLabel.ForeColor = Color.FromArgb(44, 62, 80);
            _izenburuaLabel.Location = new Point(32, 32);
            _izenburuaLabel.Name = "_izenburuaLabel";
            _izenburuaLabel.Size = new Size(1762, 96);
            _izenburuaLabel.TabIndex = 0;
            _izenburuaLabel.Text = "Dokumentuak";
            // 
            // Dokumentuak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1575);
            Name = "Dokumentuak";
            Text = "GOsasun - Dokumentuak";
            _edukiPanela.ResumeLayout(false);
            panelEdukia.ResumeLayout(false);
            panelTxartela.ResumeLayout(false);
            panelTxartela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dokumentuakGrid).EndInit();
            iragazkiPanela.ResumeLayout(false);
            iragazkiPanela.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelEdukia;
        private Panel panelTxartela;
        private Label _izenburuaLabel;
        private Label _azalpenaLabel;
        private FlowLayoutPanel iragazkiPanela;
        private Label _bilaketaLabel;
        private TextBox _bilaketaTextBox;
        private DateTimePicker _hasieraDataPicker;
        private DateTimePicker _amaieraDataPicker;
        private Button _bilatuBotoia;
        private Button _garbituBotoia;
        private CheckBox _jarraipenGuztiakCheckBox;
        private Button _osasunTxostenaSortuBotoia;
        private Button _dokumentuBerriaBotoia;
        private Label _egoeraLabel;
        private DataGridView _dokumentuakGrid;
    }
}
