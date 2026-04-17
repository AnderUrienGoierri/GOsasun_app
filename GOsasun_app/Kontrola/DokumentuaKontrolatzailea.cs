using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class DokumentuaKontrolatzailea
    {
        private readonly DokumentuaDB _dokumentuaDb = new DokumentuaDB();
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly DokumentuPdfZerbitzua _pdfZerbitzua = new DokumentuPdfZerbitzua();

        public List<Dokumentua> LortuDokumentuak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null, int? pazienteId = null)
        {
            return _dokumentuaDb.LortuDokumentuGuztiak(bilaketa, hasieraData, amaieraData, pazienteId);
        }

        public List<Dokumentua> LortuJarraipenarenDokumentuak(int jarraipenaId)
        {
            return _dokumentuaDb.LortuJarraipenarenDokumentuak(jarraipenaId);
        }

        public List<Dokumentua> LortuPazientearenBesteDokumentuak(int pazienteId, int? baztertuJarraipenaId = null, string? bilaketa = null)
        {
            List<Dokumentua> dokumentuak = _dokumentuaDb.LortuPazientearenBesteDokumentuak(pazienteId, baztertuJarraipenaId, bilaketa);

            if (!baztertuJarraipenaId.HasValue)
            {
                return dokumentuak;
            }

            List<Dokumentua> jarraipenekoDokumentuak = _dokumentuaDb.LortuJarraipenarenDokumentuak(baztertuJarraipenaId.Value);
            if (jarraipenekoDokumentuak.Count == 0)
            {
                return dokumentuak;
            }

            HashSet<string> dokumentuSinadurak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Dokumentua dokumentua in jarraipenekoDokumentuak)
            {
                GehituDokumentuSinadurak(dokumentuSinadurak, dokumentua);
            }

            return dokumentuak
                .Where(dokumentua => !DokumentuaJadaLotutaDago(dokumentua, dokumentuSinadurak))
                .ToList();
        }

        private static bool DokumentuaJadaLotutaDago(Dokumentua dokumentua, HashSet<string> dokumentuSinadurak)
        {
            return LortuDokumentuSinadurak(dokumentua).Any(dokumentuSinadurak.Contains);
        }

        private static void GehituDokumentuSinadurak(HashSet<string> dokumentuSinadurak, Dokumentua dokumentua)
        {
            foreach (string sinadura in LortuDokumentuSinadurak(dokumentua))
            {
                dokumentuSinadurak.Add(sinadura);
            }
        }

        private static IEnumerable<string> LortuDokumentuSinadurak(Dokumentua dokumentua)
        {
            if (!string.IsNullOrWhiteSpace(dokumentua.BideaZerbitzarian))
            {
                yield return $"path:{dokumentua.BideaZerbitzarian.Trim()}";
            }

            if (!string.IsNullOrWhiteSpace(dokumentua.FitxategiIzena))
            {
                yield return $"file:{dokumentua.FitxategiIzena.Trim()}";
            }
        }

        public Dokumentua? LortuDokumentua(int dokumentuId)
        {
            return _dokumentuaDb.LortuDokumentua(dokumentuId);
        }

        public bool GordeDokumentua(Dokumentua dokumentua)
        {
            return _dokumentuaDb.GordeDokumentua(dokumentua);
        }

        public bool EguneratuDokumentua(Dokumentua dokumentua)
        {
            return _dokumentuaDb.EguneratuDokumentua(dokumentua);
        }

        public bool EzabatuDokumentua(int dokumentuId)
        {
            return _dokumentuaDb.EzabatuDokumentua(dokumentuId);
        }

        public bool BerrlotuDokumentuaJarraipenera(int dokumentuId, int jarraipenaId)
        {
            return _dokumentuaDb.AldatuDokumentuarenJarraipena(dokumentuId, jarraipenaId);
        }

        public int? ZiurtatuJarraipena(int pazienteId, int? jarraipenaId, int? osasunLangileId, string? oharrak = null)
        {
            if (jarraipenaId.HasValue) return jarraipenaId.Value;

            Jarraipena jarraipena = new Jarraipena
            {
                PazienteId = pazienteId,
                OsasunLangileId = osasunLangileId,
                Oharrak = string.IsNullOrWhiteSpace(oharrak) ? "Dokumentuari lotutako jarraipen automatikoa" : oharrak,
                ErregistroData = DateTime.Now
            };

            return _jarraipenaKontrolatzailea.GordeJarraipenaEtaLortuId(jarraipena);
        }

        public bool GehituDokumentuGenerikoa(string jatorrizkoFitxategia, int pazienteId, int? jarraipenaId, int? osasunLangileId, string dokumentuIzena, string? deskribapena)
        {
            int? benetakoJarraipenaId = ZiurtatuJarraipena(pazienteId, jarraipenaId, osasunLangileId);
            if (!benetakoJarraipenaId.HasValue) return false;

            string helmugaBidea = DokumentuPdfZerbitzua.SortuHelmugaBidea(Path.GetFileName(jatorrizkoFitxategia));
            Directory.CreateDirectory(Path.GetDirectoryName(helmugaBidea)!);
            File.Copy(jatorrizkoFitxategia, helmugaBidea, false);

            Dokumentua dokumentua = new Dokumentua
            {
                JarraipenaId = benetakoJarraipenaId.Value,
                PazienteId = pazienteId,
                FitxategiIzena = Path.GetFileName(jatorrizkoFitxategia),
                BideaZerbitzarian = helmugaBidea,
                DokumentuIzena = dokumentuIzena,
                Deskribapena = string.IsNullOrWhiteSpace(deskribapena) ? null : deskribapena.Trim(),
                IgotzeData = DateTime.Now
            };

            bool ondo = _dokumentuaDb.GordeDokumentua(dokumentua);
            if (!ondo && File.Exists(helmugaBidea))
            {
                File.Delete(helmugaBidea);
            }

            return ondo;
        }

        public bool GehituTxostena(
            int pazienteId,
            int? jarraipenaId,
            int? osasunLangileId,
            string dokumentuIzena,
            string? deskribapena,
            IReadOnlyCollection<TxostenGrafikaMota>? grafikaMotak = null,
            DateTime? grafikaHasieraData = null,
            DateTime? grafikaAmaieraData = null)
        {
            int? benetakoJarraipenaId = ZiurtatuJarraipena(pazienteId, jarraipenaId, osasunLangileId, "Txosten mediko automatikoa sortzeko jarraipena");
            if (!benetakoJarraipenaId.HasValue) return false;

            string pdfBidea = _pdfZerbitzua.SortuPazientearenTxostena(pazienteId, dokumentuIzena, grafikaMotak, grafikaHasieraData, grafikaAmaieraData);

            Dokumentua dokumentua = new Dokumentua
            {
                JarraipenaId = benetakoJarraipenaId.Value,
                PazienteId = pazienteId,
                FitxategiIzena = Path.GetFileName(pdfBidea),
                BideaZerbitzarian = pdfBidea,
                DokumentuIzena = dokumentuIzena,
                Deskribapena = string.IsNullOrWhiteSpace(deskribapena) ? "Txosten mediko automatikoa" : deskribapena.Trim(),
                IgotzeData = DateTime.Now
            };

            bool ondo = _dokumentuaDb.GordeDokumentua(dokumentua);
            if (!ondo && File.Exists(pdfBidea))
            {
                File.Delete(pdfBidea);
            }

            return ondo;
        }
    }
}