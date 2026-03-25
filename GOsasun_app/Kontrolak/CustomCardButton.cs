// ============================================================
// CustomCardButton.cs - Txartel pertsonalizatua (Card Button)
// ============================================================
// Panel kontroletik heredatzen duen kontrol pertsonalizatua.
// Borde biribilduak, irudia eta testua ditu, ukipen-egokia.
// ============================================================

using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GOsasun_app.Kontrolak
{
    /// <summary>
    /// Txartel pertsonalizatua: borde biribilduak, ikono handia eta testua.
    /// Tablet eta pertsona nagusientzako diseinatua.
    /// </summary>
    public class CustomCardButton : Panel
    {
        // -----------------------------------------------------------
        // Propietateak
        // -----------------------------------------------------------
        private Image? _ikonoa;
        private string _testua = "";
        private int _bordeBiribiltasuna = 24;
        private Color _kartaKolorea = Color.FromArgb(230, 255, 255, 255);
        private Color _hoverKolorea = Color.FromArgb(245, 255, 255, 255);
        private Color _testuKolorea = Color.FromArgb(50, 50, 50);
        private bool _hoverAktibo = false;
        private Font _testuFont = new Font("Segoe UI", 15f, FontStyle.Bold);

        /// <summary>
        /// Txartelaren barneko ikonoa (irudi handia goialdean).
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? Ikonoa
        {
            get => _ikonoa;
            set { _ikonoa = value; Invalidate(); }
        }

        /// <summary>
        /// Txartelaren azpiko testua (adib. "KONTAKTUA").
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Testua
        {
            get => _testua;
            set { _testua = value; Invalidate(); }
        }

        /// <summary>
        /// Borde biribilaren erradioa (pixeletan).
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BorderBiribiltasuna
        {
            get => _bordeBiribiltasuna;
            set { _bordeBiribiltasuna = value; Invalidate(); }
        }

        /// <summary>
        /// Txartelaren atzeko planoko kolorea.
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color KartaKolorea
        {
            get => _kartaKolorea;
            set { _kartaKolorea = value; Invalidate(); }
        }

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public CustomCardButton()
        {
            // Kontrol konfigurazioa
            this.Size = new Size(400, 320);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.Margin = new Padding(20);
            this.Padding = new Padding(10);

            // Estiloa

            // Gertaerak - Hover efektua
            this.MouseEnter += (s, e) => { _hoverAktibo = true; Invalidate(); };
            this.MouseLeave += (s, e) => { _hoverAktibo = false; Invalidate(); };
        }

        // -----------------------------------------------------------
        // Marrazketa pertsonalizatua (OnPaint)
        // -----------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. Borde biribiltuen bidea sortu
            Rectangle rect = new Rectangle(2, 2, this.Width - 5, this.Height - 5);
            GraphicsPath bidea = SortuBideBiribila(rect, _bordeBiribiltasuna);

            // 2. Itzala marraztu (hover efektuarekin)
            if (_hoverAktibo)
            {
                using (GraphicsPath itzalBidea = SortuBideBiribila(
                    new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height), _bordeBiribiltasuna))
                {
                    using (PathGradientBrush itzala = new PathGradientBrush(itzalBidea))
                    {
                        itzala.CenterColor = Color.FromArgb(60, 0, 0, 0);
                        itzala.SurroundColors = new[] { Color.Transparent };
                        g.FillPath(itzala, itzalBidea);
                    }
                }
            }

            // 3. Txartelaren atzeko planoa bete
            Color atzekoPlanoa = _hoverAktibo ? _hoverKolorea : _kartaKolorea;
            using (SolidBrush brotxa = new SolidBrush(atzekoPlanoa))
            {
                g.FillPath(brotxa, bidea);
            }

            // 4. Borde leuna marraztu
            using (Pen borrokaLuma = new Pen(Color.FromArgb(80, 200, 200, 200), 1.5f))
            {
                g.DrawPath(borrokaLuma, bidea);
            }

            // 5. Ikonoa marraztu (erdian, goialdean)
            if (_ikonoa != null)
            {
                int ikonoTamaina = Math.Min(this.Width - 80, 120);
                int ikonoX = (this.Width - ikonoTamaina) / 2;
                int ikonoY = 35;
                g.DrawImage(_ikonoa, new Rectangle(ikonoX, ikonoY, ikonoTamaina, ikonoTamaina));
            }

            // 6. Testua marraztu (erdian, behean)
            if (!string.IsNullOrEmpty(_testua))
            {
                StringFormat formatua = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisWord
                };

                int testuY = _ikonoa != null ? this.Height - 100 : (this.Height - 60) / 2;
                Rectangle testuRect = new Rectangle(15, testuY, this.Width - 30, 85);

                using (SolidBrush testuBrotxa = new SolidBrush(_testuKolorea))
                {
                    g.DrawString(_testua, _testuFont, testuBrotxa, testuRect, formatua);
                }
            }

            bidea.Dispose();
        }

        // -----------------------------------------------------------
        // Borde biribilak sortzeko laguntza-metodoa
        // -----------------------------------------------------------
        private GraphicsPath SortuBideBiribila(Rectangle ertz, int erradioa)
        {
            GraphicsPath bidea = new GraphicsPath();
            int d = erradioa * 2;

            bidea.AddArc(ertz.X, ertz.Y, d, d, 180, 90);                             // Goiko-ezker
            bidea.AddArc(ertz.Right - d, ertz.Y, d, d, 270, 90);                      // Goiko-eskuin
            bidea.AddArc(ertz.Right - d, ertz.Bottom - d, d, d, 0, 90);               // Beheko-eskuin
            bidea.AddArc(ertz.X, ertz.Bottom - d, d, d, 90, 90);                      // Beheko-ezker
            bidea.CloseFigure();

            return bidea;
        }

        // -----------------------------------------------------------
        // Klik-a zabaltzen diren gertaerak (seme-kontrol guztietatik)
        // -----------------------------------------------------------
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control != null)
            {
                e.Control.Click += (s, ev) => this.OnClick(ev);
                e.Control.MouseEnter += (s, ev) => { _hoverAktibo = true; Invalidate(); };
                e.Control.MouseLeave += (s, ev) => { _hoverAktibo = false; Invalidate(); };
            }
        }

        // -----------------------------------------------------------
        // Eremuaren forma biribildua bermatu
        // -----------------------------------------------------------
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            this.Region = new Region(SortuBideBiribila(rect, _bordeBiribiltasuna));
        }
    }
}
