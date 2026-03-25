// ============================================================
// MedikuAzpiFormularioak.cs - Medikuarentzako azpi-formularioak
// ============================================================
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    // Pazienteak
    public class PazienteenFormularioa : OinarriFormularioa {
        public PazienteenFormularioa() : base() { }
        public PazienteenFormularioa(Erabiltzailea u) : base(u) { GehituAtzeraBotoia(); }
    }
    // Neurketak
    public class NeurketenFormularioa : OinarriFormularioa {
        public NeurketenFormularioa() : base() { }
        public NeurketenFormularioa(Erabiltzailea u) : base(u) { GehituAtzeraBotoia(); }
    }
    // Errezetak
    public class ErrezetenFormularioa : OinarriFormularioa {
        public ErrezetenFormularioa() : base() { }
        public ErrezetenFormularioa(Erabiltzailea u) : base(u) { GehituAtzeraBotoia(); }
    }
    // Grafikak
    public class GrafikenFormularioa : OinarriFormularioa {
        public GrafikenFormularioa() : base() { }
        public GrafikenFormularioa(Erabiltzailea u) : base(u) { GehituAtzeraBotoia(); }
    }
    // Abisuak
    public class AbisuenFormularioa : OinarriFormularioa {
        public AbisuenFormularioa() : base() { }
        public AbisuenFormularioa(Erabiltzailea u) : base(u) { GehituAtzeraBotoia(); }
    }
}
