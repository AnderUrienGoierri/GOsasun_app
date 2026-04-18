using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GOsasun_app.Interfazea.Kontrolak
{
    public sealed class PortadaPanela : Panel
    {
        private const int WmEraseBkgnd = 0x0014;
        private const int WsExComposited = 0x02000000;
        private Image? _atzekoPlanoaIrudia;
        private Color _atzekoKolorea = Color.FromArgb(214, 224, 229);

        public PortadaPanela()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WsExComposited;
                return createParams;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image? AtzekoPlanoaIrudia
        {
            get => _atzekoPlanoaIrudia;
            set
            {
                if (ReferenceEquals(_atzekoPlanoaIrudia, value))
                {
                    return;
                }

                _atzekoPlanoaIrudia = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color AtzekoKolorea
        {
            get => _atzekoKolorea;
            set
            {
                if (_atzekoKolorea == value)
                {
                    return;
                }

                _atzekoKolorea = value;
                BackColor = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(_atzekoKolorea);

            if (_atzekoPlanoaIrudia == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
            {
                return;
            }

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            float eskala = Math.Max(
                ClientSize.Width / (float)_atzekoPlanoaIrudia.Width,
                ClientSize.Height / (float)_atzekoPlanoaIrudia.Height);

            int zabalera = Math.Max(1, (int)Math.Round(_atzekoPlanoaIrudia.Width * eskala));
            int altuera = Math.Max(1, (int)Math.Round(_atzekoPlanoaIrudia.Height * eskala));
            int x = (ClientSize.Width - zabalera) / 2;
            int y = (ClientSize.Height - altuera) / 2;

            e.Graphics.DrawImage(_atzekoPlanoaIrudia, x, y, zabalera, altuera);
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate(true);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmEraseBkgnd)
            {
                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);
        }
    }
}