using System;
using System.IO;
using System.Text.Json;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public sealed class LoginSegurtasunEgoera
    {
        public bool Blokeatuta { get; init; }
        public bool SaiakeraBakarreraMugatuta { get; init; }
        public int GelditzenDirenSaiakerak { get; init; }
        public DateTime? BlokeoAmaieraLokala { get; init; }
        public TimeSpan GelditzenDenDenbora { get; init; }
    }

    public sealed class LoginEmaitza
    {
        public Erabiltzailea? Erabiltzailea { get; init; }
        public LoginSegurtasunEgoera Egoera { get; init; } = new LoginSegurtasunEgoera();

        public bool Arrakastatsua => Erabiltzailea != null;
        public bool Blokeatuta => Egoera.Blokeatuta;
    }

    public sealed class LoginBlokeoZerbitzua
    {
        private const int HasierakoSaiakeraMuga = 5;
        private static readonly TimeSpan BlokeoIraupena = TimeSpan.FromHours(8);
        private static readonly JsonSerializerOptions JsonAukerak = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private readonly string _egoeraFitxategia;

        public LoginBlokeoZerbitzua()
        {
            string karpeta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GOsasun_app",
                "segurtasuna");

            _egoeraFitxategia = Path.Combine(karpeta, "login-blokeoa.json");
        }

        public LoginSegurtasunEgoera LortuEgoera()
        {
            LoginBlokeoPersistitutakoEgoera egoera = KargatuEgoera();

            if (NormalizatuEgoera(egoera))
            {
                GordeEgoera(egoera);
            }

            return BihurtuIkuspegira(egoera);
        }

        public LoginSegurtasunEgoera ErregistratuHutsegitea()
        {
            LoginBlokeoPersistitutakoEgoera egoera = KargatuEgoera();
            DateTime orainUtc = DateTime.UtcNow;

            if (NormalizatuEgoera(egoera, orainUtc))
            {
                GordeEgoera(egoera);
            }

            if (DagoBlokeatuta(egoera, orainUtc))
            {
                return BihurtuIkuspegira(egoera, orainUtc);
            }

            if (egoera.SaiakeraBakarreraMugatuta)
            {
                AktibatuBlokeoa(egoera, orainUtc);
            }
            else
            {
                egoera.SaiakeraHutsak++;

                if (egoera.SaiakeraHutsak >= HasierakoSaiakeraMuga)
                {
                    egoera.SaiakeraBakarreraMugatuta = true;
                    egoera.SaiakeraHutsak = 0;
                    AktibatuBlokeoa(egoera, orainUtc);
                }
            }

            GordeEgoera(egoera);
            return BihurtuIkuspegira(egoera, orainUtc);
        }

        public void Berrezarri()
        {
            if (!File.Exists(_egoeraFitxategia))
            {
                return;
            }

            try
            {
                File.Delete(_egoeraFitxategia);
            }
            catch
            {
                GordeEgoera(new LoginBlokeoPersistitutakoEgoera());
            }
        }

        private LoginBlokeoPersistitutakoEgoera KargatuEgoera()
        {
            if (!File.Exists(_egoeraFitxategia))
            {
                return new LoginBlokeoPersistitutakoEgoera();
            }

            try
            {
                string json = File.ReadAllText(_egoeraFitxategia);
                return JsonSerializer.Deserialize<LoginBlokeoPersistitutakoEgoera>(json) ?? new LoginBlokeoPersistitutakoEgoera();
            }
            catch
            {
                return new LoginBlokeoPersistitutakoEgoera();
            }
        }

        private void GordeEgoera(LoginBlokeoPersistitutakoEgoera egoera)
        {
            string? direktorioa = Path.GetDirectoryName(_egoeraFitxategia);

            if (!string.IsNullOrWhiteSpace(direktorioa))
            {
                Directory.CreateDirectory(direktorioa);
            }

            string json = JsonSerializer.Serialize(egoera, JsonAukerak);
            File.WriteAllText(_egoeraFitxategia, json);
        }

        private static void AktibatuBlokeoa(LoginBlokeoPersistitutakoEgoera egoera, DateTime orainUtc)
        {
            egoera.BlokeoAmaieraUtc = orainUtc.Add(BlokeoIraupena);
        }

        private static bool DagoBlokeatuta(LoginBlokeoPersistitutakoEgoera egoera, DateTime orainUtc)
        {
            return egoera.BlokeoAmaieraUtc.HasValue && egoera.BlokeoAmaieraUtc.Value > orainUtc;
        }

        private static bool NormalizatuEgoera(LoginBlokeoPersistitutakoEgoera egoera, DateTime? orainUtc = null)
        {
            bool aldatuDa = false;
            DateTime unea = orainUtc ?? DateTime.UtcNow;

            if (egoera.SaiakeraHutsak < 0)
            {
                egoera.SaiakeraHutsak = 0;
                aldatuDa = true;
            }

            if (egoera.BlokeoAmaieraUtc.HasValue && egoera.BlokeoAmaieraUtc.Value <= unea)
            {
                egoera.BlokeoAmaieraUtc = null;
                aldatuDa = true;
            }

            return aldatuDa;
        }

        private static LoginSegurtasunEgoera BihurtuIkuspegira(LoginBlokeoPersistitutakoEgoera egoera, DateTime? orainUtc = null)
        {
            DateTime unea = orainUtc ?? DateTime.UtcNow;
            bool blokeatuta = DagoBlokeatuta(egoera, unea);
            TimeSpan gelditzenDenDenbora = blokeatuta && egoera.BlokeoAmaieraUtc.HasValue
                ? egoera.BlokeoAmaieraUtc.Value - unea
                : TimeSpan.Zero;

            return new LoginSegurtasunEgoera
            {
                Blokeatuta = blokeatuta,
                SaiakeraBakarreraMugatuta = egoera.SaiakeraBakarreraMugatuta,
                GelditzenDirenSaiakerak = egoera.SaiakeraBakarreraMugatuta
                    ? 1
                    : Math.Max(0, HasierakoSaiakeraMuga - egoera.SaiakeraHutsak),
                BlokeoAmaieraLokala = blokeatuta && egoera.BlokeoAmaieraUtc.HasValue
                    ? egoera.BlokeoAmaieraUtc.Value.ToLocalTime()
                    : null,
                GelditzenDenDenbora = gelditzenDenDenbora < TimeSpan.Zero ? TimeSpan.Zero : gelditzenDenDenbora
            };
        }

        private sealed class LoginBlokeoPersistitutakoEgoera
        {
            public int SaiakeraHutsak { get; set; }
            public bool SaiakeraBakarreraMugatuta { get; set; }
            public DateTime? BlokeoAmaieraUtc { get; set; }
        }
    }
}