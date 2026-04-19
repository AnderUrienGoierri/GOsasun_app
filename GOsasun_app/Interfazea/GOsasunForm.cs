using System.ComponentModel;
using System.Reflection;

namespace GOsasun_app.Interfazea
{
    public class GOsasunForm : Form
    {
        public event EventHandler? HasierakoAurkezpenaOsatuta;

        private sealed class KontrolDiseinuDatuak
        {
            public required Rectangle Bounds { get; init; }
            public required DockStyle Dock { get; init; }
            public required Padding Margin { get; init; }
            public required Padding Padding { get; init; }
            public required Font Font { get; init; }
            public int? BorderBiribiltasuna { get; init; }
            public int? DataGridViewErrenkadaAltuera { get; init; }
            public int? DataGridViewGoiburuAltuera { get; init; }
        }

        private static Icon? _ikonoNagusia;
        private static Rectangle? _leihoBoundsPartekatua;
        private readonly Dictionary<Control, KontrolDiseinuDatuak> _kontrolDiseinuak = new();
        private Size _diseinuClientSize;
        private bool _hasierakoEskalatzeaAplikatuta;
        private bool _pantailaOsoaAplikatuta;
        private bool _hasierakoMarrazketaEzkututa;

        protected float UnekoDiseinuEskala { get; private set; } = 1f;
        protected virtual bool EskalatzeProportzionalaGaitu => true;
        protected virtual bool PantailaHandiagoetanHanditu => false;
        protected virtual bool HasierakoMarrazketaLeundu => true;
        protected virtual bool PantailaOsoanIreki => FormBorderStyle != FormBorderStyle.FixedDialog
            && FormBorderStyle != FormBorderStyle.FixedToolWindow
            && FormBorderStyle != FormBorderStyle.SizableToolWindow;

        protected GOsasunForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            PrestatuHasierakoMarrazketa();
            base.OnLoad(e);

            if (!DiseinuModuan())
            {
                SuspendLayout();

                if (!_hasierakoEskalatzeaAplikatuta)
                {
                    AplikatuHasierakoEskalatzea();
                }

                AktibatuBufferBikoitza(this);
                ResumeLayout(true);
                PerformLayout();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            EzarriAplikazioIkonoa();
            AktibatuBufferBikoitza(this);
        }

        protected override void OnShown(EventArgs e)
        {
            EzarriAplikazioIkonoa();
            base.OnShown(e);

            if (!DiseinuModuan())
            {
                BeginInvoke(new Action(() =>
                {
                    AplikatuLeihoTamainaPartekatua();
                    GordeLeihoTamainaPartekatua();
                    AmaituHasierakoMarrazketa();
                }));
            }
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            if (!DiseinuModuan())
            {
                GordeLeihoTamainaPartekatua();
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (!DiseinuModuan() && Visible)
            {
                GordeLeihoTamainaPartekatua();
            }
        }

        private void PrestatuHasierakoMarrazketa()
        {
            if (DiseinuModuan() || !HasierakoMarrazketaLeundu || _hasierakoMarrazketaEzkututa)
            {
                return;
            }

            Opacity = 0d;
            _hasierakoMarrazketaEzkututa = true;
        }

        private void AmaituHasierakoMarrazketa()
        {
            if (!_hasierakoMarrazketaEzkututa)
            {
                return;
            }

            Opacity = 1d;
            _hasierakoMarrazketaEzkututa = false;
            Invalidate(true);
            Update();
            HasierakoAurkezpenaOsatuta?.Invoke(this, EventArgs.Empty);
        }

        private void AplikatuHasierakoEskalatzea()
        {
            if (_hasierakoEskalatzeaAplikatuta || DiseinuModuan() || !EskalatzeProportzionalaGaitu)
            {
                return;
            }

            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
            {
                return;
            }

            _hasierakoEskalatzeaAplikatuta = true;
            _diseinuClientSize = ClientSize;
            _kontrolDiseinuak.Clear();
            GordeDiseinuEgoera(this);

            float eskala = KalkulatuPantailaEskala();
            if (!PantailaHandiagoetanHanditu)
            {
                eskala = Math.Min(1f, eskala);
            }

            if (eskala <= 0f)
            {
                eskala = 1f;
            }

            UnekoDiseinuEskala = eskala;

            if (BackgroundImage != null && BackgroundImageLayout == ImageLayout.None)
            {
                BackgroundImageLayout = ImageLayout.Zoom;
            }

            if (Math.Abs(eskala - 1f) < 0.01f)
            {
                return;
            }

            SuspendLayout();
            ClientSize = EskalatuNeurria(_diseinuClientSize, eskala);
            EskalatuKontrolak(this, eskala);
            ResumeLayout(true);
            PerformLayout();
            Invalidate(true);
        }

        protected Rectangle LortuPantailarenLanEremua()
        {
            return Owner != null
                ? Screen.FromControl(Owner).WorkingArea
                : Screen.FromControl(this).WorkingArea;
        }

        protected void EzarriPantailaOsora()
        {
            if (DiseinuModuan() || !PantailaOsoanIreki)
            {
                return;
            }

            Rectangle lanEremua = LortuPantailarenLanEremua();
            _pantailaOsoaAplikatuta = true;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            Bounds = lanEremua;
        }

        internal void BehartuPantailaOsoraBerehala()
        {
            EzarriPantailaOsora();
        }

        protected void GordeLeihoTamainaPartekatua()
        {
            if (DiseinuModuan() || WindowState == FormWindowState.Minimized || Width <= 0 || Height <= 0)
            {
                return;
            }

            Rectangle lanEremua = LortuPantailarenLanEremua();
            Rectangle oraingoBounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;

            if (oraingoBounds.Width <= 0 || oraingoBounds.Height <= 0)
            {
                return;
            }

            _leihoBoundsPartekatua = DoituLeihoBounds(oraingoBounds, lanEremua);
            _pantailaOsoaAplikatuta = false;
        }

        internal void AplikatuLeihoTamainaPartekatuaBerehala()
        {
            AplikatuLeihoTamainaPartekatua();
        }

        protected void ZentratuPantailaLanEremuan()
        {
            if (DiseinuModuan() || _pantailaOsoaAplikatuta)
            {
                return;
            }

            Rectangle lanEremua = LortuPantailarenLanEremua();
            int x = lanEremua.Left + Math.Max(0, (lanEremua.Width - Width) / 2);
            int y = lanEremua.Top + Math.Max(0, (lanEremua.Height - Height) / 2);
            Location = new Point(x, y);
        }

        private void AplikatuLeihoTamainaPartekatua()
        {
            if (DiseinuModuan() || !_leihoBoundsPartekatua.HasValue)
            {
                return;
            }

            Rectangle lanEremua = LortuPantailarenLanEremua();
            Rectangle helmugaBounds = DoituLeihoBounds(_leihoBoundsPartekatua.Value, lanEremua);

            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            Bounds = helmugaBounds;
            _pantailaOsoaAplikatuta = false;
        }

        private static Rectangle DoituLeihoBounds(Rectangle jatorrizkoa, Rectangle lanEremua)
        {
            int zabalera = Math.Max(320, Math.Min(jatorrizkoa.Width, lanEremua.Width));
            int altuera = Math.Max(240, Math.Min(jatorrizkoa.Height, lanEremua.Height));
            int x = Math.Min(Math.Max(jatorrizkoa.X, lanEremua.Left), lanEremua.Right - zabalera);
            int y = Math.Min(Math.Max(jatorrizkoa.Y, lanEremua.Top), lanEremua.Bottom - altuera);
            return new Rectangle(x, y, zabalera, altuera);
        }

        protected int LortuPantailaraEgokitutakoZabalera(int nahitutakoZabalera, int marjina = 60)
        {
            Rectangle lanEremua = LortuPantailarenLanEremua();
            int leihoMarrakZabalera = Math.Max(0, Width - ClientSize.Width);
            int gehienezkoZabalera = Math.Max(320, lanEremua.Width - leihoMarrakZabalera - marjina);
            return Math.Min(nahitutakoZabalera, gehienezkoZabalera);
        }

        protected int LortuPantailaraEgokitutakoAltuera(int nahitutakoAltuera, int marjina = 60)
        {
            Rectangle lanEremua = LortuPantailarenLanEremua();
            int leihoMarrakAltuera = Math.Max(0, Height - ClientSize.Height);
            int gehienezkoAltuera = Math.Max(240, lanEremua.Height - leihoMarrakAltuera - marjina);
            return Math.Min(nahitutakoAltuera, gehienezkoAltuera);
        }

        private float KalkulatuPantailaEskala()
        {
            Rectangle lanEremua = LortuPantailarenLanEremua();

            int leihoMarrakZabalera = Width - ClientSize.Width;
            int leihoMarrakAltuera = Height - ClientSize.Height;

            int bezeroMaxZabalera = Math.Max(1, lanEremua.Width - leihoMarrakZabalera);
            int bezeroMaxAltuera = Math.Max(1, lanEremua.Height - leihoMarrakAltuera);

            float eskalaX = bezeroMaxZabalera / (float)_diseinuClientSize.Width;
            float eskalaY = bezeroMaxAltuera / (float)_diseinuClientSize.Height;
            return Math.Min(eskalaX, eskalaY);
        }

        private void GordeDiseinuEgoera(Control gurasoa)
        {
            foreach (Control kontrola in gurasoa.Controls)
            {
                _kontrolDiseinuak[kontrola] = new KontrolDiseinuDatuak
                {
                    Bounds = kontrola.Bounds,
                    Dock = kontrola.Dock,
                    Margin = kontrola.Margin,
                    Padding = kontrola.Padding,
                    Font = (Font)kontrola.Font.Clone(),
                    BorderBiribiltasuna = LortuIntPropietatea(kontrola, "BorderBiribiltasuna"),
                    DataGridViewErrenkadaAltuera = kontrola is DataGridView dgv ? dgv.RowTemplate.Height : null,
                    DataGridViewGoiburuAltuera = kontrola is DataGridView dgv2 ? dgv2.ColumnHeadersHeight : null
                };

                if (kontrola.HasChildren)
                {
                    GordeDiseinuEgoera(kontrola);
                }
            }
        }

        private void EskalatuKontrolak(Control gurasoa, float eskala)
        {
            foreach (Control kontrola in gurasoa.Controls)
            {
                if (!_kontrolDiseinuak.TryGetValue(kontrola, out KontrolDiseinuDatuak? diseinua))
                {
                    continue;
                }

                kontrola.Margin = EskalatuPadding(diseinua.Margin, eskala);
                kontrola.Padding = EskalatuPadding(diseinua.Padding, eskala);
                kontrola.Font = EskalatuFont(diseinua.Font, eskala);

                switch (diseinua.Dock)
                {
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        kontrola.Height = EskalatuBalioa(diseinua.Bounds.Height, eskala);
                        break;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        kontrola.Width = EskalatuBalioa(diseinua.Bounds.Width, eskala);
                        break;
                    case DockStyle.Fill:
                        break;
                    default:
                        kontrola.Bounds = EskalatuLaukizuzena(diseinua.Bounds, eskala);
                        break;
                }

                if (diseinua.BorderBiribiltasuna.HasValue)
                {
                    EzarriIntPropietatea(
                        kontrola,
                        "BorderBiribiltasuna",
                        EskalatuBalioa(diseinua.BorderBiribiltasuna.Value, eskala));
                }

                if (kontrola is DataGridView dgv)
                {
                    if (diseinua.DataGridViewErrenkadaAltuera.HasValue)
                    {
                        dgv.RowTemplate.Height = EskalatuBalioa(diseinua.DataGridViewErrenkadaAltuera.Value, eskala);
                    }

                    if (diseinua.DataGridViewGoiburuAltuera.HasValue)
                    {
                        dgv.ColumnHeadersHeight = EskalatuBalioa(diseinua.DataGridViewGoiburuAltuera.Value, eskala);
                    }
                }

                if (kontrola.HasChildren)
                {
                    EskalatuKontrolak(kontrola, eskala);
                }
            }
        }

        private static Rectangle EskalatuLaukizuzena(Rectangle jatorrizkoa, float eskala)
        {
            return new Rectangle(
                EskalatuBalioa(jatorrizkoa.X, eskala),
                EskalatuBalioa(jatorrizkoa.Y, eskala),
                EskalatuBalioa(jatorrizkoa.Width, eskala),
                EskalatuBalioa(jatorrizkoa.Height, eskala));
        }

        private static Size EskalatuNeurria(Size jatorrizkoa, float eskala)
        {
            return new Size(
                EskalatuBalioa(jatorrizkoa.Width, eskala),
                EskalatuBalioa(jatorrizkoa.Height, eskala));
        }

        private static Padding EskalatuPadding(Padding jatorrizkoa, float eskala)
        {
            return new Padding(
                EskalatuBalioa(jatorrizkoa.Left, eskala),
                EskalatuBalioa(jatorrizkoa.Top, eskala),
                EskalatuBalioa(jatorrizkoa.Right, eskala),
                EskalatuBalioa(jatorrizkoa.Bottom, eskala));
        }

        private static Font EskalatuFont(Font jatorrizkoa, float eskala)
        {
            float tamaina = Math.Max(1f, jatorrizkoa.Size * eskala);
            return new Font(
                jatorrizkoa.FontFamily,
                tamaina,
                jatorrizkoa.Style,
                jatorrizkoa.Unit,
                jatorrizkoa.GdiCharSet,
                jatorrizkoa.GdiVerticalFont);
        }

        private static int EskalatuBalioa(int balioa, float eskala)
        {
            return Math.Max(1, (int)Math.Round(balioa * eskala));
        }

        private static int? LortuIntPropietatea(object objektua, string izena)
        {
            PropertyInfo? propietatea = objektua.GetType().GetProperty(izena, BindingFlags.Public | BindingFlags.Instance);
            if (propietatea == null || propietatea.PropertyType != typeof(int) || !propietatea.CanRead)
            {
                return null;
            }

            return (int?)propietatea.GetValue(objektua);
        }

        private static void EzarriIntPropietatea(object objektua, string izena, int balioa)
        {
            PropertyInfo? propietatea = objektua.GetType().GetProperty(izena, BindingFlags.Public | BindingFlags.Instance);
            if (propietatea == null || propietatea.PropertyType != typeof(int) || !propietatea.CanWrite)
            {
                return;
            }

            propietatea.SetValue(objektua, balioa);
        }

        private static void AktibatuBufferBikoitza(Control kontrola)
        {
            SaiatuDoubleBufferedEzarri(kontrola);

            foreach (Control umea in kontrola.Controls)
            {
                AktibatuBufferBikoitza(umea);
            }
        }

        private static void SaiatuDoubleBufferedEzarri(Control kontrola)
        {
            try
            {
                PropertyInfo? propietatea = kontrola.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propietatea?.CanWrite == true)
                {
                    propietatea.SetValue(kontrola, true);
                }
            }
            catch
            {
                // Kontrol batzuek ez dute propietatea modu seguruan eskaintzen.
            }
        }

        private void EzarriAplikazioIkonoa()
        {
            if (DiseinuModuan())
            {
                return;
            }

            Icon? ikonoa = LortuAplikazioIkonoa();
            if (ikonoa == null)
            {
                return;
            }

            Icon?.Dispose();
            Icon = ikonoa;
            ShowIcon = true;
        }

        private static Icon? LortuAplikazioIkonoa()
        {
            if (_ikonoNagusia != null)
            {
                return (Icon)_ikonoNagusia.Clone();
            }

            Icon? exeIkonoa = null;
            try
            {
                exeIkonoa = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                if (exeIkonoa != null)
                {
                    _ikonoNagusia = (Icon)exeIkonoa.Clone();
                    return (Icon)_ikonoNagusia.Clone();
                }
            }
            finally
            {
                exeIkonoa?.Dispose();
            }

            return null;
        }

        protected bool DiseinuModuan()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode || Site?.DesignMode == true;
        }
    }
}